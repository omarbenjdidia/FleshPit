using System.Collections;
using System.Collections.Generic;
using Unity.Jobs;
using UnityEngine;

public class cellinter : MonoBehaviour
{
    public GameObject InfoB;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "default")
        {
            InfoB.SetActive(true);

        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "default")
        {
            InfoB.gameObject.SetActive(false);

        }
    }
}
