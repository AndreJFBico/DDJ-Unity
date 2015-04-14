using UnityEngine;
using System.Collections;

public class TextFloat : MonoBehaviour {

    private TextMesh text;
    private float maxDuration;
    private float duration;

	// Use this for initialization
	void Awake () {
        text = GetComponent<TextMesh>();
        maxDuration = 0.4f;
        duration = maxDuration;
	}

    public void startFloating(string message, Color color)
    {
        duration = maxDuration;
        text.text = message;
        text.color = color;
    }

    void Update()
    {
        duration -= Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, transform.position + Vector3.forward, 0.01f);
        if(duration < 0)
            FloatingText.Instance.returnText(this);
    }
}
