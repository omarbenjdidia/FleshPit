using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using Mirror;

public class enemyBullet : MonoBehaviour
{

    //public Rigidbody rb;
    //public float speed=5f;
    // Start is called before the first frame update
    void Start()
    {

        //rb.velocity = transform.forward * speed;  



    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnTriggerEnter(Collider collision)
    {
       
        if (collision.gameObject.tag == "Player")
        {           
            Debug.Log("I shot the bullet!");

            collision.gameObject.GetComponent<PlayerControllerWithFreeLookCamera>().Myhealth -= 2f;
            Destroy(gameObject);


        }

    }




}
