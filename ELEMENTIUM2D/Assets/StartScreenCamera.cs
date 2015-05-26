using UnityEngine;
using System.Collections;

public class StartScreenCamera : MonoBehaviour {

    void Update()
    {
        transform.Rotate(transform.up, 0.5f * Time.deltaTime);
    }
}
