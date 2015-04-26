using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Includes;

public class CharacterMove : MonoBehaviour {

    #region Variables
    private float currentMoveSpeed;
    private float moveSpeed;
    private float inContactWithEnemySpeed;
    private float slowFactor;
    private float boostFactor;
    private Vector3 velocity = Vector3.zero;
    private PlayerAnimController playerAnim;

    private bool fixedUpdate = false;
    private float hDir;
    private float vDir;

    private bool inContactWithEnemy;
    private Vector3 collisionWithEnemyNormal;
    private List<Collision> collisions;
    private float diagonalLength;
    private float x;
    private float z;
    private Vector3 boxPosition;
    private float epsilon;
    Vector3 calculatedMotion;

    private Transform transf;

    private Vector3 pastFollowerPosition;
    private Vector3 pastTargetPosition;
    //private List<Collision> collidedWith; 
    #endregion

    public float MoveSpeed
    {
        get { return moveSpeed;}
        set { moveSpeed = value;}
    }

    public float Slow
    {
        get { return slowFactor; }
        set { slowFactor = value; }
    }

    public float Boost
    {
        get { return boostFactor; }
        set { boostFactor = value; }
    }

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
        transf = transform;
        pastFollowerPosition = transform.position;
        pastTargetPosition = transf.position;
	}

    #region OnCollision Handlers
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
        else */
        if (collision.transform.tag.CompareTo("Enemy") == 0)
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
        else */
        if (collision.transform.tag.CompareTo("Enemy") == 0)
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

    public void setInContactWithEnemy(bool val)
    {
        inContactWithEnemy = val;
    }

    public void CollisionExit(Collision collision)
    {
        /* if (collision.gameObject.layer == LayerMask.NameToLayer("Obstacles"))
         {
             inContact = false;
         }
         else */
        if (collision.transform.tag.CompareTo("Enemy") == 0)
        {
            inContactWithEnemy = false;
        }
    } 
    #endregion

    #region RayCast functions
    bool rayCast(Vector3 position, Vector3 direction, float distance)
    {
        RaycastHit hit;
        if (Physics.Raycast(new Ray(position, direction), out hit, distance, LayerMask.GetMask("Obstacles") | LayerMask.GetMask(Constants.breakable)))
        {
            Debug.DrawRay(position, direction, Color.red);
            return true;
        }
        Debug.DrawRay(position, direction, Color.white);
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
    #endregion
    
	
	// Update is called once per frame
    void Update()
    {
        //fixedUpdate = true;
        boxPosition = GetComponent<BoxCollider>().bounds.center;
        hDir = Input.GetAxis("Horizontal");
        vDir = Input.GetAxis("Vertical");

        calculatedMotion = transf.forward * vDir + transf.right * hDir;
       
        rayCastX(ref calculatedMotion, transf.right * hDir);

        rayCastZ(ref calculatedMotion, transf.forward * vDir);


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
        else */
        if (inContactWithEnemy)
        {
            float dotProduct = Vector3.Dot(calculatedMotion, collisionWithEnemyNormal);
            // Facing a wall
            if (dotProduct < 0.0f)
            {
                Vector3 undesiredMotion = collisionWithEnemyNormal * dotProduct;
                calculatedMotion = calculatedMotion - undesiredMotion / 2.0f;
            }
        }
        Vector3 targetPosition = transf.position + calculatedMotion;
        targetPosition.y = transf.position.y;

        
        if(inContactWithEnemy)
        {
            currentMoveSpeed = inContactWithEnemySpeed;
        }
        else currentMoveSpeed = moveSpeed;

        currentMoveSpeed = currentMoveSpeed * slowFactor / boostFactor;
        //transf.position = Vector3.SmoothDamp(transf.position, targetPosition, ref velocity, 0.9f);


        transf.position = SmoothApproach(pastFollowerPosition, pastTargetPosition, targetPosition, currentMoveSpeed);
        pastFollowerPosition = transform.position;
        pastTargetPosition = targetPosition;

        //transf.position = Vector3.MoveTowards(transf.position, targetPosition, currentMoveSpeed * Time.smoothDeltaTime);
        
        if (hDir == 0 && vDir == 0)
            playerAnim.idle = true;
        else playerAnim.idle = false;
    }

    public void playerDead()
    {
        slowFactor = 0;
        playerAnim.dead = true;
    }

    void LateUpdate()
    {
        /*if (fixedUpdate)
        {*/
            transform.position = transf.position;
            fixedUpdate = false;
        //} 
    }

    Vector3 SmoothApproach( Vector3 pastPosition, Vector3 pastTargetPosition, Vector3 targetPosition, float speed )
    {
        float t = Time.smoothDeltaTime * speed;
        Vector3 v = ( targetPosition - pastTargetPosition ) / t;
        Vector3 f = pastPosition - pastTargetPosition + v;
        return targetPosition - v + f * Mathf.Exp( -t );
    }

    public void init()
    {
        moveSpeed = GameManager.Instance.Stats.moveSpeed;
        inContactWithEnemySpeed = GameManager.Instance.Stats.moveInContactWithEnemy;
        slowFactor = 1;
        boostFactor = 1;

        hDir = 0;
        vDir = 0;

        inContactWithEnemy = false;
    }
}
