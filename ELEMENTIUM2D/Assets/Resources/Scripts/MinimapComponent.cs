using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Sprite))]
public class MinimapComponent : MonoBehaviour {

    public enum Type { OBSTACLE, PLAYER, ENEMY, SPAWNER, PROP, STARTING, NOTCLEARED, ENDING, CLEARED }
    public Type type;

    private Sprite sprite;

    public void resetSpriteRenderer()
    {
        analiseType();
    }

    void analiseType()
    {
        SpriteRenderer spRenderer = gameObject.GetComponent<SpriteRenderer>();
        switch (type)
        {
            case Type.OBSTACLE:
                spRenderer.color = Color.white;
                break;
            case Type.PLAYER:
                spRenderer.color = Color.cyan;
                spRenderer.sortingOrder = 1000;
                break;
            case Type.ENEMY:
                spRenderer.color = Color.red;
                break;
            case Type.SPAWNER:
                spRenderer.color = Color.magenta;
                break;
            case Type.PROP:
                spRenderer.color = Color.yellow;
                break;
            case Type.STARTING:
                spRenderer.color = Color.blue / 1.2f;
                break;
            case Type.ENDING:
                spRenderer.color = Color.yellow / 1.2f;
                break;
            case Type.CLEARED:
                spRenderer.color = Color.green;
                break;
            case Type.NOTCLEARED:
                spRenderer.color = Color.red / 1.2f;
                break;
            default:
                break;
        }
    }

	// Use this for initialization
	void Start () {
        analiseType();
	}
}
