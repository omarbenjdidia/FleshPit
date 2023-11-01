using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIInventorySlot : MonoBehaviour, IDropHandler
{
    // Start is called before the first frame update
    public void OnDrop(PointerEventData eventData)
    {

        DragItem myItem = GetComponentInChildren<DragItem>();

        GameObject dropped = eventData.pointerDrag;
        DragItem DroppedItem = dropped.GetComponent<DragItem>();

        ItemObject varItem = DroppedItem.itemObject;
        InventorySlot slot = DroppedItem.inventorySlot;


        DroppedItem.setUISlot(myItem.inventorySlot);
        myItem.setUISlot(slot);
        //DroppedItem.SetItem( myItem.itemObject);
        //myItem.SetItem(varItem);

        //dragItem.parent = transform;
        //myItem.parent = dropped.transform;
    }
}
