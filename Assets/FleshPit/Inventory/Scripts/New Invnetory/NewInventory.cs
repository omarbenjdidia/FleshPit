using Flexalon;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewInventory : MonoBehaviour
{
    public GameObject element;
    public GameObject UIInventory;
    public GameObject UIAtoms;
    public GameObject UIEquipment;

    public InventoryObject inventory;

    [SerializeField]
    public NewUIElement x;

    [ContextMenu("Createe Inventory")]
    public void create3DInventory()
    {
        inventory.Container.Items.ForEach(slot =>
        {
            if (slot.item.gameObject3D != null)
            {
                //NewUIElement x = new NewUIElement();
                x.inventorySlot = slot;

                if (slot.item.type == ItemType.Atom)
                    x.createElement(UIAtoms.transform);
                else if (slot.item.type == ItemType.Equipment)
                    x.createElement(UIEquipment.transform);
                else
                    x.createElement(UIInventory.transform);

                //element.GetComponent<UI3DelementSlot>().createElement(transform, slot);
                //new UI3DelementSlot(transform,slot);
            }
            //createElement(slot);
        });
    }
    [ContextMenu("Update Inventory")]
    public void updateForInventory()
    {
        if (inventory != null)
        {
            foreach (InventorySlot slot in inventory.Container.Items)
            {
                var z = transform?.Find(slot.item.name);
                if (z)
                {
                    Debug.Log("FOUND !: " + slot.item.name);
                    z.GetComponent<NewUIElement>().updateAmount();
                }
                else
                {
                    //NewUIElement x = new NewUIElement();
                    x.inventorySlot = slot;
                    if (slot.item.type == ItemType.Atom)
                        x.createElement(UIAtoms.transform);
                    else if (slot.item.type == ItemType.Equipment)
                        x.createElement(UIEquipment.transform);
                    else
                        x.createElement(UIInventory.transform);

                }

            }
        }
        //updateAmountForInventory();
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
        for (int i = 0; i < 3; i++)
        {
            Transform x = parent.GetChild(i);
            while (x.childCount > 0)
            {
                DestroyImmediate(x.GetChild(0).gameObject);

            }
        }
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
