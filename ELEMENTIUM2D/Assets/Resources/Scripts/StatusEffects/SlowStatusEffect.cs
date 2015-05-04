using UnityEngine;
using System.Collections;

public class SlowStatusEffect : StatusEffect
{
    private float previousSpeed;

    private float slowTimer;

    private Texture2D sprite;

	// Use this for initialization
	protected override void Start () {
        slowTimer = duration;
        sprite = Resources.Load<Texture2D>("GUIImages/Elements/Neutral");
	}


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
        GUI.DrawTexture(new Rect(objPos.x - 10, Screen.height - objPos.y - 10, 20, 20), sprite);

        GUI.color = prevColor;
    }

    //#############################################################
    //################### VARIABLE MODIFIERS ######################
    //#############################################################

    public override void applyStatusEffect(Agent script)
    {
        applySlowStatus(script);
    }
    
    public override void resetDuration(float dur)
    {
        slowTimer = dur;
    }

    public override void initiate(float inten, float dur)
    {
        Intensity = inten;
        Duration = dur;
        slowTimer = duration;
    }

    //#############################################################
    //################### EFFECT RESPONSIBLE ######################
    //#############################################################
    private IEnumerator slowing(Agent script)
    {
        script.slowSelf(intensity);

        while (slowTimer > 0)
        {
            slowTimer -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        script.restoreMoveSpeed(intensity);

        Destroy(this);
    }

    public void applySlowStatus(Agent script)
    {
        StartCoroutine("slowing", script);
    }
}
