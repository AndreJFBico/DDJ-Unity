using UnityEngine;
using System.Collections;

public class PlayerAnimController : MonoBehaviour {

    public float horizontal;
    public float vertical;
    public bool idle;

    private Animator anim;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        anim.SetFloat("Horizontal", horizontal);
        anim.SetFloat("Vertical", vertical);
        anim.SetBool("Idle", idle);
	}
}
