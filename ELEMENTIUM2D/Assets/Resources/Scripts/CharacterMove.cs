using UnityEngine;
using System.Collections;

public class CharacterMove : MonoBehaviour {

    public float moveSpeed = 10f;

    private float hDir = 0;
    private float vDir = 0;

    private PlayerAnimController playerAnim;

	// Use this for initialization
	void Start () {
        playerAnim = GetComponentInChildren<PlayerAnimController>();
	}
	
	// Update is called once per frame
    void Update()
    {
        hDir = Input.GetAxis("Horizontal");
        vDir = Input.GetAxis("Vertical");

        transform.position = Vector3.MoveTowards(transform.position, transform.position + transform.forward*vDir + transform.right*hDir, moveSpeed * Time.deltaTime);

        playerAnim.horizontal = hDir;
        playerAnim.vertical = vDir;

        if (hDir == 0 && vDir == 0)
            playerAnim.idle = true;
        else playerAnim.idle = false;

    }
}
