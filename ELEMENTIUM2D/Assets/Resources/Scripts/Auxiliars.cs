
#define DEBUG
using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.ComponentModel;
using System.IO;

namespace Includes
{
    public enum Elements { NEUTRAL, FIRE, EARTH, WATER };
    public enum BreakableWalls { NEUTRAL, FIRE, EARTH, FROST};
    public enum StatusEffects { BURNING, SLOW, STUN, WET};

    public enum MathOperations { SUM, MUL, SET, DEFENCE, MAXHP};

    #region Constants
    public class Constants
    {
        public const string breakable = "Breakable";
        public const string elementalyModifiable = "ElementalyModifiable";
        public const string obstacles = "Obstacles";
        public static int playerProjectileLayer = LayerMask.GetMask("PlayerProjectile");
        public static int enemyProjectileLayer = LayerMask.GetMask("EnemyProjectile");
        public const float enemyRoamRadius = 2.0f;
    }
    #endregion

    #region StaticMethods
    public static class Methods
    {
        public static string GetEnumDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes = 
                (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }
    } 
    #endregion

    #region Ability Stats
    public class AbilityStats
    {
        public static void reset()
        {
            Neutral.reset();
            Frost.reset();
            Fire.reset();
            Earth.reset();
        }

        #region Neutral Abilities
        // NEUTRAL
        public class Neutral
        {
            public static void reset()
            {
                ability1.reset();
                ability2.reset();
                ability3.reset();
            }

            public class ability1
            {
                private const float def_attackSpeed = 0.25f;
                private const int def_projectile_number = 1;
                private const float def_damage = 2.5f;
                private const float def_movementForce = 15;

                public static float attackSpeed = def_attackSpeed;
                public static int projectile_number = def_projectile_number;
                public static float damage = def_damage;
                public static float movementForce = def_movementForce;
                public static string projectile = "Prefabs/Projectiles/NeutralBlast";

                public static float Damage { get { return (float)Math.Round(damage + GameManager.Instance.Stats.damage * 1f, MidpointRounding.AwayFromZero); } }
                public static float AttackSpeed { get { return attackSpeed + GameManager.Instance.Stats.attackSpeed * attackSpeed/10; } } //Player AttackSpeed is to be incremented by int numbers
            
                public static void reset()
                {
                    attackSpeed = def_attackSpeed;
                    projectile_number = def_projectile_number;
                    damage = def_damage;
                    movementForce = def_movementForce;
                    projectile = "Prefabs/Projectiles/NeutralBlast";
                }
            }

            public class ability2
            {
                private const float def_attackSpeed = 1f;
                private const int def_projectile_number = 10;
                private const float def_damage = 0.5f;
                private const float def_movementForce = 1.5f;

                public static float attackSpeed = def_attackSpeed;
                public static int projectile_number = def_projectile_number;
                public static float damage = def_damage;
                public static float movementForce = def_movementForce;
                public static string projectile = "Prefabs/Projectiles/NeutralMissile";

                public static float Damage { get { return (float)Math.Round(damage + GameManager.Instance.Stats.damage * 0.75f, MidpointRounding.AwayFromZero); } }
                public static float AttackSpeed { get { return attackSpeed + GameManager.Instance.Stats.attackSpeed * attackSpeed / 10; } } //Player AttackSpeed is to be incremented by int numbers

                public static void reset()
                {
                    attackSpeed = def_attackSpeed;
                    projectile_number = def_projectile_number;
                    damage = def_damage;
                    movementForce = def_movementForce;
                    projectile = "Prefabs/Projectiles/NeutralMissile";
                }
            }
            public class ability3
            {
                public const float def_attackSpeed = 0.25f;
                public const int def_projectile_number = 1;
                public const float def_damage = 0.1f;
                public const float def_movementForce = 2.5f;
                public const int def_splitNumber = 6;
                public const float def_negativeSplitAngle = -30;
                public const float def_positiveSplitAngle = 30;
                public const int def_numSplits = 1;

                public static float attackSpeed = def_attackSpeed;
                public static int projectile_number = def_projectile_number;
                public static float damage = def_damage;
                public static float movementForce = def_movementForce;
                public static string projectile = "Prefabs/Projectiles/NeutralBouncer";
                public static int splitNumber = def_splitNumber;
                public static float negativeSplitAngle = def_negativeSplitAngle;
                public static float positiveSplitAngle = def_positiveSplitAngle;
                public static int numSplits = def_numSplits;

                public static float Damage { get { return (float)Math.Round(damage + GameManager.Instance.Stats.damage * 0.5f, MidpointRounding.AwayFromZero); } }
                public static float AttackSpeed { get { return attackSpeed + GameManager.Instance.Stats.attackSpeed * attackSpeed / 10; } } //Player AttackSpeed is to be incremented by int numbers

                public static void reset()
                {
                    attackSpeed = def_attackSpeed;
                    projectile_number = def_projectile_number;
                    damage = def_damage;
                    movementForce = def_movementForce;
                    projectile = "Prefabs/Projectiles/NeutralBouncer";
                    splitNumber = def_splitNumber;
                    negativeSplitAngle = def_negativeSplitAngle;
                    positiveSplitAngle = def_positiveSplitAngle;
                    numSplits = def_numSplits;
                }
            }
        }
        #endregion

        #region Frost abilities
        // FROST
        public class Frost
        {
            public static void reset()
            {
                ability1.reset();
                ability2.reset();
                ability3.reset();
            }

            public class ability1
            {
                public const float def_attackSpeed = 0.25f;
                public const int def_projectile_number = 1;
                public const float def_damage = 2.5f;
                public const float def_movementForce = 5;

