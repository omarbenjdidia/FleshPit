using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_3DCraftResult : MonoBehaviour
{

    public List<InventorySlot> itemSlots;
    int oldCount = 0;

    List<GameObject> itemObjects;
    public GameObject element;

    public NewUIElement newEl;

    public UI3dCraftCircle craftCircle;

    public CraftingSystem craftingSystem;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void doTheThing()
    {
        updateResultList();
        destroyChildAllResultObj();
        updateItemObjects();
    }


    [ContextMenu("Update List")]
    public void updateResultList()
    {
        List<ItemObject> list = craftCircle.craftableMolecules();

        itemSlots.Clear();

        list.ForEach(item =>
        {
            itemSlots.Add(new InventorySlot(1, item, 1));

        });
    }

    [ContextMenu("Destory UI")]

    void destroyChildAllResultObj()
    {
        int count = transform.childCount;
        for (int i = 0; i < count; i++)
        {
            GameObject child = transform.GetChild(0).gameObject;
            DestroyImmediate(child);
        }
    }


    [ContextMenu("Update UI")]
    public void updateItemObjects()
    {


        itemSlots.ForEach(slot =>
        {
            Debug.Log("====" + slot.item.name);

            if (slot.item.gameObject3D != null)
            {
                NewUIElement x = newEl;

                x.inventorySlot = slot;
                //x.UI_element = element;
                x.createElement(transform);

                //element.GetComponent<UI3DelementSlot>().createElement(transform, slot);
                //new UI3DelementSlot(transform,slot);
            }
            //createElement(slot);
        });

    }

    // Update is called once per frame
    void Update()
    {

    }
}
