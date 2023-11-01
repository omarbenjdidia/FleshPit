using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI3dCraftCircle : MonoBehaviour
{
    public List<ItemObject> atomItems = new List<ItemObject>();

    [SerializeField]
    public CraftingSystem craftingSystem;
    int oldCount = 0;

    public UI_3DCraftResult craftResult;

    // Start is called before the first frame update
    void Start()
    {
        oldCount = transform.childCount;

    }

    [ContextMenu("Craftable List")]
    public List<ItemObject> craftableMolecules()
    {
        Debug.Log("Craftbale list ");
        Debug.Log(atomItems.Count);

        List<ItemObject> craftableMoleculesList = craftingSystem.craftableMolecules(atomItems);
        if (craftableMoleculesList.Count == 0)
        {
            //omar line
            Debug.Log(craftableMoleculesList.Count + " : From Omar");

            Debug.Log("craftableMoleculesList far8aa");
        }
        else
            craftableMoleculesList.ForEach(mol =>
            {
                Debug.Log(mol.name);
            });
        return craftableMoleculesList;
    }
    [ContextMenu("update list Craft")]
    public List<ItemObject> getItems()
    {
        List<ItemObject> atomItemsAux = new List<ItemObject>();

        int count = transform.childCount;

        for (int i = 0; i < count; i++)
        {
            GameObject child = transform.GetChild(i).gameObject;
            //UI3DelementSlot elemtSlot = child.GetComponent<UI3DelementSlot>();
            NewUIElement elemtSlot = child.GetComponent<NewUIElement>();

            //if (child!=null)
            if (elemtSlot != null)
            {
                atomItemsAux.Add(elemtSlot.inventorySlot.item);
            }
            else
                Debug.Log("countt null :" + i);
        }


        craftResult.doTheThing();
        atomItems = atomItemsAux;
        return atomItems;
    }
    void OnTransformChildrenChanged()
    {
        getItems();

    }

    void checkForChange()
    {
        if (oldCount != transform.childCount)
        {
            oldCount = transform.childCount;
            Debug.Log("count changed" + oldCount);
            getItems();
        }
    }


    // Update is called once per frame
    void Update()
    {
        //checkForChange();
    }
}