                public static float attackSpeed = def_attackSpeed;
                public static int projectile_number = def_projectile_number;
                public static float damage = def_damage;
                public static float movementForce = def_movementForce;
                public static string projectile = "Prefabs/Projectiles/FrostBolt";

                public static float Damage { get { return (float)Math.Round(damage + GameManager.Instance.Stats.damage * 0.85f, MidpointRounding.AwayFromZero); } }
                public static float AttackSpeed { get { return attackSpeed + GameManager.Instance.Stats.attackSpeed * attackSpeed / 10; } } //Player AttackSpeed is to be incremented by int numbers
            
                public static void reset()
                {
                    attackSpeed = def_attackSpeed;
                    projectile_number = def_projectile_number;
                    damage = def_damage;
                    movementForce = def_movementForce;
                    projectile = "Prefabs/Projectiles/FrostBolt";
                }
            }

            public class ability2
            {
                public const float def_attackSpeed = 2f;
                public const int def_projectile_number = 1;
                public const int def_child_projectile_number = 20;
                public const float def_damage = 1;
                public const float def_movementForce = 20;
                public const float def_deathTimer = 5;

                public static float attackSpeed = def_attackSpeed;
                public static int projectile_number = def_projectile_number;
                public static int child_projectile_number = def_child_projectile_number;
                public static float damage = def_damage;
                public static float movementForce = def_movementForce;
                public static string projectile = "Prefabs/Projectiles/WaterBurst";
                public static string childProjectile = "Prefabs/Projectiles/WaterProjectile";
                public static float deathTimer = def_deathTimer;

                public static float Damage { get { return (float)Math.Round(damage + GameManager.Instance.Stats.damage * 0.25f, MidpointRounding.AwayFromZero); } }
                public static float AttackSpeed { get { return attackSpeed + GameManager.Instance.Stats.attackSpeed * attackSpeed / 10; } } //Player AttackSpeed is to be incremented by int numbers
            
                public static void reset()
                {
                    attackSpeed = def_attackSpeed;
                    projectile_number = def_projectile_number;
                    child_projectile_number = def_child_projectile_number;
                    damage = def_damage;
                    movementForce = def_movementForce;
                    deathTimer = def_deathTimer;
                    projectile = "Prefabs/Projectiles/WaterBurst";
                    childProjectile = "Prefabs/Projectiles/WaterProjectile";
                }
            }
            public class ability3
            {
                public const float def_attackSpeed = 1.5f;
                public const int def_projectile_number = 15;
                public const float def_damage = 1;
                public const float def_movementForce = 1.5f;

                public static float attackSpeed = def_attackSpeed;
                public static int projectile_number = def_projectile_number;
                public static float damage = def_damage;
                public static float movementForce = def_movementForce;
                public static string projectile = "Prefabs/Projectiles/IceNova";

                public static float Damage { get { return (float)Math.Round(damage + GameManager.Instance.Stats.damage * 0.5f, MidpointRounding.AwayFromZero); } }
                public static float AttackSpeed { get { return attackSpeed + GameManager.Instance.Stats.attackSpeed * attackSpeed / 10; } } //Player AttackSpeed is to be incremented by int numbers
                
                public static void reset()
                {
                    attackSpeed = def_attackSpeed;
                    projectile_number = def_projectile_number;
                    damage = def_damage;
                    movementForce = def_movementForce;
                    projectile = "Prefabs/Projectiles/IceNova";
                }
            }
        }
        #endregion

        #region Fire Abilities
        // FIRE
        public class Fire
        {
            public static void reset()
            {
                ability1.reset();
                ability2.reset();
                ability3.reset();
            }

            public class ability1
            {
                public const float def_attackSpeed = 0.30f;
                public const int def_projectile_number = 1;
                public const int def_collisionNumber = 3;
                public const float def_damage = 1;
                public const float def_maxForce = 20;
                public const float def_minForce = 5;

                public static float attackSpeed = def_attackSpeed;
                public static int projectile_number = def_projectile_number;
                public static int collisionNumber = def_collisionNumber;
                public static float damage = def_damage;
                public static float maxForce = def_maxForce;
                public static float minForce = def_minForce;
                public static string projectile = "Prefabs/Projectiles/Fireball";

                public static float Damage { get { return (float)Math.Round(damage + GameManager.Instance.Stats.damage * 1f, MidpointRounding.AwayFromZero); } }
                public static float AttackSpeed { get { return attackSpeed + GameManager.Instance.Stats.attackSpeed * attackSpeed / 10; } } //Player AttackSpeed is to be incremented by int numbers
            
                public static void reset()
                {
                    attackSpeed = def_attackSpeed;
                    projectile_number = def_projectile_number;
                    collisionNumber = def_collisionNumber;
                    damage = def_damage;
                    maxForce = def_maxForce;
                    minForce = def_minForce;
                    projectile = "Prefabs/Projectiles/Fireball";
                }
            }

            public class ability2
            {
                public const float def_attackSpeed = 0.25f;
                public const int def_projectile_number = 1;
                public const float def_damage = 1;
                public const float def_movementForce = 200;

                public static float attackSpeed = def_attackSpeed;
                public static int projectile_number = def_projectile_number;
                public static float damage = def_damage;
                public static float movementForce = def_movementForce;
                public static string projectile = "Prefabs/Projectiles/";

                public static float Damage { get { return (float)Math.Round(damage + GameManager.Instance.Stats.damage * 0.5f, MidpointRounding.AwayFromZero); } }
                public static float AttackSpeed { get { return attackSpeed + GameManager.Instance.Stats.attackSpeed * attackSpeed / 10; } } //Player AttackSpeed is to be incremented by int numbers
            
