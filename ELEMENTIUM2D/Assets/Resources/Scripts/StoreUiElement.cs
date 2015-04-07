using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

[System.Serializable]
public class StoreUiElement
{
    public string name = "";
    public int maxHorizontalItems = 0;
    [SerializeField]
    private int IncPointer = 0;
    [SerializeField]
    private int DecPointer = 0;
    [SerializeField]
    private int numChecked = 1;
    [SerializeField]
    private List<Image> boxes = new List<Image>();
    [SerializeField]
    private Text points;

    [SerializeField]
    private Sprite notChecked;
    [SerializeField]
    private Sprite Checked;

    public void setPoints(Text p)
    {
        points = p;
        updateText();    
    }

    public void loadCheckedSprites()
    {
        notChecked = Resources.Load<Sprite>("Sprites/UI/itemNonChecked");
        Checked = Resources.Load<Sprite>("Sprites/UI/itemChecked");
    }

    public void addBox(Image box)
    {
        boxes.Add(box);
    }
    
    public bool incrementPointer()
    {
        checkBox(boxes[IncPointer], true);
        DecPointer = IncPointer;
            
        if (IncPointer < boxes.Count - 1)
        {
            IncPointer++;        
        }

        if (numChecked < boxes.Count)
        {
            numChecked++;
            return true;
        }
        return false;
    }

    public bool decrementPointer()
    {
        checkBox(boxes[DecPointer], false);
        IncPointer = DecPointer;

            
        if (DecPointer > 0)
        {
            DecPointer--;                   
        }

        if (numChecked > 0)
        {
            numChecked--;
            return true;
        }
        return false;
    }

    private void checkBox(Image box, bool val)
    {
        if(val)
        {
            box.sprite = Checked;
        }
        else
        {
            box.sprite = notChecked;
        }
    }

    public void updateText()
    {
        points.text = numChecked + "/" + maxHorizontalItems; 
    }

    public void reset()
    {
        boxes.Clear();
        boxes = new List<Image>();
    }

    public int getNumcheck()
    {
        return numChecked;
    }
}
