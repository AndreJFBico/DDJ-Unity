﻿using UnityEngine;
using System.Collections;
using Includes;

public class FireElement: ShootElement {

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

        gunBlast1 = GameObject.Find("FireBlast1");
        gunBlast2 = GameObject.Find("FireBlast2");
        gunBlast1.SetActive(false);
        gunBlast2.SetActive(false);

        _elementType = Includes.Elements.FIRE;
        _active = false;

        projectile1 = (GameObject)Resources.Load(AbilityStats.Fire.ability1.projectile);
        projectile2 = (GameObject)Resources.Load(AbilityStats.Fire.ability2.projectile);
        projectile3 = (GameObject)Resources.Load(AbilityStats.Fire.ability3.projectile);

        updateUnlocked();

        //attackSpeed = 0.25f;
    }

    public override void updateUnlocked()
    {
        mainUnlocked = GameManager.Instance.Stats.primary_fire_level > 0;
        secondaryUnlocked = GameManager.Instance.Stats.secondary_fire_level > 0;
        terciaryUnlocked = GameManager.Instance.Stats.terciary_fire_level > 0;
    }

    public override void checkAbilitiesCoolDown()
    {
        if (mainUnlocked)
            GameManager.Instance.GUI.GetComponent<GUIManager>().addCoolDown(_elementType, 0, AbilityStats.Fire.ability1.attackSpeed);
        if (secondaryUnlocked)
            GameManager.Instance.GUI.GetComponent<GUIManager>().addCoolDown(_elementType, 1, OilPuddleManager.Instance.internalCooldown());
        if (terciaryUnlocked)
            GameManager.Instance.GUI.GetComponent<GUIManager>().addCoolDown(_elementType, 2, AbilityStats.Fire.ability3.attackSpeed);
    }

    #region Fire functions

    public override void fireMain()
    {
        if (!mainUnlocked)
            return;

        float damage = AbilityStats.Fire.ability1.Damage;
        float attackSpeed = AbilityStats.Fire.ability1.attackSpeed;
        int projectileNumber = AbilityStats.Fire.ability1.projectile_number;

        if (canMain)
        {
            canMain = false;
            Invoke("resetMain", attackSpeed);
            fire(attackSpeed, projectileNumber, damage, projectile1);
            LoggingManager.Instance.getEntry(typeof(NumTypeAbilityPerZone)).writeEntry(projectile1.name, Elements.FIRE, GameManager.Instance.PlayerRoom.name, GameManager.Instance.DungeonRoomType());
            GameManager.Instance.GUI.GetComponent<GUIManager>().addCoolDown(_elementType, 0, attackSpeed);
        }
    }

    public override void fireSecondary()
    {
        if (!secondaryUnlocked)
            return;

        if (canSecondary)
        {
            canSecondary = false;
            Invoke("resetSecondary", OilPuddleManager.Instance.internalCooldown());
            gunBlast1.SetActive(true);
            gunBlast2.SetActive(true);
            Invoke("deactivateBlast", 0.1f);
            Instantiate(OilPuddleManager.Instance.OilPuddle, barrelEnd.position, OilPuddleManager.Instance.OilPuddle.transform.rotation);
            LoggingManager.Instance.getEntry(typeof(NumTypeAbilityPerZone)).writeEntry(OilPuddleManager.Instance.OilPuddle.name, Elements.FIRE, GameManager.Instance.PlayerRoom.name, GameManager.Instance.DungeonRoomType());
            GameManager.Instance.GUI.GetComponent<GUIManager>().addCoolDown(_elementType, 1, OilPuddleManager.Instance.internalCooldown());
        }
    }

    public override void fireTerciary()
    {
        if (!terciaryUnlocked)
            return;

        float damage = AbilityStats.Fire.ability3.Damage;
        float attackSpeed = AbilityStats.Fire.ability3.attackSpeed;
        int projectileNumber = AbilityStats.Fire.ability3.projectile_number;

        if (canTerciary)
        {
            canTerciary = false;
            Invoke("resetTerciary", attackSpeed);
            fire(attackSpeed, projectileNumber, damage, projectile3);
            LoggingManager.Instance.getEntry(typeof(NumTypeAbilityPerZone)).writeEntry(projectile3.name, Elements.FIRE, GameManager.Instance.PlayerRoom.name, GameManager.Instance.DungeonRoomType());
            GameManager.Instance.GUI.GetComponent<GUIManager>().addCoolDown(_elementType, 2, attackSpeed);
        }
    }

    #endregion

}