                public static void reset()
                {
                    attackSpeed = def_attackSpeed;
                    projectile_number = def_projectile_number;
                    damage = def_damage;
                    movementForce = def_movementForce;
                    projectile = "Prefabs/Projectiles/";
                }
            
            }
            public class ability3
            {
                public const float def_attackSpeed = 4.25f;
                public const int def_projectile_number = 1;
                public const float def_damage = 1;
                public const float def_movementForce = 200;
                public const string def_projectile = "Prefabs/Projectiles/FireHeal";
                public const float def_abilityTimer = 1.3f;

                public static float attackSpeed = def_attackSpeed;
                public static int projectile_number = def_projectile_number;
                public static float damage = def_damage;
                public static float movementForce = def_movementForce;
                public static string projectile = "Prefabs/Projectiles/FireHeal";
                public static float abilityTimer = def_abilityTimer;

                public static float Damage { get { return (float)Math.Round(damage + GameManager.Instance.Stats.damage * 1f, MidpointRounding.AwayFromZero); } }
                public static float AttackSpeed { get { return attackSpeed + GameManager.Instance.Stats.attackSpeed * attackSpeed / 10; } } //Player AttackSpeed is to be incremented by int numbers
            
                public static void reset()
                {
                    attackSpeed = def_attackSpeed;
                    projectile_number = def_projectile_number;
                    damage = def_damage;
                    movementForce = def_movementForce;
                    projectile = "Prefabs/Projectiles/FireHeal";
                    abilityTimer = def_abilityTimer;
                }
            }
        }
        #endregion

        #region Earth Abilities
        // EARTH
        public class Earth
        {
            public static void reset()
            {
                ability1.reset();
                ability2.reset();
                ability3.reset();
            }

            public class ability1
            {
                public const float def_attackSpeed = 1f;
                public const int def_projectile_number = 1;
                public const float def_damage = 6;
                public const float def_movementForce = 10;

                public static float attackSpeed = def_attackSpeed;
                public static int projectile_number = def_projectile_number;
                public static float damage = def_damage;
                public static float movementForce = def_movementForce;
                public static string projectile = "Prefabs/Projectiles/EarthDisk";

                public static float Damage { get { return (float)Math.Round(damage + GameManager.Instance.Stats.damage * 1.5f, MidpointRounding.AwayFromZero); } }
                public static float AttackSpeed { get { return attackSpeed + GameManager.Instance.Stats.attackSpeed * attackSpeed / 10; } } //Player AttackSpeed is to be incremented by int numbers
            
                public static void reset()
                {
                    attackSpeed = def_attackSpeed;
                    projectile_number = def_projectile_number;
                    damage = def_damage;
                    movementForce = def_movementForce;
                    projectile = "Prefabs/Projectiles/EarthDisk";
                }
            }

            public class ability2
            {
                public const float def_attackSpeed = 4f;
                public const int def_projectile_number = 1;
                public const float def_damage = 1;
                public const float def_movementForce = 5;
                public const float def_abilityTimer = 4.0f;

                public static float attackSpeed = def_attackSpeed;
                public static int projectile_number = def_projectile_number;
                public static float damage = def_damage;
                public static float movementForce = def_movementForce;
                public static float abilityTimer = def_abilityTimer;
                public static string projectile = "Prefabs/Projectiles/EarthShield";

                public static float Damage { get { return (float)Math.Round(damage + GameManager.Instance.Stats.damage * 1.5f, MidpointRounding.AwayFromZero); } }
                public static float AttackSpeed { get { return attackSpeed + GameManager.Instance.Stats.attackSpeed * attackSpeed / 10; } } //Player AttackSpeed is to be incremented by int numbers

                public static void reset()
                {
                    attackSpeed = def_attackSpeed;
                    projectile_number = def_projectile_number;
                    damage = def_damage;
                    movementForce = def_movementForce;
                    abilityTimer = def_abilityTimer;
                    projectile = "Prefabs/Projectiles/EarthShield";
                }
            }
            public class ability3
            {
                public const float def_attackSpeed = 0.5f;
                public const int def_projectile_number = 1;
                public const float def_damage = 3;
                public const float def_movementForce = 200;

                public static float attackSpeed = def_attackSpeed;
                public static int projectile_number = def_projectile_number;
                public static float damage = def_damage;
                public static float movementForce = def_movementForce;
                public static string projectile = "Prefabs/Projectiles/EarthStun";

                public static float Damage { get { return (float)Math.Round(damage + GameManager.Instance.Stats.damage * 1.5f, MidpointRounding.AwayFromZero); } }
                public static float AttackSpeed { get { return attackSpeed + GameManager.Instance.Stats.attackSpeed * attackSpeed / 10; } } //Player AttackSpeed is to be incremented by int numbers

                public static void reset()
                {
                    attackSpeed = def_attackSpeed;
                    projectile_number = def_projectile_number;
                    damage = def_damage;
                    movementForce = def_movementForce;
                    projectile = "Prefabs/Projectiles/EarthStun";
                }
            }
        }
        #endregion
    }
    #endregion

    #region Enemy Stats
    public class EnemyStats
    {
        // NEUTRAL
        #region Neutral Enemies

        public class Spawner
        {
            public static float maxHealth = 20;
            public static float damage = 0;
            public static float defence = 50;
            public static float waterResist = 50;
            public static float earthResist = 50;
            public static float fireResist = 50;
            public static float visionRadius = 1.65f;
            public static float unalertedSpeed = 0;
            public static float alertedSpeed = 0;
        }

        public class BasicTelegraph
        {
            public static float maxHealth = 40;
            public static float damage = 5;
            public static float defence = 0;
            public static float waterResist = 0;
            public static float earthResist = 0;
            public static float fireResist = 0;
            public static float visionRadius = 1.65f;
            public static float unalertedSpeed = 0.5f;
            public static float alertedSpeed = 1.5f;
        }

