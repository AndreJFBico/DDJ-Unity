using UnityEngine;
using System.Collections;
using TelegraphEffect;

namespace TelegraphEffect
{
    [System.Serializable]
    public class ParentWithEnemy : TelegraphInit
    {
        public override void init(Transform entity, Transform firePointTransform, Transform telegraphTransform)
        {
            //telegraphTransform.SetParent(entity);
            telegraphTransform.gameObject.SetActive(true);
            telegraphTransform.LookAt(entity);     
        }
    }
}