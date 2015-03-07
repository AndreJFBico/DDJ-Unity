using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MIConvexHull;

//[ExecuteInEditMode]
public class Point2Mesh : MonoBehaviour
{
    public void Reset()
    {
        DestroyImmediate(GetComponent<MeshRenderer>());
        DestroyImmediate(GetComponent<SkinnedMeshRenderer>());
        DestroyImmediate(GetComponent<MeshFilter>());
    }

    public void GenerateMesh()
    {
        // Create Vector2 vertices
        Transform[] children = transform.GetComponentsInChildren<Transform>();
        Vector2z[] vertices2D = new Vector2z[children.Length - 1];
        int x = 0;
        float y = 0.0f;
        foreach (Transform t in children)
        {
            if (t.name.CompareTo(transform.name) != 0)
            {
                vertices2D[x] = new Vector2z(t.transform.localPosition.x, t.transform.localPosition.z);
                x++;
                y = t.transform.localPosition.y;
            }
        }
        
        // Use the triangulator to get indices for creating triangles
        int[] indices = EarClipper.Triangulate(vertices2D);

        Vector3[] vertices = new Vector3[vertices2D.Length];
        
        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i] = new Vector3((float)vertices2D[i].x, y, (float)vertices2D[i].y);
        }

        // Create the mesh
        Mesh msh = new Mesh();
        msh.vertices = vertices.ToArray();
        msh.triangles = indices;

        msh.RecalculateNormals();
        msh.RecalculateBounds();

        // Set up game object with mesh;
        MeshRenderer mshr = gameObject.AddComponent(typeof(MeshRenderer)) as MeshRenderer;
        mshr.material = Resources.Load("Materials/defaultMat", typeof(Material)) as Material; 
        gameObject.tag = "navmeshObject";
        MeshFilter filter = gameObject.AddComponent(typeof(MeshFilter)) as MeshFilter;
        filter.mesh = msh;
    }
}
