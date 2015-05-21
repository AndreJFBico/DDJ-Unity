using UnityEngine;
using System.Collections;
using Includes;

public class PostProcessingManager {

    private static PostProcessingManager _instance = null;

    public static PostProcessingManager Instance { get { if (_instance == null) { _instance = new PostProcessingManager(); init(); } return _instance; } }

    private static bool startInterpolating = false;
    private static PostProcessingHelper helper;
    private static ParticleSystem snow;
    private static ParticleSystem fire;
    private static ParticleSystem wind;
    private static ParticleSystem rain;
    private static ParticleSystem currentEffect;

    private static void init()
    {
        helper = Camera.main.GetComponent<PostProcessingHelper>();
        // Neutral background Color
        helper.previousdesiredBackgroundColor = new Color(78.0f / 255.0f, 78.0f / 255.0f, 78.0f / 255.0f, 1.0f);
        helper.desiredBackgroundColor = new Color(78.0f / 255.0f, 78.0f / 255.0f, 78.0f / 255.0f, 1.0f);
        Camera.main.backgroundColor = helper.previousdesiredBackgroundColor;

        
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
        helper.StartCoroutine("changeColor");
    }

    public void sceneInit()
    {
        helper = Camera.main.GetComponent<PostProcessingHelper>();
        // Neutral background Color
        helper.previousdesiredBackgroundColor = new Color(78.0f / 255.0f, 78.0f / 255.0f, 78.0f / 255.0f, 1.0f);
        helper.desiredBackgroundColor = new Color(78.0f / 255.0f, 78.0f / 255.0f, 78.0f / 255.0f, 1.0f);
        Camera.main.backgroundColor = helper.previousdesiredBackgroundColor;

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
        helper.StartCoroutine("changeColor");
    }

    public void updatePostProcessing()
    {
        Elements type = GameManager.Instance.DungeonRoomType();

        switch (type)
        {
            case Elements.NEUTRAL:
                helper.desiredBackgroundColor = new Color(78.0f / 255.0f, 78.0f / 255.0f, 78.0f / 255.0f, 1.0f);
                helper.startChanging();
                currentEffect.enableEmission = false;
                rain.enableEmission = true;
                currentEffect = rain;
                currentEffect.Play();
                break;

            case Elements.FIRE:
                helper.desiredBackgroundColor = new Color(65.0f / 255.0f, 20.0f / 255.0f, 10.0f / 255.0f, 1.0f);
                helper.startChanging();
                currentEffect.enableEmission = false;
                fire.enableEmission = true;
                currentEffect = fire;
                currentEffect.Play();
                break;

            case Elements.WATER:
                helper.desiredBackgroundColor = new Color(29.0f / 255.0f, 42.0f / 255.0f, 71.0f / 255.0f, 1.0f);
                helper.startChanging();
                currentEffect.enableEmission = false;
                snow.enableEmission = true;
                currentEffect = snow;
                currentEffect.Play();
                break;

            case Elements.EARTH:
                helper.desiredBackgroundColor = new Color(28.0f / 255.0f, 44.0f / 255.0f, 29.0f / 255.0f, 1.0f);
                helper.startChanging();
                currentEffect.enableEmission = false;
                wind.enableEmission = true;
                currentEffect = wind;
                currentEffect.Play();
                break;
        }
    }
}
