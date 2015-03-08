using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PathManager : MonoBehaviour
{

    public bool initialized;

    //Unordered list of all graphNodes
    List<PathNode> graph;
    Transform player;
    Transform roomManager;
    AstarPath astarManager;

    // Use this for initialization
    void Start()
    {
        initialized = false;
        merge();
    }

    void findAllNodes()
    {
        //GameObject[] transforms = GameObject.FindGameObjectsWithTag("navmeshObject");

        //Mesh.CombineMeshes(transforms);
        /*foreach (GameObject t in transforms)
        {
            PathNode node = new PathNode(t.transform.position, false);
            graph.Add(node);
        }*/
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void merge()
    {
        // Search for all navmeshes and place them as children of this object
        GameObject[] navmeshObjects = GameObject.FindGameObjectsWithTag("navmeshObject");
        foreach (GameObject obj in navmeshObjects)
        {
            obj.transform.parent = this.transform;
        }

        Matrix4x4 myTransform = transform.worldToLocalMatrix;
        Dictionary<Material, List<CombineInstance>> combines = new Dictionary<Material, List<CombineInstance>>();
        MeshRenderer[] meshRenderers = GetComponentsInChildren<MeshRenderer>();
        foreach (var meshRenderer in meshRenderers)
        {
            foreach (var material in meshRenderer.sharedMaterials)
                if (material != null && !combines.ContainsKey(material))
                    combines.Add(material, new List<CombineInstance>());
        }

        MeshFilter[] meshFilters = GetComponentsInChildren<MeshFilter>();
        foreach (var filter in meshFilters)
        {
            if (filter.sharedMesh == null)
                continue;
            CombineInstance ci = new CombineInstance();
            ci.mesh = filter.sharedMesh;
            ci.transform = myTransform * filter.transform.localToWorldMatrix;
            combines[filter.GetComponent<Renderer>().sharedMaterial].Add(ci);
            filter.GetComponent<Renderer>().enabled = false;
        }

        foreach (Material m in combines.Keys)
        {
            var go = new GameObject("Combined Navmesh");
            go.transform.parent = transform;
            go.transform.localPosition = Vector3.zero;
            go.transform.localRotation = Quaternion.identity;
            go.transform.localScale = Vector3.one;

            var filter = go.AddComponent<MeshFilter>();
            filter.mesh.CombineMeshes(combines[m].ToArray(), true, true);

            AutoWeld(filter.mesh, 0.15f, 1.0f);
            astarManager = transform.gameObject.AddComponent<AstarPath>();
            astarManager.astarData.AddGraph("NavMeshGraph");
            astarManager.astarData.navmesh.sourceMesh = filter.mesh;
            astarManager.astarData.navmesh.sourceMesh.Optimize();
            astarManager.showNavGraphs = false;
            astarManager.showSearchTree = false;
            astarManager.showGraphs = false;
            astarManager.Scan();
            initialized = true;

            var renderer = go.AddComponent<MeshRenderer>();
            renderer.material = m;
            break;
        }

        //Deleate previous objects
        Transform[] children = transform.GetComponentsInChildren<Transform>();
        foreach (Transform t in children)
        {
            if (t.tag.CompareTo("navmeshObject") == 0)
            {
                Destroy(t.gameObject);
            }
        }
    }

    private void AutoWeld(Mesh mesh, float threshold, float bucketStep)
    {
        Vector3[] oldVertices = mesh.vertices;
        Vector3[] newVertices = new Vector3[oldVertices.Length];
        int[] old2new = new int[oldVertices.Length];
        int newSize = 0;
        // Find AABB
        Vector3 min = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
        Vector3 max = new Vector3(float.MinValue, float.MinValue, float.MinValue);
        for (int i = 0; i < oldVertices.Length; i++)
        {
            if (oldVertices[i].x < min.x) min.x = oldVertices[i].x;
            if (oldVertices[i].y < min.y) min.y = oldVertices[i].y;
            if (oldVertices[i].z < min.z) min.z = oldVertices[i].z;
            if (oldVertices[i].x > max.x) max.x = oldVertices[i].x;
            if (oldVertices[i].y > max.y) max.y = oldVertices[i].y;
            if (oldVertices[i].z > max.z) max.z = oldVertices[i].z;
        }
        // Make cubic buckets, each with dimensions "bucketStep"
        int bucketSizeX = Mathf.FloorToInt((max.x - min.x) / bucketStep) + 1;
        int bucketSizeY = Mathf.FloorToInt((max.y - min.y) / bucketStep) + 1;
        int bucketSizeZ = Mathf.FloorToInt((max.z - min.z) / bucketStep) + 1;
        List<int>[, ,] buckets = new List<int>[bucketSizeX, bucketSizeY, bucketSizeZ];
        // Make new vertices
        for (int i = 0; i < oldVertices.Length; i++)
        {
            // Determine which bucket it belongs to
            int x = Mathf.FloorToInt((oldVertices[i].x - min.x) / bucketStep);
            int y = Mathf.FloorToInt((oldVertices[i].y - min.y) / bucketStep);
            int z = Mathf.FloorToInt((oldVertices[i].z - min.z) / bucketStep);
            // Check to see if it's already been added
            if (buckets[x, y, z] == null)
                buckets[x, y, z] = new List<int>(); // Make buckets lazily
            for (int j = 0; j < buckets[x, y, z].Count; j++)
            {
                Vector3 to = newVertices[buckets[x, y, z][j]] - oldVertices[i];
                if (Vector3.SqrMagnitude(to) < threshold)
                {
                    old2new[i] = buckets[x, y, z][j];
                    goto skip; // Skip to next old vertex if this one is already there
                }
            }
            // Add new vertex
            newVertices[newSize] = oldVertices[i];
            buckets[x, y, z].Add(newSize);
            old2new[i] = newSize;
            newSize++;
        skip: ;
        }
        // Make new triangles
        int[] oldTris = mesh.triangles;
        int[] newTris = new int[oldTris.Length];
        for (int i = 0; i < oldTris.Length; i++)
        {
            newTris[i] = old2new[oldTris[i]];
        }
        Vector3[] finalVertices = new Vector3[newSize];
        for (int i = 0; i < newSize; i++)
            finalVertices[i] = newVertices[i];
        mesh.Clear();
        mesh.vertices = finalVertices;
        mesh.triangles = newTris;
        mesh.RecalculateNormals();
        mesh.Optimize();
    }

    /*private void AutoWeld(Mesh mesh, float threshold)
    {
        Vector3[] verts = mesh.vertices;
        // Build new vertex buffer and remove "duplicate" verticies
        // that are within the given threshold.
        List<Vector3> newVerts = new List<Vector3>();
        List<Vector2> newUVs = new List<Vector2>();
        int k = 0;
        foreach (Vector3 vert in verts)
        {
            // Has vertex already been added to newVerts list?
            foreach (Vector3 newVert in newVerts)
                if (Vector3.Distance(newVert, vert) <= threshold)
                    goto skipToNext;
            // Accept new vertex!
            newVerts.Add(vert);
            newUVs.Add(mesh.uv[k]);
        skipToNext: ;
            ++k;
        }
        // Rebuild triangles using new verticies
        int[] tris = mesh.triangles;
        for (int i = 0; i < tris.Length; ++i)
        {
            // Find new vertex point from buffer
            for (int j = 0; j < newVerts.Count; ++j)
            {
                if (Vector3.Distance(newVerts[j], verts[tris[i]]) <= threshold)
                {
                    tris[i] = j;
                    break;
                }
            }
        }
        // Update mesh!
        mesh.Clear();
        mesh.vertices = newVerts.ToArray();
        mesh.triangles = tris;
        mesh.uv = newUVs.ToArray();
        mesh.RecalculateBounds();
    }*/
}
