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

   //private float quarterPi = 0;
    public float eighthPi = 0;

    //private float hDir = 0;
    //private float vDir = 0;

    float spriteDot = 0;
    float animDot = 0;

    public string[] js;

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
        //quarterPi = Mathf.Cos(Mathf.PI / 4);
        eighthPi = Mathf.Cos(Mathf.PI / 8);

	}

    void detectTargetPosition()
    {
		Transform targetMarker = transform.FindChild("BarrelEnd").FindChild("JoystickTarget");
        js = Input.GetJoystickNames();
		if(Input.GetJoystickNames().Length > 0){
			targetMarker.gameObject.SetActive(true);
			float hor = Input.GetAxisRaw ("JoystickRightHor");
			float ver = Input.GetAxisRaw ("JoystickRightVer");
			if(Mathf.Abs(hor) < 0.1)
				hor = 0;
			if(Mathf.Abs(ver) < 0.1)
				ver = 0;
			if(hor == 0 && ver == 0)
				return;

			//Debug.Log (hor + " " + ver);
			point = new Vector3(transform.position.x + hor, transform.position.y, transform.position.z + ver);
			float rot_y = Mathf.Atan2(hor, ver) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.Euler(90.0f, rot_y, 0f);

			float n = Mathf.Sqrt( hor * hor + ver * ver);
			float nhor = hor / n;
			float nver = ver / n;
			targetMarker.position = new Vector3(transform.position.x + nhor, transform.position.y, transform.position.z + nver); 
		}
		else{
			targetMarker.gameObject.SetActive(false);
			Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
	        Vector3 aux = worldPosition - transform.position;
	        aux.Normalize();

	        point = new Vector3(worldPosition.x, transform.position.y, worldPosition.z);
			

	        float rot_y = Mathf.Atan2(aux.x, aux.z) * Mathf.Rad2Deg;
	        transform.rotation = Quaternion.Euler(90.0f, rot_y, 0f);

	        //RaycastHit hit;
	        //Vector3 aux = Vector3.right;
	        /*Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
	        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
	        {
	            point = new Vector3(hit.point.x, transform.position.y, hit.point.z);
	        }*/
	        //transform.LookAt(aux);
		}
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
        Vector3 axis = Vector3.forward;
        Vector3 target = Vector3.Normalize(point - transform.position);

        spriteDot = Vector3.Dot(SpriteAxis, target);
        animDot = Vector3.Dot(axis, target);

        //Flip Weapon
        if (spriteDot <= 0)
        {
            weapon1.SetActive(false);
            weapon2.SetActive(true);
            activeWeapon = 2;
        }

        if (spriteDot > 0)
        {
            weapon1.SetActive(true);
            weapon2.SetActive(false);
            activeWeapon = 1;
        }

        //Change Weapon->Player relative Depth
        float aux = (1 - eighthPi);

        if (animDot < -aux)
        {
            weapon1Sprite.sortingOrder = playerSprite.sortingOrder - 1;
            weapon2Sprite.sortingOrder = playerSprite.sortingOrder - 1;
        }
        if (animDot > -aux)
        {
            weapon1Sprite.sortingOrder = playerSprite.sortingOrder + 1;
            weapon2Sprite.sortingOrder = playerSprite.sortingOrder + 1;
        }

        //Change player Anim
        if (animDot > eighthPi)
        {
            playerAnim.vertical = 1;
            playerAnim.horizontal = 0;
        }
        else if (animDot > aux && spriteDot > 0)
        {
            playerAnim.vertical = 1;
            playerAnim.horizontal = 1;
        }
        else if (animDot > aux && spriteDot < 0)
        {
            playerAnim.vertical = 1;
            playerAnim.horizontal = -1;
        }
        else if (animDot > -aux && spriteDot > 0)
        {
            playerAnim.vertical = 0;
            playerAnim.horizontal = 1;
        }
        else if (animDot > -aux && spriteDot < 0)
        {
            playerAnim.vertical = 0;
            playerAnim.horizontal = -1;
        }
        else if (animDot <= -aux)
        {
            playerAnim.vertical = -1;
            //playerAnim.horizontal = 0;
        }

    }
}
