using UnityEngine;
using System.Collections;

public class MinimapComponent : MonoBehaviour {

    public enum Type { OBSTACLE, PLAYER, ENEMY, SPAWNER, PROP}
    public Type type;

    private Sprite sprite;

	// Use this for initialization
	void Start () {
        sprite = Resources.Load<Sprite>("Sprites/square");
        SpriteRenderer spRenderer = gameObject.AddComponent<SpriteRenderer>();
        spRenderer.sprite = sprite;
        switch (type)
        {
            case Type.OBSTACLE:
                spRenderer.color = Color.white;
                break;
            case Type.PLAYER:
                spRenderer.color = Color.blue;
                spRenderer.sortingOrder = 1000;
                break;
            case Type.ENEMY:
                spRenderer.color = Color.red;
                break;
            case Type.SPAWNER:
                spRenderer.color = Color.grey;
                break;
            case Type.PROP:
                spRenderer.color = Color.yellow;
                break;
            default:
                break;
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
