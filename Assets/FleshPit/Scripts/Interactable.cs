using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public GameObject popUpbox;
    public GameObject InfoB;
   
   
    //public TMP_Text popUpText ;
    public void PopUp(string text)
    {
        popUpbox.SetActive(true);
         InfoB.SetActive(false);
        //popUpText.text = text;

    }
    // Start is called before the first frame update
    void Start()
    {
        popUpbox.SetActive(false);
        InfoB.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void close ()
    {
        popUpbox.SetActive(false);  
    }
   

}
