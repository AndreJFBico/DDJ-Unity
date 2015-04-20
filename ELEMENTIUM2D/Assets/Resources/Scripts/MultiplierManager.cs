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

    void Start()
    {
        multiplierTimer = () => { return GameManager.Instance.Stats.multiplierTimer; };
        currentMultiplierTimer = multiplierTimer();
        setCurrentMultiplier = (number) => { return (GameManager.Instance.Stats.currentMultiplier = number); };
        getCurrentMultiplier = () => { return GameManager.Instance.Stats.currentMultiplier; };
        multiplierTimerRunning = false;
    }

    private IEnumerator resetMultiplier()
    {
        float increment = 0.1f;
        while(currentMultiplierTimer > 0)
        {
            currentMultiplierTimer -= increment;
            yield return new WaitForSeconds(increment);
        }
        setCurrentMultiplier(0);
        multiplierTimerRunning = false;
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
