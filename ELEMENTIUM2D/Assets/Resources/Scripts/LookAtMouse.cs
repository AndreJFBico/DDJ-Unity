using UnityEngine;
using System.Collections;

public class LookAtMouse : MonoBehaviour {

    private SpriteRenderer playerSprite;
    private SpriteRenderer weapon1Sprite;
    private SpriteRenderer weapon2Sprite;

    public PlayerAnimController playerAnim;

    private GameObject weapon1;
    private GameObject weapon2;
    private int activeWeapon;

    private Vector3 point;

    private float quarterPi = 0;
    public float eighthPi = 0;

    private float hDir = 0;
    private float vDir = 0;

    float spriteDot = 0;
    float animDot = 0;


	// Use this for initialization
    void Start()
    {
        playerSprite = GameObject.FindGameObjectWithTag("PlayerSprite").GetComponent<SpriteRenderer>();

        SpriteRenderer[] renderers = GameObject.FindGameObjectWithTag("WeaponPos1").GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer item in renderers)
            if(item.name == "Weapon")
                weapon1Sprite = item;

        renderers = GameObject.FindGameObjectWithTag("WeaponPos2").GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer item in renderers)
            if (item.name == "Weapon")
                weapon2Sprite = item;

        weapon1 = GameObject.FindGameObjectWithTag("WeaponPos1");
        weapon2 = GameObject.FindGameObjectWithTag("WeaponPos2");
        activeWeapon = 1;
        quarterPi = Mathf.Cos(Mathf.PI / 4);
        eighthPi = Mathf.Cos(Mathf.PI / 8);

	}

    void detectTargetPosition()
    {
        Vector3 aux = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        aux.Normalize();

        float rot_z = Mathf.Atan2(aux.y, aux.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);

        RaycastHit hit;
        //Vector3 aux = Vector3.right;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            aux = new Vector3(hit.point.x, hit.point.y, transform.position.z);
        }
        point = aux;
    }
    
    void LateUpdate()
    {
        //float aux = (1 - eighthPi);

        //if (animDot < -aux)
        //{
        //    weapon1Sprite.sortingOrder = weapon1Sprite.sortingOrder - 1;
        //    weapon2Sprite.sortingOrder = weapon1Sprite.sortingOrder - 1;
        //}
        //if (animDot > -aux)
        //{
        //    weapon1Sprite.sortingOrder = weapon1Sprite.sortingOrder + 1;
        //    weapon2Sprite.sortingOrder = weapon1Sprite.sortingOrder + 1;
        //}
    }

	// Update is called once per frame
    void Update()
    {
        detectTargetPosition();

        Vector3 SpriteAxis = Vector3.right;
        Vector3 axis = Vector3.up;
        Vector3 target = Vector3.Normalize(transform.position - point);

        spriteDot = Vector3.Dot(SpriteAxis, target);
        animDot = Vector3.Dot(axis, target);

        //Flip Weapon
        if (spriteDot <= 0)
        {
            weapon1.SetActive(true);
            weapon2.SetActive(false);
            activeWeapon = 1;
        }

        if (spriteDot > 0)
        {
            weapon1.SetActive(false);
            weapon2.SetActive(true);
            activeWeapon = 2;
        }

        //Change Weapon->Player relative Depth
        float aux = (1 - eighthPi);

        if (animDot < -aux)
        {
            weapon1Sprite.sortingOrder = weapon1Sprite.sortingOrder-1;
            weapon2Sprite.sortingOrder = weapon1Sprite.sortingOrder-1;
        }
        if (animDot > -aux)
        {
            weapon1Sprite.sortingOrder = weapon1Sprite.sortingOrder+1;
            weapon2Sprite.sortingOrder = weapon1Sprite.sortingOrder+1;
        }

        //Change player Anim
        if (animDot > aux)
        {
            playerAnim.vertical = -1;
        }
        else if (animDot > -aux && spriteDot > 0)
        {
            playerAnim.vertical = 0;
            playerAnim.horizontal = -1;
        }
        else if (animDot > -aux && spriteDot < 0)
        {
            playerAnim.vertical = 0;
            playerAnim.horizontal = 1;
        }
        else if (animDot > -eighthPi && spriteDot > 0)
        {
            playerAnim.vertical = 1;
            playerAnim.horizontal = -1;
        }
        else if (animDot > -eighthPi && spriteDot < 0)
        {
            playerAnim.vertical = 1;
            playerAnim.horizontal = 1;
        }
        else if (animDot <= -eighthPi)
        {
            playerAnim.vertical = 1;
            playerAnim.horizontal = 0;
        }

    }
}
