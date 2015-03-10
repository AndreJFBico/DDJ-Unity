
#define DEBUG

using UnityEngine;
using System.Collections;


namespace Includes
{
    public enum Elements { NEUTRAL, FIRE, EARTH, FROST };
    public enum BreakableWalls { NEUTRAL, FIRE, EARTH, FROST};
    
    public class Constants
    {
        public const string breakable = "Breakable";
        public const string elementalyModifiable = "ElementalyModifiable";
    }
    
    public class AbilityStats
    {
        // NEUTRAL
        public class Neutral
        {
            public class ability1
            {
                public static float attackSpeed = 0.10f;
                public static int projectile_number = 1;
                public static float damage = 5;
                public static float movementForce = 10;
                public static string sprite = "Prefabs/Projectiles/NeutralBlast";
            }

            public class ability2
            {
                public static float attackSpeed = 0.25f;
                public static int projectile_number = 10;
                public static float damage = 5;
                public static float movementForce = 1.5f;
                public static string sprite = "Prefabs/Projectiles/NeutralMissile";
            }
            public class ability3
            {
                public static float attackSpeed = 0.25f;
                public static int projectile_number = 1;
                public static float damage = 5;
                public static float movementForce = 2.5f;
                public static string sprite = "Prefabs/Projectiles/NeutralBouncer";
                public static int splitNumber = 6;
                public static float negativeSplitAngle = -30;
                public static float positiveSplitAngle = 30;
                public static int numSplits = 1;
            }
        }

        // FROST
        public class Frost
        {
            public class ability1
            {
                public static float attackSpeed = 0.25f;
                public static int projectile_number = 1;
                public static float damage = 10;
                public static float movementForce = 5;
                public static string sprite = "Prefabs/Projectiles/FrostBolt";
            }

            public class ability2
            {
                public static float attackSpeed = 0.25f;
                public static int projectile_number = 1;
                public static float damage = 5;
                public static float movementForce = 200;
                public static string sprite = "Prefabs/Projectiles/";
            }
            public class ability3
            {
                public static float attackSpeed = 0.25f;
                public static int projectile_number = 1;
                public static float damage = 5;
                public static float movementForce = 200;
                public static string sprite = "Prefabs/Projectiles/";
            }
        }

        // FIRE
        public class Fire
        {
            public class ability1
            {
                public static float attackSpeed = 0.30f;
                public static int projectile_number = 4;
                public static float damage = 6;
                public static float maxForce = 20;
                public static float minForce = 5;
                public static string sprite = "Prefabs/Projectiles/Fireball";
            }

            public class ability2
            {
                public static float attackSpeed = 0.25f;
                public static int projectile_number = 1;
                public static float damage = 5;
                public static float movementForce = 200;
                public static string sprite = "Prefabs/Projectiles/";
            }
            public class ability3
            {
                public static float attackSpeed = 0.25f;
                public static int projectile_number = 1;
                public static float damage = 5;
                public static float movementForce = 200;
                public static string sprite = "Prefabs/Projectiles/";
            }
        }

        // EARTH
        public class Earth
        {
            public class ability1
            {
                public static float attackSpeed = 0.50f;
                public static int projectile_number = 1;
                public static float damage = 20;
                public static float movementForce = 10;
                public static string sprite = "Prefabs/Projectiles/EarthDisk";
            }

            public class ability2
            {
                public static float attackSpeed = 0.25f;
                public static int projectile_number = 1;
                public static float damage = 5;
                public static float movementForce = 200;
                public static string sprite = "Prefabs/Projectiles/";
            }
            public class ability3
            {
                public static float attackSpeed = 0.25f;
                public static int projectile_number = 1;
                public static float damage = 5;
                public static float movementForce = 200;
                public static string sprite = "Prefabs/Projectiles/";
            }
        }
    }

    public class EnemyStats
    {
        // NEUTRAL
        public class Neutral
        {
            public static float maxHealth = 50;
            public static float damage = 10;
            public static float defence = 2;
            public static float waterResist = 5;
            public static float earthResist = 5;
            public static float fireResist = 5;
        }

        // Frost
        public class Frost
        {
            public static float maxHealth = 100;
            public static float damage = 10;
            public static float defence = 2;
            public static float waterResist = 5;
            public static float earthResist = 5;
            public static float fireResist = 5;
        }

        // FIRE
        public class Fire
        {
            public static float maxHealth = 100;
            public static float damage = 10;
            public static float defence = 2;
            public static float waterResist = 5;
            public static float earthResist = 5;
            public static float fireResist = 5;
        }

        // EARTH
        public class Earth
        {
            public static float maxHealth = 100;
            public static float damage = 10;
            public static float defence = 2;
            public static float waterResist = 5;
            public static float earthResist = 5;
            public static float fireResist = 5;
        }
    }
}
