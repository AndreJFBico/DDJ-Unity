using UnityEngine;
using System.Collections;
using System.Reflection;
using System;
using TelegraphEffect;

[System.Serializable]
public class Telegraph : MonoBehaviour {

    Sprite sprite;
    public Transform particleSystem;

    public TelegraphInit initialization;
    [SerializeField]
    public int initIndex;
    [SerializeField]
    public string initScrpt;

    [SerializeField]
    public float timeBetweenTelegraphs = 1.5f;

    public int damageValue = 10;

    public TelegraphDamage damage;
    [SerializeField]
    public int damageIndex;
    [SerializeField]
    public string damScrpt;


    public TelegraphMotion motion;
    [SerializeField]
    public int motionIndex;
    [SerializeField]
    public string motScrpt;


    public float priority = 1;
    public float duration = 4.0f;
    bool moving = false;
    float timer = 0.0f;

    Transform sObj;
    Transform player;

    public static Type FindTypeInLoadedAssemblies(string typeName)
    {
        Type _type = null;
        foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
        {
            _type = assembly.GetType(typeName);
            if (_type != null)
                break;
        }

        return _type;
    }


    void Update()
    {
        if(moving)
        {
            motion.applyMotion(sObj, transform);
        }
        if (timer >= duration + timeBetweenTelegraphs)
        {
            transform.FindChild("Colliders").gameObject.SetActive(true);
            particleSystem.gameObject.SetActive(false);
            gameObject.SetActive(false);
            timer = 0.0f;
            moving = false;
            damage.deltDamage = false;
        }
        else
        if (timer >= duration)
        {
            if(player!= null )
            {
                damage.damage(player, damageValue);
            }
            //hides the sprite
            transform.FindChild("Colliders").gameObject.SetActive(false);
            GetComponentInChildren<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.0f);
            particleSystem.gameObject.SetActive(true);
        }
        else GetComponentInChildren<SpriteRenderer>().color = new Color(1f, 1f, 1f, timer / duration);
        timer += Time.deltaTime;
    }

    public void init(Transform startingObject, Transform currentFireTransform)
    {
        // INIT
        Type type = FindTypeInLoadedAssemblies("TelegraphEffect." + initScrpt);
        initialization = (TelegraphInit)Activator.CreateInstance(type);

        // DAMAGE
        type = FindTypeInLoadedAssemblies("TelegraphEffect." + damScrpt);
        damage = (TelegraphDamage)Activator.CreateInstance(type);

        // MOTION
        type = FindTypeInLoadedAssemblies("TelegraphEffect." + motScrpt);
        motion = (TelegraphMotion)Activator.CreateInstance(type);

        sprite = GetComponent<Sprite>();
        initialization.init(startingObject, currentFireTransform, transform);
        sObj = startingObject;
    }

    public void setupMotion()
    {
        moving = true;
    }

    void OnTriggerEnter(Collider collider)
    {
        if(collider.tag.CompareTo("Player") == 0 && timer <= duration)
        {
            player = collider.transform;
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.tag.CompareTo("Player") == 0)
        {
            player = null;
        }
    }
}
