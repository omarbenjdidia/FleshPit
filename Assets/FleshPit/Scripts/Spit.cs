using System.Collections;
using System.Collections.Generic;
using System.Security.Claims;
using UnityEngine;

public class Spit : MonoBehaviour
{
    public GameObject VFX;
    private GameObject _destroyvfx;
    public GameObject Character;
    void Start()
    {
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag== "Character")
        {
            _destroyvfx = Instantiate(VFX, (new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z)), transform.rotation);
            Destroy(gameObject);
            Destroy(_destroyvfx, 0.8f);
        }

    }

}
