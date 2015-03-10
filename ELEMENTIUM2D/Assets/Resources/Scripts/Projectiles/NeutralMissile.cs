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
        type = Elements.NEUTRAL;
    }

    public override void OnCollisionEnter(Collision collision)
    {
        if (collidedWith(collision, damage)) ;
        else if (collidedWithBreakable(collision)) ;
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Unhitable"))
            return;
        base.OnCollisionEnter(collision);
    }

    void Update()
    {
        if (target != null && timer >= 1)
        {
            Vector3 targetDir = target.position - transform.position;

            float angle = Mathf.Atan2(targetDir.x, targetDir.z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.up);
        }
        timer += Time.deltaTime;
        transform.Translate(Vector3.forward * Time.deltaTime * AbilityStats.Neutral.ability2.movementForce);
    }

    public void OnTriggerEnter(Collider collision)
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
        transform.Rotate(new Vector3(0.0f, result, 0.0f));
    }
}
