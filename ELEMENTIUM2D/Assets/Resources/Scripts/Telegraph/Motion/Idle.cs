using UnityEngine;
using System.Collections;
using TelegraphEffect;

namespace TelegraphEffect
{
    [System.Serializable]
    public class Idle : TelegraphMotion
    {

        public override void applyMotion(Transform tofollow, Transform entity)
        {
            entity.LookAt(tofollow);
        }
    }
}