        public class BasicNeutral
        {
            public static float maxHealth = 20;
            public static float damage = 5;
            public static float defence = 0;
            public static float waterResist = 0;
            public static float earthResist = 0;
            public static float fireResist = 0;
            public static float visionRadius = 1.65f;
            public static float unalertedSpeed = 0.5f;
            public static float alertedSpeed = 1.5f;
        }

        public class RangedNeutral
        {
            public static float maxHealth = 20;
            public static float damage = 5;
            public static float defence = 0;
            public static float waterResist = 0;
            public static float earthResist = 0;
            public static float fireResist = 0;
            public static float visionRadius = 2.2f;
            public static float rangedRadius = 1.8f;
            public static float rangedAttackSpeed = 0.5f;
            public static float movementForce = 5f;
            public static string neutralEnemyProjectile = "Prefabs/Projectiles/Enemy/Musk";
            public static float unalertedSpeed = 0.5f;
            public static float alertedSpeed = 1.0f;
        }

        public class HealerNeutral
        {
            public static float maxHealth = 20;
            public static float healAmount = 1;
            public static float damage = 2;
            public static float defence = 0;
            public static float waterResist = 0;
            public static float earthResist = 0;
            public static float fireResist = 0;
            public static float visionRadius = 1.65f;
            public static float rangedRadius = 1.5f;
            public static float rangedAttackSpeed = 0.5f;
            public static string neutralEnemyProjectile = "Prefabs/Projectiles/Enemy/Healing";
            public static float unalertedSpeed = 0.5f;
            public static float alertedSpeed = 1.8f;
        }

        public class NeutralShield
        {
            public static float maxHealth = 20;
            public static float damage = 5;
            public static float defence = 2;
            public static float waterResist = 5;
            public static float earthResist = 5;
            public static float fireResist = 5;
            public static float visionRadius = 1.65f;
            public static float rangedRadius = 1.5f;
            public static float rangedAttackSpeed = 0.5f;
            // This has to be more specific there might by more neutral projectiles, and the damage a projectile does is different from a basic neutral enemy
            public static string neutralEnemyProjectile = "Prefabs/Projectiles/Enemy/Musk";

            public static bool shielded = true;
            public static Elements shieldType = Elements.WATER;
            public static float shieldHP = 40;
        }
        #endregion

        // Frost
        #region Frost/Water Enemies
        public class WaterBasic
        {
            public static float maxHealth = 20;
            public static float damage = 5;
            public static float defence = 0;
            public static float waterResist = 80;
            public static float earthResist = -50;
            public static float fireResist = 0;
            public static float visionRadius = 1.65f;
            public static Elements type = Elements.WATER;
            public static float statusDurability = 5;
            public static float statusIntensity = 1f;
            public static float unalertedSpeed = 0.5f;
            public static float alertedSpeed = 1.5f;
        }
        #endregion

        // FIRE
        #region Fire Enemies
        public class FireBasic
        {
            public static float maxHealth = 20;
            public static float damage = 5;
            public static float defence = 0;
            public static float waterResist = -50;
            public static float earthResist = 0;
            public static float fireResist = 80;
            public static float visionRadius = 1.65f;
            public static Elements type = Elements.FIRE;
            public static float statusDurability = 5;
            public static float statusIntensity = 5f;
            public static float unalertedSpeed = 0.5f;
            public static float alertedSpeed = 1.5f;
        }

        public class FireRanged
        {
            public static float maxHealth = 20;
            public static float damage = 5;
            public static float defence = 0;
            public static float waterResist = -50;
            public static float earthResist = 0;
            public static float fireResist = 80;
            public static float visionRadius = 2.2f;
            public static float rangedRadius = 1.8f;
            public static float rangedAttackSpeed = 0.5f;
            public static float movementForce = 5f;
            public static string neutralEnemyProjectile = "Prefabs/Projectiles/Enemy/EnemyFireball";
            public static float unalertedSpeed = 0.5f;
            public static float alertedSpeed = 1.0f;
        }
        #endregion

        // EARTH
        #region Earth Enemies
        public class EarthBasic
        {
            public static float maxHealth = 70;
            public static float damage = 15;
            public static float defence = 0;
            public static float waterResist = 0;
            public static float earthResist = 80;
            public static float fireResist = -50;
            public static float visionRadius = 1.65f;
            public static Elements type = Elements.EARTH;
            public static float statusDurability = 3;
            public static float statusIntensity = 0.35f;
            public static float unalertedSpeed = 0.35f;
            public static float alertedSpeed = .9f;
        }

        public class EarthRanged
        {
            public static float maxHealth = 20;
            public static float damage = 10;
            public static float defence = 2;
            public static float waterResist = 5;
            public static float earthResist = 5;
            public static float fireResist = 5;
            public static float visionRadius = 2f;
            public static Elements type = Elements.EARTH;
            public static float unalertedSpeed = 0.5f;
            public static float alertedSpeed = 1.5f;
        }
        #endregion
    }
    #endregion

    // STARTING STATS
    #region PlayerStats
    [Serializable]
    public class PlayerStats
    {
        public const float def_damage = 5;
        public const float def_attackSpeed = 0;//Player AttackSpeed is to be incremented by int numbers

        public const float def_maxHealth = 30.0f;
        public const float def_defence = 0;
        public const float def_multiplierTimer = 4;

        public const float def_primary_neutral_level = 1;
        public const float def_secondary_neutral_level = 0;
        public const float def_terciary_neutral_level = 0;

