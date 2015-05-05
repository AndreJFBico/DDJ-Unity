using UnityEngine;
using System.Collections;
using Includes;

public class NeutralElement : ShootElement
{

    // Use this for initialization
    void Start()
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

        gunBlast1 = GameObject.Find("NeutralBlast1");
        gunBlast2 = GameObject.Find("NeutralBlast2");
        gunBlast1.SetActive(false);
        gunBlast2.SetActive(false);

        _elementType = Includes.Elements.NEUTRAL;
        _active = true;

        projectile1 = (GameObject)Resources.Load(AbilityStats.Neutral.ability1.projectile);
        projectile2 = (GameObject)Resources.Load(AbilityStats.Neutral.ability2.projectile);
        projectile3 = (GameObject)Resources.Load(AbilityStats.Neutral.ability3.projectile);

        updateUnlocked();

    }

    public override void updateUnlocked()
    {
        mainUnlocked = GameManager.Instance.Stats.primary_neutral_level > 0;
        secondaryUnlocked = GameManager.Instance.Stats.secondary_neutral_level > 0;
        terciaryUnlocked = GameManager.Instance.Stats.terciary_neutral_level > 0;
    }

    public override void fireMain()
    {
        if (!mainUnlocked)
            return;

        float damage = AbilityStats.Neutral.ability1.Damage;
        float attackSpeed = AbilityStats.Neutral.ability1.attackSpeed;
        int projectileNumber = AbilityStats.Neutral.ability1.projectile_number;

        if (canMain)
        {
            canMain = false;
            Invoke("resetMain", attackSpeed);
            fire(attackSpeed, projectileNumber, damage, projectile1);
            GameManager.Instance.GUI.GetComponent<GUIManager>().addCoolDown(0, attackSpeed);
        }
    }

    public override void fireSecondary()
    {
        if (!secondaryUnlocked)
            return;

        float damage = AbilityStats.Neutral.ability2.Damage;
        float attackSpeed = AbilityStats.Neutral.ability2.attackSpeed;
        int projectileNumber = AbilityStats.Neutral.ability2.projectile_number;

        if (canSecondary)
        {
            canSecondary = false;
            Invoke("resetSecondary", attackSpeed);
            fire(attackSpeed, projectileNumber, damage, projectile2);
            GameManager.Instance.GUI.GetComponent<GUIManager>().addCoolDown(1, attackSpeed);
        }
    }

    public override void fireTerciary()
    {
        if (!terciaryUnlocked)
            return;

        float damage = AbilityStats.Neutral.ability3.Damage;
        float attackSpeed = AbilityStats.Neutral.ability3.attackSpeed;
        int projectileNumber = AbilityStats.Neutral.ability3.projectile_number;

        if (canTerciary)
        {
            canTerciary = false;
            Invoke("resetTerciary", attackSpeed);
            fire(attackSpeed, projectileNumber, damage, projectile3);
            GameManager.Instance.GUI.GetComponent<GUIManager>().addCoolDown(2, attackSpeed);
        }
    }
}
