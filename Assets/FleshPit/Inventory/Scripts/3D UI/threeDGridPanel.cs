using Flexalon;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;

public class threeDGridPanel : MonoBehaviour
{
    //public GameObject prefab;
    public GameObject element;

    public InventoryObject inventory;
    // Start is called before the first frame update
    void Start()
    {

    }

    [ContextMenu("Update Inventory")]
    public void updateForInventory()
    {
        if (inventory != null)
        {
            foreach (InventorySlot slot in inventory.Container.Items)
            {
                var x = transform?.Find(slot.item.name);
                if (x)
                {
                    x.GetComponent<UI3DelementSlot>().updateAmount(slot.amount);
                }
                else
                    element.GetComponent<UI3DelementSlot>().createElement(transform, slot);

            }
        }
        //updateAmountForInventory();
    }
    void updateAmountForInventory()
    {
        foreach (Transform child in transform)
        {
            // Do something with each child object
            Debug.Log("Child object name: " + child.name);

            UI3DelementSlot slot3D = child.gameObject.GetComponent<UI3DelementSlot>();
            if (slot3D)
            {
                var y = inventory.Container.Items.Find(x => x.ID == slot3D.inventorySlot.ID);
                if (y != null)
                    slot3D.updateAmount(y.amount);
                else
                    element.GetComponent<UI3DelementSlot>().createElement(transform, y);


            }


        };

    }
    [ContextMenu("Recreate Inventory")]
    public void Recreate()
    {
        DestroyAllChildren();
        create3DInventory();
    }
    
        [ContextMenu("Destroy Inventory")]
    public void DestroyAllChildren()
    {
        Transform parent = transform;
        while (parent.childCount>0)
        {
            DestroyImmediate(parent.GetChild(0).gameObject);

        }

        //foreach (Transform child in parent)
        //{
        //    DestroyImmediate(child.gameObject);
        //};
    }

    [ContextMenu("Createe Inventory")]
    public void create3DInventory()
    {
        inventory.Container.Items.ForEach(slot =>
        {
            if (slot.item.gameObject3D != null)
            {
                element.GetComponent<UI3DelementSlot>().createElement(transform, slot);
                //new UI3DelementSlot(transform,slot);
            }
            //createElement(slot);
        });
    }

    void createElement(InventorySlot inventorySlot)
    {
        //GameObject instance = element;
        GameObject instance = Instantiate(element);
        instance.name = inventorySlot.item.name;
        instance.transform.parent = transform;

        instance.GetComponent<UI3DelementSlot>().inventorySlot = inventorySlot;

        if (inventorySlot.item.gameObject3D != null)
        {
            GameObject v3d = Instantiate(inventorySlot.item.gameObject3D);

            if (inventorySlot.item.type == ItemType.Atom)
            {
                Atom3DObj atom3D = v3d.GetComponent<Atom3DObj>();
                if (atom3D != null)
                {
                    atom3D.atom = inventorySlot.item;

                }
                else
                    Debug.Log("nulll");
            }



            v3d.name = inventorySlot.item.name + "~3D";
            v3d.transform.SetParent(instance.transform);
            v3d.transform.position = v3d.transform.parent.position;

            FlexalonInteractable interactable = instance.GetComponent<FlexalonInteractable>();
            interactable.Collider = v3d.GetComponent<Collider>();
            //interactable.Collider = v3d.GetComponent<BoxCollider>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            updateForInventory();
        }
    }
}