        public const float def_primary_earth_level = 0;
        public const float def_secondary_earth_level = 0;
        public const float def_terciary_earth_level = 0;

        public const float def_primary_fire_level = 0;
        public const float def_secondary_fire_level = 0;
        public const float def_terciary_fire_level = 0;

        public const float def_primary_water_level = 0;
        public const float def_secondary_water_level = 0;
        public const float def_terciary_water_level = 0;

        public const float def_stamina = 10;
        public const float def_killTimer = 8;
        public const float def_hitTimer = 3;
        
        // VARIABLES, these can be changed and reset at will they represent the current player stats
        public float moveSpeed = 2f;
        public float moveInContactWithEnemy = 1f;
        public float maxHealth = def_maxHealth;
        public float health = def_maxHealth;
        public float damage = def_damage;
        public float attackSpeed = def_attackSpeed; //Player AttackSpeed is to be incremented by int numbers
        public float defence = def_defence;

        public float waterResist = 0;
        public float earthResist = 0;
        public float fireResist = 0;
        public float damageTimer = 1.5f;
        public float multiplierTimer = def_multiplierTimer;
        public int currentMultiplier = 0;
        public int[] multiplierLevels = { 15, 35, 70 };


        public float primary_neutral_level = def_primary_neutral_level;
        public float secondary_neutral_level = def_secondary_neutral_level;
        public float terciary_neutral_level = def_terciary_neutral_level;

        public float primary_earth_level = def_primary_earth_level;
        public float secondary_earth_level = def_secondary_earth_level;
        public float terciary_earth_level = def_terciary_earth_level;

        public float primary_fire_level = def_primary_fire_level;
        public float secondary_fire_level = def_secondary_fire_level;
        public float terciary_fire_level = def_terciary_fire_level;

        public float primary_water_level = def_primary_water_level;
        public float secondary_water_level = def_secondary_water_level;
        public float terciary_water_level = def_terciary_water_level;

        // LIMITS, these will never be reset
        public float lim_moveSpeed = float.MaxValue;
        public float lim_moveInContactWithEnemy = float.MaxValue;
        public float lim_maxHealth = float.MaxValue;
        public float lim_damage = float.MaxValue;
        public float lim_defence = float.MaxValue;
        public float lim_waterResist = float.MaxValue;
        public float lim_earthResist = float.MaxValue;
        public float lim_fireResist = float.MaxValue;
        public float lim_damageTimer = float.MaxValue;
        public float lim_stamina = float.MaxValue;
        public float lim_maxStamina = float.MaxValue;

        public float lim_primary_neutral_level = 1;
        public float lim_secondary_neutral_level = 0;
        public float lim_terciary_neutral_level = 0;

        public float lim_primary_earth_level = 0;
        public float lim_secondary_earth_level = 0;
        public float lim_terciary_earth_level = 0;

        public float lim_primary_fire_level = 1;
        public float lim_secondary_fire_level = 0;
        public float lim_terciary_fire_level = 0;

        public float lim_primary_water_level = 1;
        public float lim_secondary_water_level = 1;
        public float lim_terciary_water_level = 1;

        public float lim_points = 4;

        public float depth = 10;
        public float stamina = def_stamina;
        public float maxStamina = def_stamina;

        public float maxKillTimer = def_killTimer;
        public float maxHitTimer = def_hitTimer;

        public float killTimer = def_killTimer;
        public float hitTimer = def_hitTimer;

        public bool inCombat = false;
        public float maxInCombatTimer = 3;
        public float inCombatTimer = 3;

        //ATTENTION IF YOU ADD A NEW VARIABLE PLS DONT FORGET TO ADD IT TO RESET!!!!!!!
        public void reset()
        {
            moveSpeed = 2f;
            moveInContactWithEnemy = 1f;
            maxHealth = def_maxHealth;
            health = def_maxHealth;
            damage = def_damage;
            attackSpeed = def_attackSpeed;
            defence = def_defence;
            waterResist = 0;
            earthResist = 0;
            fireResist = 0;
            damageTimer = 1.5f;
            multiplierTimer = def_multiplierTimer;
            multiplierLevels = new int[]{ 11, 24, 45};
            currentMultiplier = 0;

            stamina = def_stamina;
            maxStamina = def_stamina;

            maxKillTimer = def_killTimer;
            maxHitTimer = def_hitTimer;
            
            killTimer = def_killTimer;
            hitTimer = def_hitTimer;
        
            inCombat = false;

            maxInCombatTimer = 3;
            inCombatTimer = 3;
            
            primary_neutral_level = def_primary_neutral_level;
            secondary_neutral_level = def_secondary_neutral_level;
            terciary_neutral_level = def_terciary_neutral_level;

            primary_earth_level = def_primary_earth_level;
            secondary_earth_level = def_secondary_earth_level;
            terciary_earth_level = def_terciary_earth_level;

            primary_fire_level = def_primary_fire_level;
            secondary_fire_level = def_secondary_fire_level;
            terciary_fire_level = def_terciary_fire_level;

            primary_water_level = def_primary_water_level;
            secondary_water_level = def_secondary_water_level;
            terciary_water_level = def_terciary_water_level;
        }

        public void dumpStats()
        {
            Debug.Log(
                moveSpeed +"\n"
                + moveInContactWithEnemy + "\n"

                + maxHealth + "\n"
                + stamina + "\n"
                + damage + "\n"
                + attackSpeed + "\n"
                + defence + "\n"

                + waterResist + "\n"
                + earthResist + "\n"
                + fireResist + "\n"

                + damageTimer + "\n"

                + lim_moveSpeed + "\n"
                + lim_moveInContactWithEnemy + "\n"
                + lim_maxHealth + "\n"
                + lim_damage + "\n"
                
                + lim_defence + "\n"
                + lim_waterResist + "\n"
                + lim_earthResist + "\n"
                + lim_fireResist + "\n"
                + lim_damageTimer + "\n"
                
                );
        }

