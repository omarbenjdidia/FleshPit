using Flexalon;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI3DelementSlot : MonoBehaviour
{
    [HideInInspector]
    public InventorySlot inventorySlot;

    public ItemObject itemObject;

    public TextMeshPro amountTxt;
    public GameObject element;
    // Start is called before the first frame update

    public UI3DelementSlot(Transform parent, InventorySlot _inventorySlot)
    {
        createElement(parent, _inventorySlot);
    }
    public UI3DelementSlot()
    {
        if (inventorySlot != null)
            itemObject = inventorySlot.item;
    }

    void Start()
    {
        if (inventorySlot != null)
        {
            itemObject = inventorySlot.item;
            updateAmount();
        }
        //AddChildColliderToParent(); 


    }

    void AddChildColliderToParent()
    {
        Collider childCollider = GetComponentInChildren<Collider>();
        if (childCollider == null) return; // Check if child has a collider

        Collider copiedCollider = gameObject.AddComponent<Collider>();
        copiedCollider = childCollider;
        copiedCollider.isTrigger = childCollider.isTrigger;
        copiedCollider.sharedMaterial = childCollider.sharedMaterial;
    }

    public void updateAmount(int amount)
    {
        string x = amount.ToString();

        amountTxt.text = x;
    }
    public void updateAmount()
    {
        string x = inventorySlot.amount.ToString();

        amountTxt.text = x;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("3asbaaa" + other.gameObject.name);
    }

    void OnCollisionEnter(Collision other)
    {
        Debug.Log("coll");
        if (other.gameObject.tag == "test")
        {
            Debug.Log("Triggring UI3d atom");


            UI3DelementSlot x = other.gameObject.GetComponent<UI3DelementSlot>();
            UI3DelementSlot parent = other.gameObject.GetComponentInParent<UI3DelementSlot>();
            if (x)
            {
                if (x.itemObject = parent.itemObject)
                {
                    Debug.Log("Added");

                    parent.inventorySlot.amount += x.inventorySlot.amount;
                    parent.updateAmount();
                }
            }
        }
    }

    public void onClickToCraft()
    {
        Debug.Log("clicked");
        if (gameObject.GetComponentInParent<UI_3DCraftResult>() != null)
            if (itemObject.type == ItemType.Molecule)
            {
                Debug.Log("clicked Craft");
                CraftingSystem craftingSystem = GetComponentInParent<CraftingSystem>();
                craftingSystem.CraftMolecule((MoleculeObject)itemObject);
            }
    }

    private void OnTransformParentChanged()
    {
        Debug.Log("New parent transform: ");

        //Debug.Log("New parent transform: " + transform.parent.name);
    }

    public void createElement(Transform parent, InventorySlot _inventorySlot)
    {
        inventorySlot = _inventorySlot;

        //GameObject myPrefab = Resources.Load<GameObject>("Assets/FleshPit/Inventory/Prefabs/UI things/3D Element.prefab");

        //GameObject instance = element;
        GameObject instance = Instantiate(element);

        instance.name = inventorySlot.item.name;
        instance.transform.parent = parent;

        Debug.Log("d5alt ll ~~");

        if (_inventorySlot.item.gameObject3D != null)
        {
            GameObject v3d = Instantiate(_inventorySlot.item.gameObject3D);

            Debug.Log("d5alt ll" + _inventorySlot.item.type.ToString());

            // if atom 
            if (_inventorySlot.item.type == ItemType.Atom)
            {
                Debug.Log("d5alt ll add");

                v3d.AddComponent<Atom3DObj>();
                Atom3DObj atom3D = v3d.GetComponent<Atom3DObj>();
                if (atom3D)
                {

                    Debug.Log("d5alt ll !null");
                    atom3D.atom = _inventorySlot.item;
                    atom3D.changeMaterielColor();

                }
                else
                    Debug.Log("nulll");
            }

            updateAmount();


            // setting in the grid
            v3d.name = inventorySlot.item.name + "~3D";
            v3d.transform.SetParent(instance.transform);
            v3d.transform.position = v3d.transform.parent.position;


            // add il collider ll mouse
            FlexalonInteractable interactable = instance.GetComponent<FlexalonInteractable>();
            interactable.Collider = v3d.GetComponent<Collider>();
            //interactable.Collider = v3d.GetComponent<BoxCollider>();
        }
    }
}
