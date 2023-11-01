using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICrafting : MonoBehaviour
{
    List<UICraftingSlot> uICraftingSlots;

    UICraftingSlot slot;

    // Start is called before the first frame update
    void Start()
    {
        //slot = gameObject.transform.GetChild(1).gameObject.GetComponent<UICraftingSlot>;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
      
    }
}
