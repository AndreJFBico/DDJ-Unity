using UnityEngine;
using System.Collections;
using Includes;

public class BurningStatusEffect : StatusEffect
{
    private float burningTimer;
    private float burningDamage = 0;
    private bool isBurning = false;

    private Texture2D sprite;

	// Use this for initialization
	protected override void Start () {
        burningTimer = duration;
        sprite = Resources.Load<Texture2D>("GUIImages/Elements/Fire");
	}

    #region GUI
    //#############################################################
    //############################ GUI ############################
    //#############################################################
    void OnGUI()
    {
        Color guiColor = Color.white;
        guiColor.a = 0.55f;
        Color prevColor = GUI.color;
        GUI.color = guiColor;
        Vector3 objPos = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        GUI.DrawTexture(new Rect(objPos.x-10, Screen.height - objPos.y-10, 20, 20), sprite);

        GUI.color = prevColor;
    }
    #endregion

    #region Variable Modifiers
    //#############################################################
    //################### VARIABLE MODIFIERS ######################
    //#############################################################
    public override void applyStatusEffect(Agent script)
    {
        applyBurningStatus(script);
    }

    public override void initiate(float inten, float dur)
    {
        Intensity = inten;
        Duration = dur;
        burningTimer = duration;
    }

    public override void resetDuration(float dur)
    {
        burningTimer = dur;
    }
    #endregion

    #region Effect Responsible
    //#############################################################
    //################### EFFECT RESPONSIBLE ######################
    //#############################################################
    private IEnumerator burning(Agent script)
    {
        float interval = 1f;
        while (burningTimer > 0)
        {
            script.takeDamage(intensity/(duration/interval), Elements.FIRE, true);
            burningTimer -= interval;
            yield return new WaitForSeconds(interval);
        }
        Destroy(this);
        isBurning = false;
        burningDamage = 0;
    }

    public void applyBurningStatus(Agent script)
    {
        if (isBurning)
        {
            if (intensity > burningDamage)
            {
                StopCoroutine("burning");
                StartCoroutine("burning", script);
                burningDamage = intensity;
            }
            burningTimer = duration;
        }
        else
        {
            isBurning = true;
            burningTimer = duration;
            burningDamage = intensity;
            StartCoroutine("burning", script);
        }
    }
    #endregion
}
