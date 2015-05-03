using UnityEngine;
using System.Collections;
using TelegraphEffect;

namespace TelegraphEffect
{
    [System.Serializable]
    public class InstantDamageWithCooldown : TelegraphDamage
    {
        public override void damage(Transform entity, float damage)
        {
            entity.GetComponent<Agent>().takeDamage(damage, Includes.Elements.FIRE, true);
        }
    }
}
