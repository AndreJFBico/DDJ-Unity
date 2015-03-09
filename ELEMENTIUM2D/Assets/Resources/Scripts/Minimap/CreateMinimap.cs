using UnityEngine;
using System.Collections;
using System.IO;
       
  
public class CreateMinimap : MonoBehaviour {

    public float minX = Mathf.Infinity;
    public float minY = Mathf.Infinity;
    public float maxX = Mathf.NegativeInfinity;
    public float maxY = Mathf.NegativeInfinity;

    public float width = 0;
    public float height = 0;

    public float ratio;

    public int texWidth = 0;
    public int texHeight = 0;

    private Texture2D minimap;

    private float screenWidth;
    private float screenHeight;

    private bool textureChanged = false;
    private int textureResolution = 500;

	// Use this for initialization
	void Awake () {
        Transform[] alltransforms = FindObjectsOfType<Transform>() as Transform[];
        foreach (Transform item in alltransforms)
        {
            if (item.GetComponent<Renderer>() == null)
                continue;
            float xL = item.GetComponent<Renderer>().bounds.min.x;
            float xH = item.GetComponent<Renderer>().bounds.max.x;
            float yL = item.GetComponent<Renderer>().bounds.min.z;
            float yH = item.GetComponent<Renderer>().bounds.max.z;

            if (xL < minX)
                minX = xL;
            if (xH > maxX)
                maxX = xH;
            if (yL < minY)
                minY = yL;
            if (yH > maxY)
                maxY = yH;
        }
        width = maxX - minX;
        height = maxY - minY;

        ratio = width / height;

        texWidth = textureResolution;
        texHeight = (int)(textureResolution* (1.0f /ratio)) + 1;

        minimap = new Texture2D(texWidth, texHeight);
        int k = texWidth * texHeight;
        Color[] colors = new Color[k];
        for (int i = 0; i < k; i++ )
        {
            colors[i] = Color.black;
        }
        minimap.SetPixels(0, 0, texWidth, texHeight, colors);
        minimap.Apply();

        screenHeight = Screen.height;
        screenWidth = Screen.width;
	}
	
    void Start()
    {
        StartCoroutine("applySetPixels");
    }

    void OnGUI()
    {
        GUI.DrawTexture(new Rect(20, screenHeight - 200, 250, 200), minimap, ScaleMode.ScaleToFit);
    }

    IEnumerator applySetPixels()
    {
        while (true)
        {
            if (textureChanged)
            {
                minimap.Apply();
                textureChanged = false;
            }
            yield return new WaitForSeconds(1);
        }
    }

    public void updateTexture(int posX, int posY, int width, int height, Color[] color)
    {
        textureChanged = true;
        minimap.SetPixels(posX - width/2, posY - height/2, width, height, color);
    }


    void SaveTextureToFile( Texture2D texture, string fileName)
  {
     byte[] bytes=texture.EncodeToPNG();
     Stream file = File.Open(Application.dataPath + "/"+fileName,FileMode.Create);
     var binary = new BinaryWriter(file);
     binary.Write(bytes);
     file.Close();
  }
	// Update is called once per frame
	void Update () {
	    if(Input.GetKeyDown(KeyCode.P))
        {
            SaveTextureToFile(minimap, "lewl.png");
        }
	}
}
