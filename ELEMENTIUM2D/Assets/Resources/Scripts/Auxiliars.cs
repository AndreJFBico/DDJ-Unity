
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
        #region Neutral Abilities
        // NEUTRAL
        public class Neutral
        {
            public class ability1
            {
                public static float attackSpeed = 0.25f;
                public static int projectile_number = 1;
                public static float damage = 2.5f;
                public static float movementForce = 10;
                public static string projectile = "Prefabs/Projectiles/NeutralBlast";

                public static float Damage { get { return damage + GameManager.Instance.Stats.damage * 1f; } }
                public static float AttackSpeed { get { return attackSpeed + GameManager.Instance.Stats.attackSpeed * attackSpeed/10; } } //Player AttackSpeed is to be incremented by int numbers
            }

            public class ability2
            {
                public static float attackSpeed = 1f;
                public static int projectile_number = 10;
                public static float damage = 0.5f;
                public static float movementForce = 1.5f;
                public static string projectile = "Prefabs/Projectiles/NeutralMissile";

                public static float Damage { get { return damage + GameManager.Instance.Stats.damage * 0.75f; } }
                public static float AttackSpeed { get { return attackSpeed + GameManager.Instance.Stats.attackSpeed * attackSpeed / 10; } } //Player AttackSpeed is to be incremented by int numbers
            }
            public class ability3
            {
                public static float attackSpeed = 0.25f;
                public static int projectile_number = 1;
                public static float damage = 0.1f;
                public static float movementForce = 2.5f;
                public static string projectile = "Prefabs/Projectiles/NeutralBouncer";
                public static int splitNumber = 6;
                public static float negativeSplitAngle = -30;
                public static float positiveSplitAngle = 30;
                public static int numSplits = 1;

                public static float Damage { get { return damage + GameManager.Instance.Stats.damage * 0.5f; } }
                public static float AttackSpeed { get { return attackSpeed + GameManager.Instance.Stats.attackSpeed * attackSpeed / 10; } } //Player AttackSpeed is to be incremented by int numbers
            }
        }
        #endregion

        #region Frost abilities
        // FROST
        public class Frost
        {
            public class ability1
            {
                public static float attackSpeed = 0.25f;
                public static int projectile_number = 1;
                public static float damage = 2.5f;
                public static float movementForce = 5;
                public static string projectile = "Prefabs/Projectiles/FrostBolt";

                public static float Damage { get { return damage + GameManager.Instance.Stats.damage * 0.85f; } }
                public static float AttackSpeed { get { return attackSpeed + GameManager.Instance.Stats.attackSpeed * attackSpeed / 10; } } //Player AttackSpeed is to be incremented by int numbers
            }

            public class ability2
            {
                public static float attackSpeed = 2f;
                public static int projectile_number = 1;
                public static int child_projectile_number = 20;
                public static float damage = 1;
                public static float movementForce = 20;
                public static string projectile = "Prefabs/Projectiles/WaterBurst";
                public static string childProjectile = "Prefabs/Projectiles/WaterProjectile";
                public static float deathTimer = 5;

                public static float Damage { get { return damage + GameManager.Instance.Stats.damage * 0.25f; } }
                public static float AttackSpeed { get { return attackSpeed + GameManager.Instance.Stats.attackSpeed * attackSpeed / 10; } } //Player AttackSpeed is to be incremented by int numbers
            }
            public class ability3
            {
                public static float attackSpeed = 1.5f;
                public static int projectile_number = 15;
                public static float damage = 1;
                public static float movementForce = 1.5f;
                public static string projectile = "Prefabs/Projectiles/IceNova";

                public static float Damage { get { return damage + GameManager.Instance.Stats.damage * 0.5f; } }
                public static float AttackSpeed { get { return attackSpeed + GameManager.Instance.Stats.attackSpeed * attackSpeed / 10; } } //Player AttackSpeed is to be incremented by int numbers
            }
        }
        #endregion

        #region Fire Abilities
        // FIRE
        public class Fire
        {
            public class ability1
            {
                public static float attackSpeed = 0.30f;
                public static int projectile_number = 1;
                public static int collisionNumber = 3;
                public static float damage = 1;
                public static float maxForce = 20;
                public static float minForce = 5;
                public static string projectile = "Prefabs/Projectiles/Fireball";

                public static float Damage { get { return damage + GameManager.Instance.Stats.damage * 1f; } }
                public static float AttackSpeed { get { return attackSpeed + GameManager.Instance.Stats.attackSpeed * attackSpeed / 10; } } //Player AttackSpeed is to be incremented by int numbers
            }

            public class ability2
            {
                public static float attackSpeed = 0.25f;
                public static int projectile_number = 1;
                public static float damage = 1;
                public static float movementForce = 200;
                public static string projectile = "Prefabs/Projectiles/";

                public static float Damage { get { return damage + GameManager.Instance.Stats.damage * 0.5f; } }
                public static float AttackSpeed { get { return attackSpeed + GameManager.Instance.Stats.attackSpeed * attackSpeed / 10; } } //Player AttackSpeed is to be incremented by int numbers
            }
            public class ability3
            {
                public static float attackSpeed = 0.25f;
                public static int projectile_number = 1;
                public static float damage = 1;
                public static float movementForce = 200;
                public static string projectile = "Prefabs/Projectiles/Fireball";

                public static float Damage { get { return damage + GameManager.Instance.Stats.damage * 1f; } }
                public static float AttackSpeed { get { return attackSpeed + GameManager.Instance.Stats.attackSpeed * attackSpeed / 10; } } //Player AttackSpeed is to be incremented by int numbers
            }
        }
        #endregion

        #region Earth Abilities
        // EARTH
        public class Earth
        {
            public class ability1
            {
                public static float attackSpeed = 1f;
                public static int projectile_number = 1;
                public static float damage = 6;
                public static float movementForce = 10;
                public static string projectile = "Prefabs/Projectiles/EarthDisk";

                public static float Damage { get { return damage + GameManager.Instance.Stats.damage * 1.5f; } }
                public static float AttackSpeed { get { return attackSpeed + GameManager.Instance.Stats.attackSpeed * attackSpeed / 10; } } //Player AttackSpeed is to be incremented by int numbers
            }

            public class ability2
            {
                public static float attackSpeed = 4f;
                public static int projectile_number = 1;
                public static float damage = 1;
                public static float movementForce = 5;
                public static float abilityTimer = 4.0f;
                public static string projectile = "Prefabs/Projectiles/EarthShield";

                public static float Damage { get { return damage + GameManager.Instance.Stats.damage * 1.5f; } }
                public static float AttackSpeed { get { return attackSpeed + GameManager.Instance.Stats.attackSpeed * attackSpeed / 10; } } //Player AttackSpeed is to be incremented by int numbers
            }
            public class ability3
            {
                public static float attackSpeed = 0.5f;
                public static int projectile_number = 1;
                public static float damage = 3;
                public static float movementForce = 200;
                public static string projectile = "Prefabs/Projectiles/EarthStun";

                public static float Damage { get { return damage + GameManager.Instance.Stats.damage * 1.5f; } }
                public static float AttackSpeed { get { return attackSpeed + GameManager.Instance.Stats.attackSpeed * attackSpeed / 10; } } //Player AttackSpeed is to be incremented by int numbers
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
            public static float visionRadius = 0;
            public static float unalertedSpeed = 0;
            public static float alertedSpeed = 0;
        }

        public class BasicNeutral
        {
            public static float maxHealth = 20;
            public static float damage = 5;
            public static float defence = 0;
            public static float waterResist = 0;
            public static float earthResist = 0;
            public static float fireResist = 0;
            public static float visionRadius = 5.46f;
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
            public static float visionRadius = 5.46f;
            public static float rangedRadius = 1.5f;
            public static float rangedAttackSpeed = 0.5f;
            public static string neutralEnemyProjectile = "Prefabs/Projectiles/Musk";
            public static float unalertedSpeed = 0.5f;
            public static float alertedSpeed = 1.5f;
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
            public static float visionRadius = 5.46f;
            public static float rangedRadius = 1.5f;
            public static float rangedAttackSpeed = 0.5f;
            public static string neutralEnemyProjectile = "Prefabs/Projectiles/Healing";
            public static float unalertedSpeed = 0.5f;
            public static float alertedSpeed = 1.5f;
        }

        public class NeutralShield
        {
            public static float maxHealth = 20;
            public static float damage = 5;
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
            public static float visionRadius = 5.46f;
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
            public static float visionRadius = 5.46f;
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
            public static float waterResist = 5;
            public static float earthResist = 5;
            public static float fireResist = 5;
            public static float visionRadius = 5.46f;
            public static Elements type = Elements.FIRE;
            public static float unalertedSpeed = 0.5f;
            public static float alertedSpeed = 1.5f;
        }
        #endregion

        // EARTH
        #region Earth Enemies
        public class EarthBasic
        {
            public static float maxHealth = 50;
            public static float damage = 15;
            public static float defence = 0;
            public static float waterResist = 0;
            public static float earthResist = 80;
            public static float fireResist = -50;
            public static float visionRadius = 5.46f;
            public static Elements type = Elements.EARTH;
            public static float statusDurability = 3;
            public static float statusIntensity = 0.35f;
            public static float unalertedSpeed = 0.35f;
            public static float alertedSpeed = 1f;
        }

        public class EarthRanged
        {
            public static float maxHealth = 20;
            public static float damage = 10;
            public static float defence = 2;
            public static float waterResist = 5;
            public static float earthResist = 5;
            public static float fireResist = 5;
            public static float visionRadius = 5.46f;
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
        public const float def_damage = 2;
        public const float def_attackSpeed = 0;//Player AttackSpeed is to be incremented by int numbers
        //public const float inc_damage_level = 1.5f;

        public const float def_health = 30.0f;
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

        //public const float def_inc_health = 10.5f;
        
        // VARIABLES, these can be changed and reset at will they represent the current player stats
        public float moveSpeed = 2f;
        public float moveInContactWithEnemy = 1f;
        public float maxHealth = def_health;
        public float health = def_health;
        public float damage = def_damage;
        public float attackSpeed = def_attackSpeed; //Player AttackSpeed is to be incremented by int numbers
        public float defence = def_defence;

        public Func<float> getDefence = () => { return def_defence * 2.0f; };

        public float waterResist = 0;
        public float earthResist = 0;
        public float fireResist = 0;
        public float damageTimer = 1.5f;
        public float multiplierTimer = def_multiplierTimer;
        public int currentMultiplier = 0;
        public int[] multiplierLevels = { 7, 12, 20 };


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

        public float lim_primary_neutral_level = 1;
        public float lim_secondary_neutral_level = 1;
        public float lim_terciary_neutral_level = 1;

        public float lim_primary_earth_level = 1;
        public float lim_secondary_earth_level = 1;
        public float lim_terciary_earth_level = 1;

        public float lim_primary_fire_level = 1;
        public float lim_secondary_fire_level = 1;
        public float lim_terciary_fire_level = 1;

        public float lim_primary_water_level = 1;
        public float lim_secondary_water_level = 1;
        public float lim_terciary_water_level = 1;

        public float lim_points = 4;

        public float depth = 3;


        //ATTENTION IF YOU ADD A NEW VARIABLE PLS DONT FORGET TO ADD IT TO RESET!!!!!!!
        public void reset()
        {
            moveSpeed = 2f;
            moveInContactWithEnemy = 1f;
            maxHealth = def_health;
            health = def_health;
            damage = def_damage;
            attackSpeed = def_attackSpeed;
            defence = def_defence;
            waterResist = 0;
            earthResist = 0;
            fireResist = 0;
            damageTimer = 2.35f;
            multiplierTimer = def_multiplierTimer;
            multiplierLevels = new int[]{ 7, 12, 20};
            currentMultiplier = 0;

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

    }
    #endregion

    #region Game Manager
    [Serializable]
    public class GameManager
    {
        private static GameManager _instance = null;
        private static GameObject player;

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

        public GameObject Player { get { return player; } }

        public Sprite UnknownSymbol { get { return unknownSymbol; } }

        public PlayerStats Stats { get { return playerStats; } }

        public List<string> StatNames { get { return statNames; } }

        public Sprite FireElement { get { return fireElement; } }

        public Sprite EarthElement { get { return earthElement; } }

        public Sprite WaterElement { get { return waterElement; } }

        public Sprite NeutralElement { get { return neutralElement; } }

        public ShootElement CurrentElement { get { return currentElement; } set { currentElement = value; } }

        public static GameManager Instance { get { if (_instance == null) { _instance = new GameManager(); playerStats = new PlayerStats(); } return _instance; } }


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

        public void changeStatVariable(string field, float value, MathOperations operation)
        {
            var ob = playerStats;
            var typ = typeof(PlayerStats);
            var f = typ.GetField(field);
            //var prop = typ.GetProperty("DataFile");
            var val = (float)f.GetValue(ob);
            switch (operation)
            {
                case MathOperations.SUM:
                    f.SetValue(ob, value + val);
                    break;
                case MathOperations.MUL:
                    f.SetValue(ob, val * value);
                    break;
                case MathOperations.SET:
                    f.SetValue(ob, value);
                    break;
                case MathOperations.DEFENCE:
                    f.SetValue(ob, val + (100 - val) * (0.04f * value));
                    break;
                case MathOperations.MAXHP:
                    float newval = val * value;
                    float diff = newval - val;
                    f.SetValue(ob, val * value);
                    typ.GetField("health").SetValue(ob, (float)typ.GetField("health").GetValue(ob) + diff);
                    break;
            }
        }

        public void resetPlayerStats()
        {
            playerStats.reset();
        }

        public void sceneInit()
        {
            player = GameObject.FindWithTag("Player");
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
