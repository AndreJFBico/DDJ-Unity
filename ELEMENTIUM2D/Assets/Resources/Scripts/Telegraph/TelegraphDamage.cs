using UnityEngine;
using System.Collections;

namespace TelegraphEffect
{
    [System.Serializable]
    public abstract class TelegraphDamage
    {
        public bool deltDamage = false;
        public abstract void damage(Transform entity, float damage);
    }
}
