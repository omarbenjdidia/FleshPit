using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item/simple")]
public class ScriptableItem : ScriptableObject
{

    public int id;
    
    public int price;
    public int quantity;

    public new string name;
    public string description;

    public Sprite sprite;

    ScriptableItem(int id, int price, int quantity, string name, string description, Sprite sprite)
    {
        this.id = id;
        this.price = price;
        this.quantity = quantity;
        this.name = name;
        this.description = description;
        this.sprite = sprite;
    }
    ScriptableItem (int id, int price, int quantity, string name, string description)
    {
        this.id = id;
        this.price = price;
        this.quantity = quantity;
        this.name = name;
        this.description = description;
    }
}
