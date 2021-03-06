﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Includes;

public class CharacterMove : MonoBehaviour {

    #region Variables
    public Transform fadeSprite;
    private float currentMoveSpeed;
    private float moveSpeed;
    private float inContactWithEnemySpeed;
    private float slowFactor;
    private float previousMoveSpeed;
    private float boostFactor;
    private Vector3 velocity = Vector3.zero;
    private PlayerAnimController playerAnim;

    private bool fixedUpdate = false;
    private float hDir;
    private float vDir;

    private bool doubleTapping = false;
    private bool doubleTapDown = false;
    private bool fade = false;
    private bool update = false;

    private bool inContactWithEnemy;
    private Vector3 collisionWithEnemyNormal;
    private List<Collision> collisions;
    private float diagonalLength;
    private float x;
    private float z;
    private Vector3 boxPosition;
    private float epsilon;
    private float maximumDeltaTime = 0.3333f;
    Vector3 calculatedMotion;

    private float previousRenderTime = 0f;

    //private Transform transf;

    private Vector3 positionToMove;
    private Vector3 previousPosition;
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
        previousRenderTime = 0f;
        collisions = new List<Collision>();
        playerAnim = GetComponentInChildren<PlayerAnimController>();
        //collidedWith = new List<Collision>();
        init();
        x = GetComponent<BoxCollider>().bounds.extents.x;
        z = GetComponent<BoxCollider>().bounds.extents.z;
        diagonalLength = Mathf.Sqrt(Mathf.Pow(x, 2.0f) + Mathf.Pow(z, 2.0f)) * 1.2f;
        boxPosition = GetComponent<BoxCollider>().bounds.center;
        epsilon = 1.4f;

        //transf = transform.position;
        //pastFollowerPosition = transf;
        //pastTargetPosition = transf;

