using UnityEngine;
using System.Collections;
using Includes;

public class CameraScrpt : MonoBehaviour {

    Transform target;
    public float smoothTime = 0.3F;
    private Vector3 velocity = Vector3.zero;
    private Vector3 transf;
    private bool fixedUpdate = false;

    public float pixelToUnits = 40f;

	// Use this for initialization
	void Start () 
    {
        target = GameManager.Instance.Player.transform;
        GetComponent<UnityEngine.Camera>().transparencySortMode = TransparencySortMode.Orthographic;
	}

    void LateUpdate()
    {
        /*if (!target) 
            return;
        fixedUpdate = true;

        float target_x = target.transform.position.x;
        float target_y = target.transform.position.z;
 
        float rounded_x = RoundToNearestPixel(target_x);
        float rounded_y = RoundToNearestPixel(target_y);
        transf = new Vector3(rounded_x, transform.position.y, rounded_y); // this is 2d, so my camera is that far from the screen.
        transform.position  = transf = Vector3.SmoothDamp(transform.position, transf, ref velocity, smoothTime);*/
        Vector3 targetPosition = target.TransformPoint(new Vector3(0, 20, 0));
        //transform.position = targetPosition;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }

    public float RoundToNearestPixel(float unityUnits)
    {
        float valueInPixels = unityUnits * pixelToUnits;
        valueInPixels = Mathf.Round(valueInPixels);
        float roundedUnityUnits = valueInPixels * (1 / pixelToUnits);
        return roundedUnityUnits;
    }

}