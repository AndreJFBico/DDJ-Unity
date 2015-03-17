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

    public override void applyStatusEffect(EnemyScript script)
    {
        applySlowStatus(script);
    }
    
    public override void resetDuration(float dur)
    {
        slowTimer = dur;
    }

    public override void setIntensity(float inten)
    {
        base.setIntensity(inten);
    }

    public override void setDuration(float dur)
    {
        base.setDuration(dur);
        slowTimer = duration;
    }

    //#############################################################
    //################### EFFECT RESPONSIBLE ######################
    //#############################################################
    private IEnumerator slowing(EnemyScript script)
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        previousSpeed = agent.speed;
        agent.speed *= intensity;

        while (slowTimer > 0)
        {
            slowTimer -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        agent.speed = previousSpeed;
        Destroy(this);
    }

    public void applySlowStatus(EnemyScript script)
    {
        StartCoroutine("slowing", script);
    }
}
