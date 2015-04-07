using UnityEngine;
using System.Collections;

public class ElementiumMonoBehaviour : MonoBehaviour {

    bool inited = false;

    public virtual void Disable() { }
    public virtual void Init() { }
    public virtual void Enable() { }

    public void tryInitialize()
    {
        if (!inited)
        {
            Init();
            inited = true;
        }
        else Enable();
    }
}
