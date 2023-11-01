using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bacSkill : MonoBehaviour
{
    public GameObject VFX;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }
    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("I exploded!");
            other.gameObject.GetComponent<Rigidbody>().AddExplosionForce(100f, transform.position, 10f, 3.0F);
            GameObject vfx = Instantiate(VFX, transform.position, transform.rotation);
            //NetworkServer.Spawn(vfx);
            Destroy(vfx, 1.5f);
            Destroy(gameObject);

            other.gameObject.GetComponent<PlayerControllerWithFreeLookCamera>().Myhealth -= 10f;
            

        }
    }
}
