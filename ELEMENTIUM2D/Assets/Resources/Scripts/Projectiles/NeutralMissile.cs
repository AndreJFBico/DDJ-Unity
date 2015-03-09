using UnityEngine;
using System.Collections;
using Includes;

public class NeutralMissile : ProjectileBehaviour {

    private Transform target;
    private float timer = 0.0f;

    protected override void Start()
    {
        base.Start();
        damage = AbilityStats.Neutral.ability2.damage;
        Invoke("destroyClone", 6f);
    }

    public override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.CompareTo("Enemy") == 0)
        {
            EnemyScript enemy = collision.gameObject.GetComponent<EnemyScript>();
            enemy.takeDamage(damage, Elements.NEUTRAL);
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Unhitable"))
            return;
        base.OnCollisionEnter2D(collision);
    }

    void Update()
    {
        if (target != null && timer >= 2)
        {
            Vector3 targetDir = target.position - transform.position;

            float angle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        timer += Time.deltaTime;
        transform.Translate(Vector3.right * Time.deltaTime * AbilityStats.Neutral.ability2.movementForce);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.CompareTo("Enemy") == 0)
        {
            target = collision.transform;
        }
    }

    private void destroyClone()
    {
        Destroy(gameObject);
    }

    public override void applyMovement()
    {
        // Rotates the missile randomly
        int result = Random.Range(0, 360);
        transform.Rotate(new Vector3(0, 0, result));
    }
}
