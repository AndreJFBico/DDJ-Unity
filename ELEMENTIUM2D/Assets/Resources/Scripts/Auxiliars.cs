
#define DEBUG
using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace Includes
{
    public enum Elements { NEUTRAL, FIRE, EARTH, FROST };
    public enum BreakableWalls { NEUTRAL, FIRE, EARTH, FROST};
    public enum StatusEffects { BURNING, SLOW, STUN, WET};

    public enum MathOperations { SUM, MUL, SET};

    #region Constants
    public class Constants
    {
        public const string breakable = "Breakable";
        public const string elementalyModifiable = "ElementalyModifiable";
        public const float enemyRoamRadius = 2.0f;
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
                public static float damage = 12.5f;
                public static float movementForce = 10;
                public static string projectile = "Prefabs/Projectiles/NeutralBlast";
            }

            public class ability2
            {
                public static float attackSpeed = 1f;
                public static int projectile_number = 10;
                public static float damage = 7.5f;
                public static float movementForce = 1.5f;
                public static string projectile = "Prefabs/Projectiles/NeutralMissile";
            }
            public class ability3
            {
                public static float attackSpeed = 0.25f;
                public static int projectile_number = 1;
                public static float damage = 5;
                public static float movementForce = 2.5f;
                public static string projectile = "Prefabs/Projectiles/NeutralBouncer";
                public static int splitNumber = 6;
                public static float negativeSplitAngle = -30;
                public static float positiveSplitAngle = 30;
                public static int numSplits = 1;
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
                public static float damage = 10;
                public static float movementForce = 5;
                public static string projectile = "Prefabs/Projectiles/FrostBolt";
            }

            public class ability2
            {
                public static float attackSpeed = 2.01f;
                public static int projectile_number = 1;
                public static int child_projectile_number = 20;
                public static float damage = 5;
                public static float movementForce = 20;
                public static string projectile = "Prefabs/Projectiles/WaterBurst";
                public static string childProjectile = "Prefabs/Projectiles/WaterProjectile";
                public static float deathTimer = 5;
            }
            public class ability3
            {
                public static float attackSpeed = 0.25f;
                public static int projectile_number = 7;
                public static float damage = 5;
                public static float movementForce = 1.5f;
                public static string projectile = "Prefabs/Projectiles/IceNova";
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
                public static float damage = 6;
                public static float maxForce = 20;
                public static float minForce = 5;
                public static string projectile = "Prefabs/Projectiles/Fireball";
            }

            public class ability2
            {
                public static float attackSpeed = 0.25f;
                public static int projectile_number = 1;
                public static float damage = 5;
                public static float movementForce = 200;
                public static string projectile = "Prefabs/Projectiles/";
            }
            public class ability3
            {
                public static float attackSpeed = 0.25f;
                public static int projectile_number = 1;
                public static float damage = 5;
                public static float movementForce = 200;
                public static string projectile = "Prefabs/Projectiles/Fireball";
            }
        }
        #endregion

        #region Earth Abilities
        // EARTH
        public class Earth
        {
            public class ability1
            {
                public static float attackSpeed = 0.50f;
                public static int projectile_number = 1;
                public static float damage = 20;
                public static float movementForce = 10;
                public static string projectile = "Prefabs/Projectiles/EarthDisk";
            }

            public class ability2
            {
                public static float attackSpeed = 0.25f;
                public static int projectile_number = 1;
                public static float damage = 5;
                public static float movementForce = 5;
                public static float abilityTimer = 4.0f;
                public static string projectile = "Prefabs/Projectiles/EarthShield";
            }
            public class ability3
            {
                public static float attackSpeed = 0.25f;
                public static int projectile_number = 1;
                public static float damage = 5;
                public static float movementForce = 200;
                public static string projectile = "Prefabs/Projectiles/EarthStun";
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
        public class BasicNeutral
        {
            public static float maxHealth = 50;
            public static float damage = 10;
            public static float defence = 2;
            public static float waterResist = 5;
            public static float earthResist = 5;
            public static float fireResist = 5;
            public static float visionRadius = 5.46f;
        }

        public class RangedNeutral
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
            public static string neutralEnemyProjectile = "Prefabs/Projectiles/Musk";
        }

        public class HealerNeutral
        {
            public static float maxHealth = 50;
            public static float healAmount = 3;
            public static float damage = 10;
            public static float defence = 2;
            public static float waterResist = 5;
            public static float earthResist = 5;
            public static float fireResist = 5;
            public static float visionRadius = 5.46f;
            public static float rangedRadius = 1.5f;
            public static float rangedAttackSpeed = 0.5f;
            public static string neutralEnemyProjectile = "Prefabs/Projectiles/Healing";
        }

        public class NeutralShield
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
        #endregion

        // Frost
        #region Frost/Water Enemies
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
        #endregion

        // FIRE
        #region Fire Enemies
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
        #endregion

        // EARTH
        #region Earth Enemies
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
        #endregion
    }
    #endregion

    // STARTING STATS
    #region PlayerStats
    [Serializable]
    public class PlayerStats
    {
        public const float def_damage = 3;
        //public const float inc_damage_level = 1.5f;

        public const float def_defence = 0;

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

        public const float def_health = 30.0f;
        //public const float def_inc_health = 10.5f;
        
        // VARIABLES, these can be changed and reset at will they represent the current player stats
        public float moveSpeed = 2.5f;
        public float moveInContactWithEnemy = 1.0f;
        public float maxHealth = def_health;
        public float damage = def_damage;
        public float defence = def_defence;
        public float waterResist = 0;
        public float earthResist = 0;
        public float fireResist = 0;
        public float damageTimer = 2.35f;

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
        public float lim_moveSpeed = 2;
        public float lim_moveInContactWithEnemy = 0;
        public float lim_maxHealth = 3;
        public float lim_damage = 3;
        public float lim_defence = 2;
        public float lim_waterResist = 0;
        public float lim_earthResist = 0;
        public float lim_fireResist = 0;
        public float lim_damageTimer = 3f;

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

        //ATTENTION IF YOU ADD A NEW VARIABLE PLS DONT FORGET TO ADD IT TO RESET!!!!!!!
        public void reset()
        {
            moveSpeed = 2.5f;
            moveInContactWithEnemy = 1.0f;
            maxHealth = def_health;
            damage = def_damage;
            defence = def_defence;
            waterResist = 0;
            earthResist = 0;
            fireResist = 0;
            damageTimer = 2.35f;

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
        private static PlayerStats playerStats;
        private static List<string> statNames;
        private static ShootElement currentElement;

        private static GameObject[] neutralEnemies;

        protected GameManager() { init(); }
        // Singleton pattern implementation

        public GameObject IceWall { get { return iceWall; } }

        public GameObject WaterPuddle { get { return waterPuddle; } }

        public GameObject OilPuddle { get { return oilPuddle; } }

        public GameObject Player { get { return player; } }

        public PlayerStats Stats { get { return playerStats; } }

        public List<string> StatNames { get { return statNames; } }

        public ShootElement CurrentElement { get { return currentElement; } set { currentElement = value; } }

        public static GameManager Instance { get { if (_instance == null) { _instance = new GameManager(); playerStats = new PlayerStats(); } return _instance; } }


        private void init()
        {
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
            }
        }

        public void resetPlayerStats()
        {
            playerStats.reset();
        }

        public void sceneInit()
        {
            player = GameObject.FindWithTag("Player");
            iceWall = (GameObject)Resources.Load("Prefabs/Environment/IceWall");
            waterPuddle = (GameObject)Resources.Load("Prefabs/Environment/WaterPuddle");
            oilPuddle = (GameObject)Resources.Load("Prefabs/Environment/OilPuddle");
            neutralEnemies = Resources.LoadAll("Prefabs/Enemies/Neutral") as GameObject[];
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
