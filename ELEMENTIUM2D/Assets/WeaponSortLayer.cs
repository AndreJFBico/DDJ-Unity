using UnityEngine;
using System.Collections;

public class WeaponSortLayer : MonoBehaviour {

    private GameObject player;

    private SpriteRenderer render;

    private SpriteRenderer weaponSprite;

    //private float weaponOffset;

	// Use this for initialization
    void Start()
    {
        render = GameObject.FindGameObjectWithTag("PlayerSprite").GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player");
        
        //weaponOffset = transform.parent.position.z;
    }
	
	// Update is called once per frame
	void LateUpdate () {

        if (transform.position.z > player.transform.position.z)
        {
            renderer.sortingOrder = render.sortingOrder - 1;
        }
        if (transform.position.z < player.transform.position.z)
        {
            renderer.sortingOrder = render.sortingOrder + 1;
        }
	}
}
