using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageText : MonoBehaviour
{

    public Color[] colors;
    // Start is called before the first frame update
    void Start()
    {
        changeColor();
        Destroy(gameObject,1);
    }

    public void changeColor()
    {
        GetComponentInChildren<TextMeshPro>().color= colors[Random.Range(1,colors.Length)];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
