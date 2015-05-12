using UnityEngine;

public class ClickIgnore : MonoBehaviour, ICanvasRaycastFilter
{
    public bool IsRaycastLocationValid(Vector2 screenPoint, Camera eventCamera)
    {
        return false;
    }
}