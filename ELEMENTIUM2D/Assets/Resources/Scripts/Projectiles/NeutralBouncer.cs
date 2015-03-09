using UnityEngine;
using System.Collections;
using Includes;

public class NeutralBouncer : ProjectileBehaviour {

    public float disabledTimer = 0.06f;

    protected override void Start()
    {
        base.Start();
        damage = AbilityStats.Neutral.ability3.damage;
        Invoke("destroyClone", 6f);
    }

    void OnCollisionStay2D(Collision2D coll)
    {
        base.OnCollisionEnter2D(coll);
    }

    public override void OnCollisionEnter2D(Collision2D collision)
    {
        if (disabledTimer > 0.05f)
        {
            if (collision.gameObject.tag.CompareTo("Enemy") == 0)
            {
                EnemyScript enemy = collision.gameObject.GetComponent<EnemyScript>();
                enemy.takeDamage(damage, Elements.NEUTRAL);
                for (int i = 0; i < AbilityStats.Neutral.ability3.splitNumber; i++)
                {
                    GameObject forkedObj = (GameObject)Instantiate(this.gameObject, collision.transform.position + transform.right * ((collision.collider.bounds.size.y + collision.collider.bounds.size.x) / 2.0f), transform.rotation);
                    forkedObj.GetComponent<NeutralBouncer>().disabledTimer = 0.0f;
                    float result = Random.Range(AbilityStats.Neutral.ability3.negativeSplitAngle, AbilityStats.Neutral.ability3.positiveSplitAngle);
                    forkedObj.transform.Rotate(new Vector3(0, 0, result));
                }
            }
            else if (collision.gameObject.layer == LayerMask.NameToLayer("Unhitable"))
                return;
            base.OnCollisionEnter2D(collision);
        }
    }

    void Update()
    {
        disabledTimer += Time.deltaTime;
        Transform child = transform.FindChild("Sprite") as Transform;
        child.transform.Rotate(0, 0, Time.deltaTime * 92.0f);
        transform.Translate(Vector3.right * Time.deltaTime * AbilityStats.Neutral.ability3.movementForce);
    }

    private void destroyClone()
    {
        Destroy(gameObject);
    }

    public override void applyMovement()
    {
    }
}
