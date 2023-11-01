using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootTarget : MonoBehaviour
{
    public InventoryObject inventory;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("trig");
        if (other.tag == "LootDrop")
        {
            Debug.Log("tagged");
            var item = other.GetComponent<DropFollowPlayer>().item;
            if (item)
            {
                inventory.AddItem(item, 1);
                Destroy(other.gameObject);
            }
        }
    }
}
