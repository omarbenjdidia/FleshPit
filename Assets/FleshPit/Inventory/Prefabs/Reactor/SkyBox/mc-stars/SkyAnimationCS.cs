using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyAnimationCS : MonoBehaviour
{

    public float rotateSpeed = 1.0f; 

    void Update()
    {

        transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);
    }
}
