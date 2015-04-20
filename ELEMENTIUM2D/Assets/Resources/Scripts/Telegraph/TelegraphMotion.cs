using UnityEngine;
using System.Collections;

namespace TelegraphEffect
{
    [System.Serializable]
    public abstract class TelegraphMotion
    {
        public abstract void applyMotion(Transform tofollow, Transform entity);
    }
}
