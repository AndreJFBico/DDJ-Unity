using UnityEngine;
using System.Collections;

namespace TelegraphEffect
{
    [System.Serializable]
    public abstract class TelegraphInit
    {
        public abstract void init(Transform entity, Transform firePointTransform, Transform telegraphTransform);
    }
}


