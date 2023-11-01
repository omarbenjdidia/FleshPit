using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;

public class MukSkill : MonoBehaviour
{
    public GameObject VFX;
    // Start is called before the first frame update
    void Start()
    {
    }

    public void OnTriggerEnter(Collider collision)
    {

        if (collision.gameObject.tag == "Player"&& GameObject.Find("EarthShatter Variant(Clone)")== null)
        {
            Debug.Log("I did earth quake!");
            collision.gameObject.GetComponent<Rigidbody>();
            GameObject vfx = Instantiate(VFX, transform.position+ new Vector3(0f, 2.5f, 0f), transform.rotation);
            //NetworkServer.Spawn(vfx);
            Destroy(vfx, 0.5f);

            collision.gameObject.GetComponent<PlayerControllerWithFreeLookCamera>().Myhealth -= 3f;
        }

    }

}


