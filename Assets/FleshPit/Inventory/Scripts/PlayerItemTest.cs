using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemTest : MonoBehaviour
{
    public InventoryObject inventory;

    [SerializeField]
    public GameObject inventoryPanel;
    public bool inventoryPanelActive = false;
    // Start is called before the first frame update

    private void OnTriggerEnter(Collider other)
    {
        var item = other.GetComponent<WorldItemObj>();
        if (item != null)
        {
            inventory.AddItem(item.itemObject, 1);
            EvenHandlerRamy.trig();

            Destroy(other.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            EvenHandlerRamy.trig();
        if(Input.GetKeyDown(KeyCode.E))
        {
            inventoryPanel.SetActive(!inventoryPanelActive);
            inventoryPanelActive=!inventoryPanelActive;
        }
    }
}
