using UnityEngine;
using System;
using System.Collections;
using Includes;

public class MultiplierManager: MonoBehaviour{

    protected Func<float> multiplierTimer;
    protected Func<int, int> setCurrentMultiplier;
    protected Func<int> getCurrentMultiplier;
    protected float currentMultiplierTimer;
    protected bool multiplierTimerRunning;

    protected Func<float> maxKillTimer;
    protected Func<float> killTimer;
    protected Func<float, float> setKillTimer;
    protected Func<float> maxHitTimer;
    protected Func<float> hitTimer;
    protected Func<float, float> setHitTimer;

    void Start()
    {
        multiplierTimer = () => { return GameManager.Instance.Stats.multiplierTimer; };
        currentMultiplierTimer = multiplierTimer();
        setCurrentMultiplier = (number) => { return (GameManager.Instance.Stats.currentMultiplier = number); };
        getCurrentMultiplier = () => { return GameManager.Instance.Stats.currentMultiplier; };
        multiplierTimerRunning = false;
        maxKillTimer = () => { return GameManager.Instance.Stats.maxKillTimer; };
        killTimer = () => { return GameManager.Instance.Stats.killTimer; };
        setKillTimer = (a) => { return GameManager.Instance.Stats.killTimer = a; };
        maxHitTimer = () => { return GameManager.Instance.Stats.maxHitTimer; };
        hitTimer = () => { return GameManager.Instance.Stats.hitTimer; };
        setHitTimer = (a) => { return GameManager.Instance.Stats.hitTimer = a; };
    }

    private IEnumerator resetMultiplier()
    {
        float increment = 0.1f;
        while(hitTimer() > 0 && killTimer() > 0 )
        {
            setHitTimer(hitTimer() - increment);
            setKillTimer(killTimer() - increment);
            yield return new WaitForSeconds(increment);
        }
        LoggingManager.Instance.MaxMultiplier = getCurrentMultiplier();
        setCurrentMultiplier(0);
        multiplierTimerRunning = false;
    }

    public void resetKillTimer()
    {
        setKillTimer(maxKillTimer());
        setHitTimer(maxHitTimer());
    }

    public void resetHitTimer()
    {
        setHitTimer(maxHitTimer());
    }

    public void increaseMultiplier(int inc)
    {
        //increase current multiplier by amount
        //reset/start timer to reduce currentMultiplier to 0
        setCurrentMultiplier(getCurrentMultiplier() + inc);
        if(!multiplierTimerRunning)
        {
            multiplierTimerRunning = true;
            currentMultiplierTimer = multiplierTimer();
            StartCoroutine("resetMultiplier");
        }
        currentMultiplierTimer = multiplierTimer();

    }
}
