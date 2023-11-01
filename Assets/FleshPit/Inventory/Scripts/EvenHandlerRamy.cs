using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvenHandlerRamy : MonoBehaviour
{

    public delegate void MyEventHandler();
    public static event MyEventHandler refreshInventoryUI;


    void Start()
    {
        if (refreshInventoryUI != null)
        {
            refreshInventoryUI();
        }
    }

    public static void trig()
    {
        if (refreshInventoryUI != null)
        {
            refreshInventoryUI();
        }
    }
}
