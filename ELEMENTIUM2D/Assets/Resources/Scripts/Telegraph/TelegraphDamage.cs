using UnityEngine;
using System.Collections;

namespace TelegraphEffect
{
    [System.Serializable]
    public abstract class TelegraphDamage
    {
        public abstract void damage(Transform entity, float damage);
    }
}
