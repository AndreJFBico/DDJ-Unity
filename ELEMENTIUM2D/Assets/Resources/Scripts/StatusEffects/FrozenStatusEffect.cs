using UnityEngine;
using System.Collections;

public class FrozenStatusEffect : StatusEffect {


    private float previousSpeed;

    private float stunnedTimer;

    private Texture2D sprite;

    // Use this for initialization
    protected override void Start()
    {
        stunnedTimer = duration;// = duration
        sprite = Resources.Load<Texture2D>("GUIImages/frozen");
    }


    //#############################################################
    //############################ GUI ############################
    //#############################################################

    void OnGUI()
    {
        Color guiColor = Color.white;
        guiColor.a = 1.0f;
        Color prevColor = GUI.color;
        GUI.color = guiColor;
        Vector3 objPos = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        GUI.DrawTexture(new Rect(objPos.x - 60, Screen.height - objPos.y - 50, 120, 100), sprite, ScaleMode.ScaleToFit, true, 1.0F);

        GUI.color = prevColor;
    }

    //#############################################################
    //################### VARIABLE MODIFIERS ######################
    //#############################################################

    public override void applyStatusEffect(Agent script)
    {
        applyFrozenStatus(script);
    }

    public override void resetDuration(float dur)
    {
        stunnedTimer = dur;
    }

    public override void initiate(float inten, float dur)
    {
        Intensity = inten;
        Duration = dur;
        stunnedTimer = duration;
    }

    //#############################################################
    //################### EFFECT RESPONSIBLE ######################
    //#############################################################
    private IEnumerator freeze(Agent script)
    {
        EnemyScript agent = GetComponent<EnemyScript>();
        agent.stop();

        while (stunnedTimer > 0)
        {
            stunnedTimer -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        if (agent.inRangeWithPlayer())
            agent.restart(true);
        else agent.restart(false);
        Destroy(this);
    }

    public void applyFrozenStatus(Agent script)
    {
        StartCoroutine("freeze", script);
    }
}
