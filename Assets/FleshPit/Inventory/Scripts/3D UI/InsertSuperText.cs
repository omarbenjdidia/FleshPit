using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsertSuperText : MonoBehaviour
{
    public SuperTextMesh SuperTextMesh;
    public string[] listText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

   public void setAboutText(UI3DelementSlot slot)
    {
        SuperTextMesh.text= slot.itemObject.description;
    }
   public void setAboutText(string text)
    {
        SuperTextMesh.text= text;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            setAboutText("hahahaha");
        }
    }
}
