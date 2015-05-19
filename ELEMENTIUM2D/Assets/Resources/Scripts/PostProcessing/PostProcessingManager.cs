using UnityEngine;
using System.Collections;
using Includes;

public class PostProcessingManager {

    private static PostProcessingManager _instance = null;

    public static PostProcessingManager Instance { get { if (_instance == null) { _instance = new PostProcessingManager(); init(); } return _instance; } }

    private static ParticleSystem snow;
    private static ParticleSystem fire;
    private static ParticleSystem wind;
    private static ParticleSystem rain;
    private static ParticleSystem currentEffect;

    private static void init()
    {
        rain = GameObject.Find("Rain").transform.GetComponent<ParticleSystem>();
        snow = GameObject.Find("Snow").transform.GetComponent<ParticleSystem>();
        fire = GameObject.Find("Fire").transform.GetComponent<ParticleSystem>();
        wind = GameObject.Find("Wind").transform.GetComponent<ParticleSystem>();
        currentEffect = rain;
        rain.enableEmission = true;
        snow.enableEmission = false;
        fire.enableEmission = false;
        wind.enableEmission = false;
        currentEffect.Play();
    }

    public void sceneInit()
    {
        rain = GameObject.Find("Rain").transform.GetComponent<ParticleSystem>();
        snow = GameObject.Find("Snow").transform.GetComponent<ParticleSystem>();
        fire = GameObject.Find("Fire").transform.GetComponent<ParticleSystem>();
        wind = GameObject.Find("Wind").transform.GetComponent<ParticleSystem>();
        currentEffect = rain;
        rain.enableEmission = true;
        snow.enableEmission = false;
        fire.enableEmission = false;
        wind.enableEmission = false;
        currentEffect.Play();
    }


    public void updatePostProcessing()
    {
        Elements type = GameManager.Instance.DungeonRoomType();

        switch (type)
        {
            case Elements.NEUTRAL:
                currentEffect.enableEmission = false;
                rain.enableEmission = true;
                currentEffect = rain;
                currentEffect.Play();
                break;
            case Elements.FIRE:
                currentEffect.enableEmission = false;
                fire.enableEmission = true;
                currentEffect = fire;
                currentEffect.Play();
                break;
            case Elements.WATER:
                currentEffect.enableEmission = false;
                snow.enableEmission = true;
                currentEffect = snow;
                currentEffect.Play();
                break;

            case Elements.EARTH:
                currentEffect.enableEmission = false;
                wind.enableEmission = true;
                currentEffect = wind;
                currentEffect.Play();
                break;
        }
    }
}
