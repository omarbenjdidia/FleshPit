using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Descriptionpanel : MonoBehaviour
{

    public TextMeshProUGUI nameTxt;
    public TextMeshProUGUI DescriptionTxt;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateDescription(UI3DelementSlot uI3Delement)
    {
        Debug.Log("ELEMEENT"+ uI3Delement.name);
        nameTxt.text = "Name: " + uI3Delement.itemObject.name;
        DescriptionTxt.text = "Desctiption: " + uI3Delement.itemObject.description;
    }
}
