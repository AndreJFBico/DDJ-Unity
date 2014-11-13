using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour {

    private bool firing = false;
    private GameObject bulletPrefab;
    public float attackSpeed = 1f;
    private PlayerStats playerStats;

	// Use this for initialization
	void Start () {
        playerStats = (GameObject.FindWithTag("Player") as GameObject).GetComponent<PlayerStats>();
        bulletPrefab = Resources.Load("Prefab/Bullet") as GameObject;
        StartCoroutine("shooting");
	}
	
    private IEnumerator shooting()
    {
        while (true)
        {
            if (Input.GetMouseButton(0))
            {
                GameObject bullet = (GameObject)Instantiate(bulletPrefab, transform.position + transform.forward, Quaternion.identity);
                bullet.rigidbody.AddForce(transform.forward * 1000);
                yield return new WaitForSeconds(attackSpeed);
            }
            yield return new WaitForFixedUpdate();
        }
    }

	// Update is called once per frame
	void Update () {
	    
	}
}
