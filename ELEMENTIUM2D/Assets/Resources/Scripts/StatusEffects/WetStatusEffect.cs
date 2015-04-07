using UnityEngine;
using System.Collections;
using Includes;

public class WetStatusEffect : StatusEffect
{
    private float wetTimer;
    private bool isWet = false;

    private Texture2D sprite;

	// Use this for initialization
	protected override void Start () {
        wetTimer = duration;
        sprite = Resources.Load<Texture2D>("GUIImages/Elements/Frost");
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
    public override void applyStatusEffect(EnemyScript script)
    {
        applyWetStatus(script);
    }

    public override void setIntensity(float inten)
    {
        base.setIntensity(inten);
    }

    public override void setDuration(float dur)
    {
        base.setDuration(dur);
        wetTimer = duration;
    }

    public override void resetDuration(float dur)
    {
        wetTimer = dur;
    }
    #endregion

    #region Effect Responsible
    //#############################################################
    //################### EFFECT RESPONSIBLE ######################
    //#############################################################
    private IEnumerator wet(EnemyScript script)
    {
        while (wetTimer > 0)
        {
            wetTimer -= 1;
            yield return new WaitForSeconds(1);
        }
        isWet = false;
        Destroy(this);
    }

    public void applyWetStatus(EnemyScript script)
    {
        if (isWet)
        {
            wetTimer = duration;
        }
        else
        {
            isWet = true;
            wetTimer = duration;
            StartCoroutine("wet", script);
        }
    }
    #endregion
}
