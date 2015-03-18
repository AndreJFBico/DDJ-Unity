using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Includes;

public class CharacterMove : MonoBehaviour {

    private float moveSpeed;
    private float inContactWithEnemySpeed;

    private PlayerAnimController playerAnim;

    private float hDir;
    private float vDir;

    private float inContactNumber = 0;

    private bool inContact;
    private bool inContactWithEnemy;
    private Vector3 collisionNormal;
    private Vector3 collisionWithEnemyNormal;
    private List<Collision> collisions;
    private float diagonalLength;
    private float x;
    private float z;
    private Vector3 boxPosition;
    private float epsilon;

    private bool blockedRight = false;
    private bool blockedLeft = false;
    private bool blockedUp = false;
    private bool blockedDown = false;
    //private List<Collision> collidedWith;

	// Use this for initialization
	void Start () {
        collisions = new List<Collision>();
        playerAnim = GetComponentInChildren<PlayerAnimController>();
        //collidedWith = new List<Collision>();
        init();
        x = GetComponent<BoxCollider>().bounds.extents.x;
        z = GetComponent<BoxCollider>().bounds.extents.z;
        diagonalLength = Mathf.Sqrt(Mathf.Pow(x, 2.0f) + Mathf.Pow(z, 2.0f)) * 1.2f;
        boxPosition = GetComponent<BoxCollider>().bounds.center;
        epsilon = 1.4f;
	}

    public void CollisionStay(Collision collision)
    {
        /*if (collision.gameObject.layer == LayerMask.NameToLayer("Obstacles"))
        {
            if (!inContact)
            {
                inContact = true;
                if (collision.contacts != null && collision.contacts.Length > 0)
                {
                    collisionNormal = collision.contacts[0].normal;
                }
            }
        }
        else */if (collision.transform.tag.CompareTo("Enemy") == 0)
        {
            if (!inContactWithEnemy)
            {
                inContactWithEnemy = true;
                if (collision.contacts != null && collision.contacts.Length > 0)
                {
                    collisionWithEnemyNormal = collision.contacts[0].normal;
                }
            }
        }
    }

    public void CollisionEnter(Collision collision)
    {
        /*if (collision.gameObject.layer == LayerMask.NameToLayer("Obstacles") )
        {
            if (!inContact)
            {
                inContact = true;
                if (collision.contacts != null && collision.contacts.Length > 0)
                {
                    collisionNormal = collision.contacts[0].normal;
                }
            }
            else
            {
                if (collision.contacts != null && collision.contacts.Length > 0)
                {
                    collisionNormal += collision.contacts[0].normal;
                   // collisionNormal.Normalize();
                }
           }
        }
        else */if (collision.transform.tag.CompareTo("Enemy") == 0)
        {
            if (!inContactWithEnemy)
            {
                inContactWithEnemy = true;
                if (collision.contacts != null && collision.contacts.Length > 0)
                {
                    collisionWithEnemyNormal = collision.contacts[0].normal;
                }
            }
            else
            {
                if (collision.contacts != null && collision.contacts.Length > 0)
                {
                    collisionWithEnemyNormal += collision.contacts[0].normal;
                    collisionWithEnemyNormal.Normalize();
                }
            }
        }
    }

    public void CollisionExit(Collision collision)
    {
       /* if (collision.gameObject.layer == LayerMask.NameToLayer("Obstacles"))
        {
            inContact = false;
        }
        else */if (collision.transform.tag.CompareTo("Enemy") == 0)
        {
            inContactWithEnemy = false;
        }
    }

    bool rayCast(Vector3 position, Vector3 direction, float distance)
    {
        RaycastHit hit;
        if (Physics.Raycast(new Ray(position, direction), out hit, distance, LayerMask.GetMask("Obstacles")))
        {
            Debug.DrawRay(position, direction);
            return true;
        }
        Debug.DrawRay(position, direction);
        return false;
    }

