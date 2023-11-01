using Flexalon;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
//using static UnityEditor.Rendering.FilterWindow;

public class NewUIElement : MonoBehaviour
{
    [SerializeField]
    public InventorySlot inventorySlot;
    public TextMeshPro amountTxt;

    private CraftingSystem craftingSystem;

    public GameObject capcule ;
    public GameObject UI_element ;

    // Start is called before the first frame update
    void Start()
    {
        amountTxt = GetComponentInChildren<TextMeshPro>();
        updateAmount();
    }

    public void createElement(Transform parent)
    {
        if (inventorySlot != null)
        {
            // Path to the prefab relative to the project folder
            string prefabPath = "Assets/FleshPit/Inventory/Scripts/New Invnetory/UI_Element.prefab";


            //GameObject prefab = Resources.Load<GameObject>(prefabPath);
            //GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);



            GameObject instance = Instantiate(UI_element);
            //GameObject instance = Instantiate(prefab);

            instance.GetComponent<NewUIElement>().inventorySlot = inventorySlot;

            instance.name = inventorySlot.item.name;
            instance.transform.parent = parent.transform;


            if (inventorySlot.item.gameObject3D != null)
            {
                GameObject v3d = Instantiate(inventorySlot.item.gameObject3D);

                // if atom 
                if (inventorySlot.item.type == ItemType.Atom)
                {
                   
                    //capcule.SetActive(false);

                    v3d.AddComponent<Atom3DObj>();
                    Atom3DObj atom3D = v3d.GetComponent<Atom3DObj>();
                    if (atom3D)
                    {

                        atom3D.atom = inventorySlot.item;
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

                GameObject invGObj = GameObject.Find("~~Inventory~~");

                interactable.Bounds = invGObj.GetComponent<BoxCollider>();
                craftingSystem = invGObj.GetComponent<CraftingSystem>();
            }
        }
    }

    public void updateAmount()
    {
        if (amountTxt != null)
        {
            string x = inventorySlot.amount.ToString();
            amountTxt.text = x;
        }
    }
    void Flexon()
    {
        //gameObject.SetActive(true);

        GameObject invGObj = GameObject.Find("Inventory");
        if (invGObj != null)
        {
            GetComponent<FlexalonInteractable>().Bounds = invGObj.GetComponent<BoxCollider>();
        }

    }

    public void onClickedToCraft()
    {
        if (transform.parent.gameObject.name == "Result Layout")
        {
            Debug.Log("YAAAASSS!");

            GameObject invGObj = GameObject.Find("~~Inventory~~");

            craftingSystem = invGObj.GetComponent<CraftingSystem>();

            craftingSystem.CraftMolecule((MoleculeObject)inventorySlot.item);
        }
        else
            Debug.Log("Nooo!");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
