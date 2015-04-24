using UnityEngine;
using System.Collections;
using Includes;

public class CameraScrpt : MonoBehaviour {

    Transform target;
    public float smoothTime = 0.3F;
    private Vector3 velocity = Vector3.zero;

	// Use this for initialization
	void Start () {
        target = GameManager.Instance.Player.transform;
        GetComponent<UnityEngine.Camera>().transparencySortMode = TransparencySortMode.Orthographic;
	}

    void Update()
    {
        if (!target) 
            return;

        Vector3 targetPosition = target.TransformPoint(new Vector3(0, 20, 0));
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }

	
}
