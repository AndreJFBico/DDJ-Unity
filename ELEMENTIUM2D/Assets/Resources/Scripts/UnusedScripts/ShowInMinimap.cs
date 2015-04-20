using UnityEngine;
using System.Collections;

public class ShowInMinimap : MonoBehaviour {

    private Sprite sprite;
    private SpriteRenderer spriteR;

    public void init(Sprite sp, SpriteRenderer spR)
    {
        spriteR = spR;
        sprite = sp;
    }

    void OnWillRenderObject()
    {
        if (Camera.current == Camera.main)
        {
            spriteR.sprite = sprite;
            Destroy(this);
        }
    }
}
