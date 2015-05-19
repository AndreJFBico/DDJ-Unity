using UnityEngine;
using System.Collections;
using Includes;

public class EarthElement : ShootElement
{
    // Use this for initialization
    void Awake()
    {
        Transform[] aux = GetComponentsInChildren<Transform>();
        foreach (Transform item in aux)
        {
            if (item.name == "BarrelEnd")
            {
                barrelEnd = item;
            }
            if (item.name == "Rotator")
            {
                rotator = item;
            }
        }

        gunBlast1 = GameObject.Find("EarthBlast1");
        gunBlast2 = GameObject.Find("EarthBlast2");
        gunBlast1.SetActive(false);
        gunBlast2.SetActive(false);

        _elementType = Includes.Elements.EARTH;
        _active = false;

        projectile1 = (GameObject)Resources.Load(AbilityStats.Earth.ability1.projectile);
        projectile2 = (GameObject)Resources.Load(AbilityStats.Earth.ability2.projectile);
        projectile3 = (GameObject)Resources.Load(AbilityStats.Earth.ability3.projectile);

        updateUnlocked();

        //attackSpeed = 0.25f;
    }

    public override void updateUnlocked()
    {
        mainUnlocked = GameManager.Instance.Stats.primary_earth_level > 0;
        secondaryUnlocked = GameManager.Instance.Stats.secondary_earth_level > 0;
        terciaryUnlocked = GameManager.Instance.Stats.terciary_earth_level > 0;
    }

    public override void checkAbilitiesCoolDown()
    {
        if (mainUnlocked)
            GameManager.Instance.GUI.GetComponent<GUIManager>().addCoolDown(_elementType, 0, AbilityStats.Earth.ability1.attackSpeed);
        if (secondaryUnlocked)
            GameManager.Instance.GUI.GetComponent<GUIManager>().addCoolDown(_elementType, 1, AbilityStats.Earth.ability2.attackSpeed);
        if (terciaryUnlocked)
            GameManager.Instance.GUI.GetComponent<GUIManager>().addCoolDown(_elementType, 2, AbilityStats.Earth.ability3.attackSpeed);
    }

    public override void fireMain()
    {
        if (!mainUnlocked)
            return;

        float damage = AbilityStats.Earth.ability1.Damage;
        float attackSpeed = AbilityStats.Earth.ability1.attackSpeed;
        int projectileNumber = AbilityStats.Earth.ability1.projectile_number;

        if (canMain)
        {
            canMain = false;
            Invoke("resetMain", attackSpeed);
            fire(attackSpeed, projectileNumber, damage, projectile1);
            LoggingManager.Instance.getEntry(typeof(NumTypeAbilityPerZone)).writeEntry(projectile1.name, Elements.EARTH, GameManager.Instance.PlayerRoom.name, GameManager.Instance.DungeonRoomType());
            GameManager.Instance.GUI.GetComponent<GUIManager>().addCoolDown(_elementType, 0, attackSpeed);
        }
    }

    public override void fireSecondary()
    {
        if (!secondaryUnlocked)
            return;

        float damage = AbilityStats.Earth.ability2.Damage;
        float attackSpeed = AbilityStats.Earth.ability2.attackSpeed;
        int projectileNumber = AbilityStats.Earth.ability2.projectile_number;

        if (canSecondary)
        {
            canSecondary = false;
            Invoke("resetSecondary", attackSpeed);
            fire(attackSpeed, projectileNumber, damage, projectile2);
            LoggingManager.Instance.getEntry(typeof(NumTypeAbilityPerZone)).writeEntry(projectile2.name, Elements.EARTH, GameManager.Instance.PlayerRoom.name, GameManager.Instance.DungeonRoomType());
            GameManager.Instance.GUI.GetComponent<GUIManager>().addCoolDown(_elementType, 1, attackSpeed);
        }
    }

    public override void fireTerciary()
    {
        if (!terciaryUnlocked)
            return;

        float damage = AbilityStats.Earth.ability3.Damage;
        float attackSpeed = AbilityStats.Earth.ability3.attackSpeed;
        int projectileNumber = AbilityStats.Earth.ability3.projectile_number;

        if (canTerciary)
        {
            canTerciary = false;
            Invoke("resetTerciary", attackSpeed);
            fire(attackSpeed, projectileNumber, damage, projectile3);
            LoggingManager.Instance.getEntry(typeof(NumTypeAbilityPerZone)).writeEntry(projectile3.name, Elements.EARTH, GameManager.Instance.PlayerRoom.name, GameManager.Instance.DungeonRoomType());
            GameManager.Instance.GUI.GetComponent<GUIManager>().addCoolDown(_elementType, 2, attackSpeed);
        }
    }
}