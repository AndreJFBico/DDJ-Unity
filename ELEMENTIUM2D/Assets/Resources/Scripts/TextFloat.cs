using UnityEngine;
using System.Collections;

public class TextFloat : MonoBehaviour {

    private TextMesh text;
    private float textSpeed;
    private float defDuration;
    private float maxDuration;
    private float duration;
    private TextMesh childText;

	// Use this for initialization
	void Awake () {
        textSpeed = 0.01f;
        text = GetComponent<TextMesh>();
        childText = transform.FindChild("Child").GetComponent<TextMesh>();
        defDuration = 0.4f;
        maxDuration = defDuration;
        duration = defDuration;
	}

    public void startFloating(string message, Color color)
    {
        maxDuration = defDuration;
        duration = defDuration;
        text.text = message;
        childText.text = message;
        text.color = color;
    }

    public void startFloating(string message, Color color, float time)
    {
        maxDuration = time;
        duration = time;
        text.text = message;
        childText.text = message;
        text.color = color;
    }

    void Update()
    {
        duration -= Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, transform.position + Vector3.forward, (defDuration/maxDuration)*0.01f);
        if(duration < 0)
            FloatingText.Instance.returnText(this);
    }
}
