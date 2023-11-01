using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarMonster : MonoBehaviour
{
    public float health;
    public Slider slider;
    void Start()
    {
        health = 50f;
        slider = GetComponent<Slider>(); 
    }

    private void Update()
    {
        if(health<=0)
            Destroy(gameObject.transform.parent.gameObject.transform.parent.gameObject);
    }
}
