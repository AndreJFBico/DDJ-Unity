using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PathNode : MonoBehaviour
{

    //Nodes that are in contact or visible from this one
   /* private List<PathNode> inContact;
    private Vector3 position;
    private bool temporary;*/

   /* public PathNode(Vector3 pos, bool temp)
    {
        temporary = temp;
        position = pos;
    }*/

    public bool isVisibleWith(PathNode node)
    {
        /*Vector3 raycastPos = new Vector3(position.x, position.y);
        Ray ray = new Ray(raycastPos, node.position - position);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, (node.position - position).magnitude + 0.5f, pathfindingLayer))
        {
            Debug.DrawRay(transform.position, transform.forward * hit.distance, Color.yellow);
            Debug.Log("Did Hit " + hit.collider.gameObject.name);
            return true;
        }*/
        return false;
    }

    /*public void addContact(PathNode node)
    {
        inContact.Add(node);
    }*/
}