        //transf = transform;
        positionToMove = transform.position;
        previousPosition = transform.position;
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
        if (Physics.Raycast(new Ray(position, direction), out hit, distance, LayerMask.GetMask(Constants.obstacles) | LayerMask.GetMask(Constants.breakable)))
        {
            Debug.DrawRay(position, direction, Color.red);
            if(!hit.collider.isTrigger)
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

    void stopMovement()
    {
        moveSpeed = previousMoveSpeed;
        fade = false;
    }

    void stopDoubleTap()
    {
        doubleTapping = false;
    }

    void doubleTap()
    {
        Player p = GameManager.Instance.Player;

        if (!GameManager.Instance.Stats.inCombat && GameManager.Instance.PlayerRoom && GameManager.Instance.PlayerRoom.isCleared())
        {
            if (Input.GetButton("Dodge") && !doubleTapping && !doubleTapDown)
            {
                if(!p.isTired(0.3f))
                {
                    doubleTapping = true;
                    fade = true;
                    previousMoveSpeed = moveSpeed;
                    moveSpeed = moveSpeed * 2.0f;
                    Invoke("stopMovement", 0.1f);
                    Invoke("stopDoubleTap", 0.1f);
                    p.consumeStamina(0.3f);
                }
                else doubleTapDown = true;
            }
        }
        else if (Input.GetButtonDown("Dodge") && !doubleTapping && !p.isTired(1.7f))
        {
            if(!p.isTired(1.7f))
            {
                doubleTapping = true;
                fade = true;
                previousMoveSpeed = moveSpeed;
                moveSpeed = moveSpeed * 4.0f;
                Invoke("stopMovement", 0.1f);
                Invoke("stopDoubleTap", 0.4f);
                p.consumeStamina(1.7f);
            }
        }
        if (Input.GetButtonUp("Dodge"))
        {
            doubleTapDown = false;
        }
    }

    float signedAngleRadian(Vector3 vec1, Vector3 vec2)
    {
        //Get the dot product
        float dot = Vector3.Dot(vec1, vec2);
        // Divide the dot by the product of the magnitudes of the vectors
        dot = dot / (vec1.magnitude * vec2.magnitude);
        //Get the arc cosin of the angle, you now have your angle in radians 
        var acos = Mathf.Acos(dot);

        return acos * Mathf.Sign(Vector3.Cross(vec1, vec2).y);
    }

    bool checkPosition(Vector3 pos)
    {
        return !rayCast(transform.position, pos - transform.position, (pos - transform.position).magnitude * epsilon);
    }

    void Update()
    {
        boxPosition = GetComponent<BoxCollider>().bounds.center;
        hDir = Input.GetAxis("Horizontal");
        vDir = Input.GetAxis("Vertical");

        // Double tap
        doubleTap();

        float startTimer = Time.realtimeSinceStartup;
        if (vDir == 0.0f && hDir == 0.0f)
        {
            if ((Time.realtimeSinceStartup - previousRenderTime) <= maximumDeltaTime)
                previousRenderTime = Time.realtimeSinceStartup;
            else
            {
                previousRenderTime = Time.realtimeSinceStartup + maximumDeltaTime;
            }
            calculatedMotion = new Vector3(0.0f, 0.0f, 0.0f);
            return;
        }

        Vector3 direction = transform.forward * vDir + transform.right * hDir;
        float angle = signedAngleRadian(direction, transform.right);

        float x = 0;
        float z = 0;

        // h is 1
        x = Mathf.Cos(angle) * 1;

        // h is 1
        z = Mathf.Sin(angle) * 1;

        calculatedMotion = transform.forward * z + transform.right * x;

        rayCastX(ref calculatedMotion, transform.right * hDir);

        rayCastZ(ref calculatedMotion, transform.forward * vDir);

        #region Commented Region
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
        #endregion

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

        if (inContactWithEnemy)
        {
            currentMoveSpeed = inContactWithEnemySpeed;
        }
        else currentMoveSpeed = moveSpeed;

        currentMoveSpeed = currentMoveSpeed * slowFactor / boostFactor;

        Vector3 targetPosition = transform.position + calculatedMotion * currentMoveSpeed;
        targetPosition.y = transform.position.y;
        //transf.position = Vector3.SmoothDamp(transf.position, targetPosition, ref velocity, 0.9f);


        /*positionToMove = SmoothApproach(pastFollowerPosition, pastTargetPosition, targetPosition, currentMoveSpeed);
        Debug.Log(vDir + " " +hDir);
        
        pastFollowerPosition = transform.position;
        pastTargetPosition = targetPosition;*/
        update = true;
        
        float endTimer = startTimer - Time.realtimeSinceStartup;

        // We dont accept delta times greater than 0.333f seconds due in order to not allow big translations of the character screwing up collision with walls(UNTESTED CHANGE)
        if ((Time.realtimeSinceStartup - previousRenderTime) <= maximumDeltaTime)
        {
            positionToMove = Vector3.MoveTowards(transform.position, targetPosition, (targetPosition - transform.position).magnitude * (Time.realtimeSinceStartup - previousRenderTime + 0.0000001f));
            if(!checkPosition(positionToMove))
            {
                positionToMove = transform.position;
            }
            previousRenderTime = Time.realtimeSinceStartup;     
        }         
        else
        {
            positionToMove = Vector3.MoveTowards(transform.position, targetPosition, (targetPosition - transform.position).magnitude * (maximumDeltaTime + 0.0000001f));
            if (!checkPosition(positionToMove))
            {
                positionToMove = transform.position;
            }
            previousRenderTime = Time.realtimeSinceStartup + maximumDeltaTime;
        }

        if (hDir == 0 && vDir == 0)
            playerAnim.idle = true;
        else playerAnim.idle = false;
    }

    void LateUpdate()
    {
        if (update)
            if(fade)
            {
                GameObject faded = GameObject.Instantiate( fadeSprite.gameObject, transform.position, Quaternion.identity) as GameObject;
                faded.GetComponentInChildren<SpriteRenderer>().sprite = GetComponentInChildren<SpriteRenderer>().sprite;
                faded.GetComponentInChildren<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
            }
            previousPosition = transform.position;
            transform.position = positionToMove;
            update = false;
    }

    Vector3 SmoothApproach( Vector3 pastPosition, Vector3 pastTargetPosition, Vector3 targetPosition, float speed )
    {
        float t = Time.smoothDeltaTime * speed;
        if (t == 0)
            return targetPosition;
        Vector3 v = ( targetPosition - pastTargetPosition ) / t;
        Vector3 f = pastPosition - pastTargetPosition + v;
        return targetPosition - v + f * Mathf.Exp( -t );
    }

    public void playerDead()
    {
        slowFactor = 0;
        playerAnim.dead = true;
    }

    public bool isDoubleTapping()
    {
        return doubleTapping;
    }

    public Vector3 getCharacterDirection()
    {
        return calculatedMotion;
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

    public float CurrentMoveSpeed
    {
        get
        {
            return currentMoveSpeed;
        }
    }
}
