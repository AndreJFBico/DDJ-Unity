using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class IntroText : MonoBehaviour {

    public float initTime;
    public float duration;
    public bool final = false;
    public bool statik = false;
    public string text;


	// Use this for initialization
    void Start()
    {
        Invoke("init", initTime);
        gameObject.SetActive(false);
	}
	
    private void init()
    {
        gameObject.SetActive(true);
        GetComponent<Text>().text = text;
        StartCoroutine("increaseSize");
    }
    
    IEnumerator increaseSize()
    {
        float delta = 0.02f;
        float startduration = duration;
        while(duration > 0)
        {
            yield return new WaitForSeconds(delta);
            duration -= delta;
            if(!statik)
                transform.localScale *= 1.005f;
            if(duration < (startduration/2))
            {
                Color c = GetComponent<Text>().color;
                c.a -= 0.01f;
                GetComponent<Text>().color = c; 
            }
        }
        if (final)
        {
            Application.LoadLevel("defaultScene");
        }
        gameObject.SetActive(false);
    }
}
