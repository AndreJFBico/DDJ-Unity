
#define DEBUG

using UnityEngine;
using System.Collections;


namespace Includes
{
    public enum Elements { NEUTRAL, FIRE, EARTH, FROST };
    public enum BreakableWalls { NEUTRAL, FIRE, EARTH, FROST};
    public enum StatusEffects { BURNING, SLOW, STUN}
    
    public class Constants
    {
        public const string breakable = "Breakable";
        public const string elementalyModifiable = "ElementalyModifiable";
        public const float enemyRoamRadius = 2.0f;
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
                public static float attackSpeed = 2.01f;
                public static int projectile_number = 1;
                public static int child_projectile_number = 20;
                public static float damage = 5;
                public static float movementForce = 20;
                public static string sprite = "Prefabs/Projectiles/WaterBurst";
                public static string childSprite = "Prefabs/Projectiles/WaterProjectile";
                public static float deathTimer = 5;
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
                public static string sprite = "Prefabs/Projectiles/EarthShield";
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
            public static float visionRadius = 5.46f;
            public static float rangedRadius = 1.5f;
            public static float rangedAttackSpeed = 0.5f;
            // This has to be more specific there might by more neutral projectiles, and the damage a projectile does is different from a basic neutral enemy
            public static string neutralEnemyProjectile = "Prefabs/Projectiles/Musk";
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
            public static float visionRadius = 5.46f;
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
            public static float visionRadius = 5.46f;
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
            public static float visionRadius = 5.46f;
        }

        // NEUTRAL
        public class NeutralShielded
        {
            public static float maxHealth = 50;
            public static float damage = 10;
            public static float defence = 2;
            public static float waterResist = 5;
            public static float earthResist = 5;
            public static float fireResist = 5;
            public static float visionRadius = 5.46f;
            public static float rangedRadius = 1.5f;
            public static float rangedAttackSpeed = 0.5f;
            // This has to be more specific there might by more neutral projectiles, and the damage a projectile does is different from a basic neutral enemy
            public static string neutralEnemyProjectile = "Prefabs/Projectiles/Musk";

            public static bool shielded = true;
            public static Elements shieldType = Elements.FROST;
            public static float shieldHP = 40;
        }
    }

    // STARTING STATS
    public class PlayerStats
    {
        public static float moveSpeed = 2.5f;
        public static float moveInContactWithEnemy = 1.0f;
        public static float maxHealth = 50;
        public static float damage = 3;
        public static float defence = 0;
        public static float waterResist = 0;
        public static float earthResist = 0;
        public static float fireResist = 0;
        public static float damageTimer = 2.35f;
    }
}
