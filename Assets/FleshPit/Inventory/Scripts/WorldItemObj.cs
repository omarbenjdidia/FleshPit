using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldItemObj : MonoBehaviour
{
    public ItemObject itemObject;
    // Start is called before the first frame update
    public float rotationSpeed = 50f;

    public bool moveSin = false;

    public float movementSpeed = 1.0f;
    public float movementRange = 1.0f;


    private float startY;
    private void Awake()
    {
        startY = transform.position.y;
    }

    void moveObjSin()
    {

        float newY = startY + Mathf.Sin(Time.time * movementSpeed) * movementRange;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);

    }
    private void Update()
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
        if (moveSin)
        {
            moveObjSin();
        }
    }
}

