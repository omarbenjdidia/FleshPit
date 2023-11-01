using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class UIInventory : MonoBehaviour
{
    [SerializeField]
    public InventoryObject inventoryObject;

    public ItemObject empty;
    // Start is called before the first frame update
    void Start()
    {
        updateUIInventory();
    }
    public GameObject parentPrefab;

    private void OnEnable()
    {
        EvenHandlerRamy.refreshInventoryUI += reloadTrigger;
    }
    private void OnDisable()
    {
        EvenHandlerRamy.refreshInventoryUI -= reloadTrigger;
    }
    public void reloadTrigger()
    {
        Debug.Log("reloadingg");
        updateUIInventory();
    }

    private void Update()
    {
        //updateUIInventory();

    }
    public void updateUIInventory()
    {
        for (int i = 0; i < inventoryObject.Container.Items.Count; i++)
        {
            //Debug.Log(
            //    inventoryObject.Container.Items[i].item.name
            //    );
            setUISlot(i, inventoryObject.Container.Items[i].item, inventoryObject.Container.Items[i]);
        }
        //for (int i = inventoryObject.Container.Items.Count; i <= 20; i++)
        //{
        //    setUISlot(i, empty);

        //}
    }

    void setUISlot(int num, ItemObject item,InventorySlot inventorySlot)
    {
        GameObject childPrefab = parentPrefab.transform.GetChild(num).gameObject;
        // Do something with the childPrefab
        DragItem dragItem = childPrefab.GetComponentInChildren<DragItem>();
        //dragItem.SetItem(item);
        dragItem.setUISlot(inventorySlot);
    }
    ItemObject getUISlot(int num)
    {
        GameObject childPrefab = parentPrefab.transform.GetChild(num).gameObject;
        // Do something with the childPrefab
        DragItem dragItem = childPrefab.GetComponentInChildren<DragItem>();
        return dragItem.GetItem();
    }
}
