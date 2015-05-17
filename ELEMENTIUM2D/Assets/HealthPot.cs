using UnityEngine;
using System.Collections;
using Includes;

public class HealthPot : MonoBehaviour {

    private float _amount = 1;

    void OnTriggerEnter(Collider other)
    {
        if(other.tag.CompareTo("Player") == 0)
        {
            GameManager.Instance.Player.healSelf(_amount, Elements.NEUTRAL);
            Destroy(gameObject);
        }
    }
	
    public void init(float amount)
    {
        _amount = amount;
    }

}
