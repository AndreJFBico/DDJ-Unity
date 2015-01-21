using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class LeftRightShift : MonoBehaviour {

    private GameObject playerObject;
    private Animator anim;
    private float animDot;
	// Use this for initialization
    void Start()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
    void Update()
    {
        Vector3 player = playerObject.transform.position;
        Vector3 axis = Vector3.right;
        Vector3 targetPos = new Vector3(player.x, 0, player.z);
        Vector3 target = Vector3.Normalize(transform.position - targetPos);

        animDot = Vector3.Dot(axis, target);

        if(animDot <= 0)
        {
            anim.SetBool("Right", true);
        }
        else
        {
            anim.SetBool("Right", false);
        }
	}
}
