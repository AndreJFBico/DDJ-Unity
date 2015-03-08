using UnityEngine;
using System.Collections;

namespace Includes
{
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
                public static float movementForce = 200;
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
                public static float movementForce = 50;
                public static string sprite = "Prefabs/Projectiles/";
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
                public static float movementForce = 200;
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
                public static int projectile_number = 1;
                public static float damage = 30;
                public static float movementForce = 200;
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
                public static float attackSpeed = 0.10f;
                public static int projectile_number = 1;
                public static float damage = 5;
                public static float movementForce = 200;
                public static string sprite = "Prefabs/Projectiles/";
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
    public enum Elements { NEUTRAL, FIRE, EARTH, FROST };
}
