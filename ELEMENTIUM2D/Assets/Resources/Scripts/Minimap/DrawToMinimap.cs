using UnityEngine;
using System.Collections;

public class DrawToMinimap : MonoBehaviour {

    private float gameWidth;
    private float gameHeight;
    private float texWidth;
    private float texHeight;

    public float minX;
    public float minY;

    private CreateMinimap minimap;

    private int width = 3;
    private int height = 3;

    private float ratio;

    private Color[] colors;

	// Use this for initialization
	void Start () {
        minimap = GameObject.FindWithTag("Minimap").GetComponent<CreateMinimap>();
        gameWidth = minimap.width;
        gameHeight = minimap.height;
        minX = minimap.minX;
        minY = minimap.minY;
        texWidth = minimap.texWidth;
        texHeight = minimap.texHeight;
        ratio = minimap.ratio;

        int aux = width * height;

        colors = new Color[aux];
        for (int i = 0; i < aux; i++)
        {
            colors[i] = Color.white;
        }
	}

    void OnWillRenderObject()
    {
        float myPosX = transform.position.x - minX;
        float myPosY = transform.position.y - minY;

        float percX = myPosX / gameWidth;
        float percY = myPosY / gameHeight;

        Debug.Log(" PercX: " + percX + " PercY: " + percY);

        minimap.updateTexture((int)Mathf.Floor(percX * texWidth), (int)Mathf.Floor(percY * texHeight), width, height, colors);

        Destroy(gameObject.GetComponent<DrawToMinimap>());

    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
