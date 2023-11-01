using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMoleculePanel : MonoBehaviour
{
    // Start is called before the first frame update

    public InventoryObject inventory;

    [ContextMenu("update amounts")]
    public void updateAmouts()
    {
        int count = transform.childCount;

        for (int i = 0; i < count; i++)
        {
            Transform transform1 = transform.GetChild(i);
            GameObject obj = transform1.gameObject;
         
            UIMoleculeSlot uIMol= obj.GetComponent<UIMoleculeSlot>();

            if (uIMol != null)
            {
                uIMol.amount.text = inventory.amoutOf(uIMol.molecule).ToString();
            }
            
        }
    }

    void Start()
    {
        updateAmouts();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