        public static int multiplierLevelIndex()
        {
            int index = 0;
            for (int i = 0; i < GameManager.Instance.Stats.multiplierLevels.Length; i++)
            {
                if (GameManager.Instance.Stats.currentMultiplier > GameManager.Instance.Stats.multiplierLevels[i])
                {
                    index = i + 1;
                }
            }
            return index;
        }

        public static void setPlayerInCombat()
        {
            GameManager.Instance.Stats.inCombatTimer = GameManager.Instance.Stats.maxInCombatTimer;
            if(!GameManager.Instance.Stats.inCombat)
            {
                GameManager.Instance.Stats.inCombat = true;
                GameManager.Instance.Player.setPlayerInCombat();
            }
        }

    }
    #endregion

    #region Game Manager
    [Serializable]
    public class GameManager
    {
        private static GameManager _instance = null;
		private static bool firstGeneration = true;
		private static Dictionary<string, List<string>> dropGroups;
        private static GameObject player;
        private static DungeonRoom playerRoom;
        private static GameObject gui;

        private static GameObject iceWall;
        private static GameObject waterPuddle;
        private static GameObject oilPuddle;
        private static Sprite unknownSymbol;
        private static PlayerStats playerStats;
        private static List<string> statNames;
        private static ShootElement currentElement;

        private static Sprite neutralElement;
        private static Sprite fireElement;
        private static Sprite earthElement;
        private static Sprite waterElement;

        private static GameObject[] neutralEnemies;

        protected GameManager() { init(); }
        // Singleton pattern implementation

        public GameObject IceWall { get { return iceWall; } }

        public GameObject WaterPuddle { get { return waterPuddle; } }

        public GameObject OilPuddle { get { return oilPuddle; } }

        public Player Player { get { return player.GetComponent<Player>(); } }

        public DungeonRoom PlayerRoom { get { return playerRoom; } set { playerRoom = value; } }

        public GameObject GUI { get { return gui; } }

        public Sprite UnknownSymbol { get { return unknownSymbol; } }

        public PlayerStats Stats { get { return playerStats; } }

        public List<string> StatNames { get { return statNames; } }

        public Sprite FireElement { get { return fireElement; } }

        public Sprite EarthElement { get { return earthElement; } }

        public Sprite WaterElement { get { return waterElement; } }

        public Sprite NeutralElement { get { return neutralElement; } }

        public ShootElement CurrentElement { get { return currentElement; } set { currentElement = value; } }

        public static GameManager Instance { get { if (_instance == null) { _instance = new GameManager(); playerStats = new PlayerStats(); } return _instance; } }

		public bool FirstGeneration { get {return firstGeneration; } set { firstGeneration = value; } }

		public Dictionary<string, List<string>> DropGroups { get { return dropGroups;} set { dropGroups = value;} }

        private void init()
        {
            unknownSymbol = Resources.Load<Sprite>("GUIImages/Elements/round_unknown");
            iceWall = (GameObject)Resources.Load("Prefabs/Environment/AbilityCreated/IceWall");
            waterPuddle = (GameObject)Resources.Load("Prefabs/Environment/AbilityCreated/WaterPuddle");
            oilPuddle = (GameObject)Resources.Load("Prefabs/Environment/AbilityCreated/OilPuddle");
            neutralElement = Resources.Load<Sprite>("GUIImages/Elements/Neutral");
            fireElement = Resources.Load<Sprite>("GUIImages/Elements/Fire");
            earthElement = Resources.Load<Sprite>("GUIImages/Elements/Earth");
            waterElement = Resources.Load<Sprite>("GUIImages/Elements/Frost");
            neutralEnemies = Resources.LoadAll("Prefabs/Enemies/Neutral") as GameObject[];

            // Obtains all variable names within player stats
            statNames = new List<string>();
            Type type = typeof(PlayerStats); // Get type pointer
            FieldInfo[] fields = type.GetFields(); // Obtain all fields
            foreach (var field in fields) // Loop through fields
            {
                // Const
                if(!field.IsLiteral)
                {
                    if(!field.Name.Contains("lim"))
                    {
                        statNames.Add(field.Name);
                    }
                }
            }
        }

        public float getStatVariable(string field)
        {
            var ob = playerStats;
            var typ = typeof(PlayerStats);
            var f = typ.GetField(field);
            return (float)f.GetValue(ob);
        }

        void setValue(FieldInfo info, object obj, MathOperations operation, float value)
        {
            string name = info.FieldType.Name;
            if(info.FieldType.Name.CompareTo("Int32") == 0)
            {
                Debug.Log("previous value: " + (int)info.GetValue(null));
                int val = 0;
                val = (int)info.GetValue(null);
                switch (operation)
                {
                    case MathOperations.SUM:
                        info.SetValue(null, (int)value + val);
                        break;
                    case MathOperations.MUL:
                        info.SetValue(null, val + (int)value * val);
                        break;
                    case MathOperations.SET:
                        info.SetValue(null, (int)value);
                        break;
                }
                Debug.Log("after value: " + (int)info.GetValue(null));
            }
            else if (info.FieldType.Name.CompareTo("Single") == 0)
            {
                Debug.Log("previous value: " + (float)info.GetValue(null));
                var val = 0.0f;
                val = (float)info.GetValue(null);
                switch (operation)
                {
                    case MathOperations.SUM:
                        info.SetValue(null, value + val);
                        break;
                    case MathOperations.MUL:
                        info.SetValue(null, val + value * val);
                        break;
                    case MathOperations.SET:
                        info.SetValue(null, value);
                        break;
                }
                Debug.Log("after value: " + (float)info.GetValue(null));
            }
            else return;
          
           // Debug.Log("after value: " + (float)info.GetValue(null));
        }

