using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragItem : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [HideInInspector]
    public Transform parent;

    private Image image;
    public TextMeshProUGUI amounttxt;

    [HideInInspector]
    public ItemObject itemObject;

    [HideInInspector]
    public InventorySlot inventorySlot;


    //public Item item;

    //public void SetItem(Item _item)
    //{
    //    this.item = _item;
    //    gameObject.name = item.Name;
    //}
    //public Item GetItem()
    //{
    //    return this.item;
    //}

    public void setUISlot(InventorySlot slot)
    {
        this.inventorySlot = slot;
        SetItem(slot.item);
        //refreshImage();

        updateAmounttxt();

    }

    public void updateAmounttxt()
    {
        if (amounttxt||inventorySlot!=null) amounttxt.text = inventorySlot.amount.ToString();
        else Debug.Log("ddd");
    }

    public void SetItem(ItemObject _itemObject)
    {
        itemObject = _itemObject;
        gameObject.name = itemObject.name;
        refreshImage();
    }
    public ItemObject GetItem()
    {
        return this.itemObject;
    }


    void refreshImage()
    {
        if (inventorySlot!=null)
        {
            image.sprite = inventorySlot.item.uiDisplay;
        }
    }


    private void Start()
    {
        image = gameObject.GetComponent<Image>();

        if (itemObject != null)
        {
            SetItem(itemObject);
            //refreshImage();
        }

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        parent = transform.parent;
        //rectTransform.SetSiblingIndex(3);
        transform.SetParent(parent.parent);
        transform.SetAsLastSibling();
        image.raycastTarget = false;
    }


    public void OnDrag(PointerEventData eventData)
    {
        Vector2 position = Input.mousePosition;
        transform.position = position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(parent);
        image.raycastTarget = true;


    }

    private void Update()
    {

        //Vector2 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Vector2 position = Input.mousePosition;
        //Debug.Log(position.x + "~==~" + position.y);
    }

}
