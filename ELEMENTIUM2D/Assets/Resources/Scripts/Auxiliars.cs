using UnityEngine;
using System.Collections;

namespace Includes
{
    public class Cooldowns
    {
        // NEUTRAL
        public class Neutral
        {
            public static float ability1 = 0.10f;
            public static float ability2 = 0.25f;
            public static float ability3 = 0.25f;
        }

        // FROST
        public class Frost
        {
            public static float ability1 = 0.25f;
            public static float ability2 = 0.25f;
            public static float ability3 = 0.25f;
        }

        // FIRE
        public class Fire
        {
            public static float ability1 = 0.25f;
            public static float ability2 = 0.25f;
            public static float ability3 = 0.25f;
        }

        // EARTH
        public class Earth
        {
            public static float ability1 = 0.25f;
            public static float ability2 = 0.25f;
            public static float ability3 = 0.25f;
        }
    }
    public class ProjectileStats
    {
        // NEUTRAL
        public class NeutralBlast
        {
            public static float damage = 5;
            public static float movementForce = 200;
        }

        // FIRE
        public class Fireball
        {
            public static float damage = 30;
            public static float movementForce = 200;
        }

        // WATER
        public class Iceshard
        {
            public static float damage = 10;
            public static float movementForce = 200;
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