        public float changeAbilityVariable(string field, float value, MathOperations operation)
        {
            char[] delimiterChars = { '_' };

            string[] splitedString = field.Split(delimiterChars);
            string variableName = splitedString[2];
            if(splitedString.Length > 3)
            {
                for (int i = 3; i < splitedString.Length; i++ )
                {
                    variableName += "_" + splitedString[i];
                }
            }
            #region Neutral
            if (field.Contains("Neutral"))
            {
                if(field.Contains("ability1"))
                {
                    var type = typeof(AbilityStats.Neutral.ability1);
                    var f = type.GetField(variableName);
                    setValue(f, null, operation, value);
                }
                else if(field.Contains("ability2"))
                {
                    var type = typeof(AbilityStats.Neutral.ability2);
                    var f = type.GetField(variableName);
                    setValue(f, null, operation, value);
                }
                else if(field.Contains("ability3"))
                {
                    var type = typeof(AbilityStats.Neutral.ability3);
                    var f = type.GetField(variableName);
                    setValue(f, null, operation, value);
                }
            }
            #endregion
            #region Frost
            else if (field.Contains("Frost"))
            {
                if (field.Contains("ability1"))
                {
                    var type = typeof(AbilityStats.Frost.ability1);
                    var f = type.GetField(variableName);
                    setValue(f, null, operation, value);
                }
                else if (field.Contains("ability2"))
                {
                    var type = typeof(AbilityStats.Frost.ability2);
                    var f = type.GetField(variableName);
                    setValue(f, null, operation, value);
                }
                else if (field.Contains("ability3"))
                {
                    var type = typeof(AbilityStats.Frost.ability3);
                    var f = type.GetField(variableName);
                    setValue(f, null, operation, value);
                }
            }
            #endregion
            #region Fire
            else if (field.Contains("Fire"))
            {
                if (field.Contains("ability1"))
                {
                    var type = typeof(AbilityStats.Fire.ability1);
                    var f = type.GetField(variableName);
                    setValue(f, null, operation, value);
                }
                else if (field.Contains("ability2"))
                {
                    var type = typeof(AbilityStats.Fire.ability2);
                    var f = type.GetField(variableName);
                    setValue(f, null, operation, value);
                }
                else if (field.Contains("ability3"))
                {
                    var type = typeof(AbilityStats.Fire.ability3);
                    var f = type.GetField(variableName);
                    setValue(f, null, operation, value);
                }
            }
            #endregion
            #region Earth
            else if (field.Contains("Earth"))
            {
                if (field.Contains("ability1"))
                {
                    var type = typeof(AbilityStats.Earth.ability1);
                    var f = type.GetField(variableName);
                    setValue(f, null, operation, value);
                }
                else if (field.Contains("ability2"))
                {
                    var type = typeof(AbilityStats.Earth.ability2);
                    var f = type.GetField(variableName);
                    setValue(f, null, operation, value);
                }
                else if (field.Contains("ability3"))
                {
                    var type = typeof(AbilityStats.Earth.ability3);
                    var f = type.GetField(variableName);
                    setValue(f, null, operation, value);
                }
            }
            #endregion
            return 1;
        }

        public float changeStatVariable(string field, float value, MathOperations operation)
        {
            var ob = playerStats;
            var typ = typeof(PlayerStats);
            var f = typ.GetField(field);
            var val = (float)f.GetValue(ob);
            var faux = typ.GetField(field);
            var valaux = (float)faux.GetValue(ob);
            float defInc = 0;

            switch (operation)
            {
                case MathOperations.SUM:
                    f.SetValue(ob, (float)Math.Round(value + val, MidpointRounding.AwayFromZero));
                    break;
                case MathOperations.MUL:
                    faux = typ.GetField("def_" + field);
                    valaux = (float)faux.GetValue(ob);
                    defInc = (valaux * value) - valaux;
                    f.SetValue(ob, (float)Math.Round(val + defInc, MidpointRounding.AwayFromZero));
                    break;
                case MathOperations.SET:
                    f.SetValue(ob, (float)Math.Round(value, MidpointRounding.AwayFromZero));
                    break;
                case MathOperations.DEFENCE:
                    float v = (100 - val) * (0.04f * value);
                    f.SetValue(ob, (float)Math.Round(val + v, MidpointRounding.AwayFromZero));
                    return v;
                case MathOperations.MAXHP:
                    faux = typ.GetField("def_" + field);
                    valaux = (float)faux.GetValue(ob);
                    defInc = (valaux * value) - valaux;
                    f.SetValue(ob, (float)Math.Round(val + defInc, MidpointRounding.AwayFromZero));
                    typ.GetField("health").SetValue(ob, (float)Math.Round((float)typ.GetField("health").GetValue(ob) + defInc, MidpointRounding.AwayFromZero));
                    break;
            }
            return 1;
        }

        public void resetAbilityStats()
        {
            AbilityStats.reset();
        }

        public void resetPlayerStats()
        {
            playerStats.reset();
        }

        public void sceneInit()
        {
            player = GameObject.FindWithTag("Player");
            gui = GameObject.Find("GUI");
        }

    }
    #endregion

    #region GUIhelper
    public static class GuiHelper
    {
        // The texture used by DrawLine(Color)
        private static Texture2D _coloredLineTexture;

        // The color used by DrawLine(Color)
        private static Color _coloredLineColor;

