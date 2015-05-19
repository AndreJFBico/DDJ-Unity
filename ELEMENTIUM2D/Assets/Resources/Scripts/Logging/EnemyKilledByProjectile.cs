using UnityEngine;
using System.Collections;
using Includes;

namespace Logging
{
    public class EnemyKilledByProjectile
    {
        public EnemyKilledByProjectile(string eN, Elements el, string pn, Elements pty)
        {
            enemyName = eN;
            enemyType = el;
            projectileName = pn;
            projectileType = pty;
            amount = 1;
        }

        public string enemyName;
        public Elements enemyType;
        public string projectileName;
        public Elements projectileType;
        public int amount;
    }
}
