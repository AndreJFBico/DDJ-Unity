using UnityEngine;
using System.Collections;

public class PostProcessingHelper : MonoBehaviour
{
    public Color previousdesiredBackgroundColor;
    public Color desiredBackgroundColor;

    private float t = 0.0f;
    private float duration = 0.5f;
    private bool changing = false;

    IEnumerator changeColor()
    {
        while (true)
        {
            if (t <= 1.0f)
            {
                if (changing)
                {
                    t += Time.deltaTime / duration;
                }
            }
            else
            {
                previousdesiredBackgroundColor = desiredBackgroundColor;
                t = 0;
                changing = false;
            }
            Camera.main.backgroundColor = Color.Lerp(previousdesiredBackgroundColor, desiredBackgroundColor, t);
            yield return new WaitForSeconds(0.05f);
        }
    }

    public void startChanging()
    {
        changing = true;
    }
}