        /// <summary>
        /// Draw a line between two points with the specified color and a thickness of 1
        /// </summary>
        /// <param name="lineStart">The start of the line</param>
        /// <param name="lineEnd">The end of the line</param>
        /// <param name="color">The color of the line</param>
        public static void DrawLine(Vector2 lineStart, Vector2 lineEnd, Color color)
        {
            DrawLine(lineStart, lineEnd, color, 1);
        }

        /// <summary>
        /// Draw a line between two points with the specified color and thickness
        /// Inspired by code posted by Sylvan
        /// http://forum.unity3d.com/threads/17066-How-to-draw-a-GUI-2D-quot-line-quot?p=407005&viewfull=1#post407005
        /// </summary>
        /// <param name="lineStart">The start of the line</param>
        /// <param name="lineEnd">The end of the line</param>
        /// <param name="color">The color of the line</param>
        /// <param name="thickness">The thickness of the line</param>
        public static void DrawLine(Vector2 lineStart, Vector2 lineEnd, Color color, int thickness)
        {
            if (_coloredLineTexture == null || _coloredLineColor != color)
            {
                _coloredLineColor = color;
                _coloredLineTexture = new Texture2D(1, 1);
                _coloredLineTexture.SetPixel(0, 0, _coloredLineColor);
                _coloredLineTexture.wrapMode = TextureWrapMode.Repeat;
                _coloredLineTexture.Apply();
            }
            DrawLineStretched(lineStart, lineEnd, _coloredLineTexture, thickness);
        }

        /// <summary>
        /// Draw a line between two points with the specified texture and thickness.
        /// The texture will be stretched to fill the drawing rectangle.
        /// Inspired by code posted by Sylvan
        /// http://forum.unity3d.com/threads/17066-How-to-draw-a-GUI-2D-quot-line-quot?p=407005&viewfull=1#post407005
        /// </summary>
        /// <param name="lineStart">The start of the line</param>
        /// <param name="lineEnd">The end of the line</param>
        /// <param name="texture">The texture of the line</param>
        /// <param name="thickness">The thickness of the line</param>
        public static void DrawLineStretched(Vector2 lineStart, Vector2 lineEnd, Texture2D texture, int thickness)
        {
            Vector2 lineVector = lineEnd - lineStart;
            float angle = Mathf.Rad2Deg * Mathf.Atan(lineVector.y / lineVector.x);
            if (lineVector.x < 0)
            {
                angle += 180;
            }

            if (thickness < 1)
            {
                thickness = 1;
            }

            // The center of the line will always be at the center
            // regardless of the thickness.
            int thicknessOffset = (int)Mathf.Ceil(thickness / 2);

            GUIUtility.RotateAroundPivot(angle,
                                            lineStart);
            GUI.DrawTexture(new Rect(lineStart.x,
                                        lineStart.y - thicknessOffset,
                                        lineVector.magnitude,
                                        thickness),
                            texture);
            GUIUtility.RotateAroundPivot(-angle, lineStart);
        }

        /// <summary>
        /// Draw a line between two points with the specified texture and a thickness of 1
        /// The texture will be repeated to fill the drawing rectangle.
        /// </summary>
        /// <param name="lineStart">The start of the line</param>
        /// <param name="lineEnd">The end of the line</param>
        /// <param name="texture">The texture of the line</param>
        public static void DrawLine(Vector2 lineStart, Vector2 lineEnd, Texture2D texture)
        {
            DrawLine(lineStart, lineEnd, texture, 1);
        }

        /// <summary>
        /// Draw a line between two points with the specified texture and thickness.
        /// The texture will be repeated to fill the drawing rectangle.
        /// Inspired by code posted by Sylvan and ArenMook
        /// http://forum.unity3d.com/threads/17066-How-to-draw-a-GUI-2D-quot-line-quot?p=407005&viewfull=1#post407005
        /// http://forum.unity3d.com/threads/28247-Tile-texture-on-a-GUI?p=416986&viewfull=1#post416986
        /// </summary>
        /// <param name="lineStart">The start of the line</param>
        /// <param name="lineEnd">The end of the line</param>
        /// <param name="texture">The texture of the line</param>
        /// <param name="thickness">The thickness of the line</param>
        public static void DrawLine(Vector2 lineStart, Vector2 lineEnd, Texture2D texture, int thickness)
        {
            Vector2 lineVector = lineEnd - lineStart;
            float angle = Mathf.Rad2Deg * Mathf.Atan(lineVector.y / lineVector.x);
            if (lineVector.x < 0)
            {
                angle += 180;
            }

            if (thickness < 1)
            {
                thickness = 1;
            }

            // The center of the line will always be at the center
            // regardless of the thickness.
            int thicknessOffset = (int)Mathf.Ceil(thickness / 2);

            Rect drawingRect = new Rect(lineStart.x,
                                        lineStart.y - thicknessOffset,
                                        Vector2.Distance(lineStart, lineEnd),
                                        (float)thickness);
            GUIUtility.RotateAroundPivot(angle,
                                            lineStart);
            GUI.BeginGroup(drawingRect);
            {
                int drawingRectWidth = Mathf.RoundToInt(drawingRect.width);
                int drawingRectHeight = Mathf.RoundToInt(drawingRect.height);

                for (int y = 0; y < drawingRectHeight; y += texture.height)
                {
                    for (int x = 0; x < drawingRectWidth; x += texture.width)
                    {
                        GUI.DrawTexture(new Rect(x,
                                                    y,
                                                    texture.width,
                                                    texture.height),
                                        texture);
                    }
                }
            }
            GUI.EndGroup();
            GUIUtility.RotateAroundPivot(-angle, lineStart);
        }
    }
    #endregion
}
