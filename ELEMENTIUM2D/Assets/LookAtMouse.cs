using UnityEngine;
using System.Collections;

public class LookAtMouse : MonoBehaviour {

    private SpriteRenderer playerSprite;
    private SpriteRenderer weaponSprite;

    private Sprite weapon1;
    private Sprite weapon2;

    private Vector3 point;

    private float quarterPi = 0;

	// Use this for initialization
    void Start()
    {
        playerSprite = GameObject.FindGameObjectWithTag("PlayerSprite").GetComponent<SpriteRenderer>();
        weaponSprite = GameObject.FindGameObjectWithTag("Weapon").GetComponent<SpriteRenderer>();
        weapon1 = Resources.Load<Sprite>("Sprites/Weapon/Weapon");
        weapon2 = Resources.Load<Sprite>("Sprites/Weapon/Weapon2");
        quarterPi = Mathf.Cos(Mathf.PI / 4);
	}

    void detectTargetPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        Vector3 aux = Vector3.right;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            aux = new Vector3(hit.point.x, transform.position.y, hit.point.z);
            transform.LookAt(aux);
        }
        point = aux;
    }

    
       

	// Update is called once per frame
    void Update()
    {
        detectTargetPosition();
        float yRot = 0;
        Vector3 SpriteAxis = Vector3.right;
        Vector3 axis = Vector3.up;
        Vector3 target = Vector3.Normalize(transform.position - point);

        float spriteDot = Vector3.Dot(SpriteAxis, target);
        float AnimDot = Vector3.Dot(axis, target);

        if (spriteDot <= 0)
        {
            weaponSprite.sprite = weapon1;
        }
        if (spriteDot > 0)
        {
            weaponSprite.sprite = weapon2;
        }


        //if (vDir > 0)
        //{
        //    weaponSprite.sortingOrder = 0;
        //}
        //if (vDir < 0)
        //{
        //    weaponSprite.sortingOrder = 2;
        //}
    }
}