    bool rayCastZ(ref Vector3 calculatedMotion, Vector3 direction)
    {
        if (rayCast(boxPosition + new Vector3(0.0f, 0.0f, 0.0f), direction, z * epsilon))
        {
            calculatedMotion.z = 0.0f;
            return true;
        }
        if (rayCast(boxPosition + new Vector3(x, 0.0f, 0.0f), direction, z * epsilon))
        {
            calculatedMotion.z = 0.0f;
            return true;
        }
        if (rayCast(boxPosition + new Vector3(-x, 0.0f, 0.0f), direction, z * epsilon))
        {
            calculatedMotion.z = 0.0f;
            return true;
        }
        return false;
    }


    bool rayCastX(ref Vector3 calculatedMotion, Vector3 direction)
    {
        if (rayCast(boxPosition + new Vector3(0.0f, 0.0f, 0.0f), direction, x * epsilon))
        {
            calculatedMotion.x = 0.0f;
            return true;
        }
        if (rayCast(boxPosition + new Vector3(0.0f, 0.0f, z), direction, x * epsilon))
        {
            calculatedMotion.x = 0.0f;
            return true;
        }
        if (rayCast(boxPosition + new Vector3(0.0f, 0.0f, -z), direction, x * epsilon))
        {
            calculatedMotion.x = 0.0f;
            return true;
        }
        return false;
    }
    
	
	// Update is called once per frame
    void Update()
    {
        boxPosition = GetComponent<BoxCollider>().bounds.center;
        hDir = Input.GetAxis("Horizontal");
        vDir = Input.GetAxis("Vertical");

        Vector3 calculatedMotion = transform.forward * vDir + transform.right * hDir;
       
        rayCastX(ref calculatedMotion, transform.right * hDir);

        rayCastZ(ref calculatedMotion, transform.forward * vDir);


        /*if (rayCast(new Vector3(x, 0.0f, z), diagonalLength))
        {
            if (calculatedMotion.z > 0.0f)
                calculatedMotion.z = 0.0f;
            if (calculatedMotion.x > 0.0f)
                calculatedMotion.x = 0.0f;
        }
        if (rayCast(new Vector3(x, 0.0f, -z), diagonalLength))
        {
            if (calculatedMotion.z < 0.0f)
                calculatedMotion.z = 0.0f;
            if (calculatedMotion.x > 0.0f)
                calculatedMotion.x = 0.0f;
        }
        if (rayCast(new Vector3(-x, 0.0f, z), diagonalLength))
        {
            if (calculatedMotion.z > 0.0f)
                calculatedMotion.z = 0.0f;
            if (calculatedMotion.x < 0.0f)
                calculatedMotion.x = 0.0f;
        }
        if (rayCast(new Vector3(-x, 0.0f, -z), diagonalLength))
        {
            if (calculatedMotion.z < 0.0f)
                calculatedMotion.z = 0.0f;
            if (calculatedMotion.x < 0.0f)
                calculatedMotion.x = 0.0f;
        }*/

        /*if (inContact)
        {
            float dotProduct = Vector3.Dot(calculatedMotion, collisionNormal);
            // Facing a wall
            if(dotProduct < 0.0f)
            {
                Vector3 undesiredMotion = collisionNormal * dotProduct;
                calculatedMotion = calculatedMotion - undesiredMotion;          
            }
        }
        else */if (inContactWithEnemy)
        {
            float dotProduct = Vector3.Dot(calculatedMotion, collisionWithEnemyNormal);
            // Facing a wall
            if (dotProduct < 0.0f)
            {
                Vector3 undesiredMotion = collisionWithEnemyNormal * dotProduct;
                calculatedMotion = calculatedMotion - undesiredMotion / 2.0f;
            }
        }
        Vector3 targetPosition = transform.position + calculatedMotion;
        targetPosition.y = transform.position.y;


        if(inContactWithEnemy)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, inContactWithEnemySpeed * Time.deltaTime);
        }
        else transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        if (hDir == 0 && vDir == 0)
            playerAnim.idle = true;
        else playerAnim.idle = false;
    }

    public void init()
    {
        moveSpeed = PlayerStats.moveSpeed;
        inContactWithEnemySpeed = PlayerStats.moveInContactWithEnemy;

        hDir = 0;
        vDir = 0;

        inContact = false;
        inContactWithEnemy = false;
    }
}
