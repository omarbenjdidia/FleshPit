
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Runtime.Serialization;
using System.Linq;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
public class InventoryObject : ScriptableObject
{
    public string savePath;
    public ItemDatabaseObject database;
    public Inventory Container;


    [ContextMenu("999 Inventory")]
    public void fill999()
    {
        foreach (InventorySlot slot in Container.Items)
        {
            if (slot.item.type==ItemType.Atom)
            {
                slot.amount = 999;
            }
        }
    }

    public int amoutOf(ItemObject item)
    {
        InventorySlot slot = Container.Items.Find(x => x.item.Equals(item));
        if (slot == null)
            return -1;
        else
            return slot.amount;
    }
    public List<InventorySlot> GetAtomsSlots()
    {
        return Container.Items.Where(x => x.item.type.Equals(ItemType.Atom)).ToList();
    }
    public List<ItemObject> GetItems()
    {
        List<ItemObject> itemsList = new List<ItemObject>();

        for (int i = 0; i < Container.Items.Count; i++)
        {
            itemsList.Add(Container.Items[i].item);

        }
        return itemsList;
    }
    public void AddItem(ItemObject _item, int _amount)
    {

        for (int i = 0; i < Container.Items.Count; i++)
        {
            //if (Container.Items[i].ID == _item.Id)
            if (Container.Items[i].item.name == _item.name)
            {
                Debug.Log("making" + _item.name);

                Container.Items[i].AddAmount(_amount);
                return;
            }
        }
        Container.AddItemSlot(_item.Id, _item, _amount);

        SetEmptySlot(_item, _amount);

    }
    public InventorySlot SetEmptySlot(ItemObject _item, int _amount)
    {
        for (int i = 0; i < Container.Items.Count; i++)
        {
            if (Container.Items[i].ID <= -1)
            {
                Container.Items[i].UpdateSlot(_item.Id, _item, _amount);
                return Container.Items[i];
            }
        }
        //set up functionality for full inventory
        return null;
    }

    public void MoveItem(InventorySlot item1, InventorySlot item2)
    {
        InventorySlot temp = new InventorySlot(item2.ID, item2.item, item2.amount);
        item2.UpdateSlot(item1.ID, item1.item, item1.amount);
        item1.UpdateSlot(temp.ID, temp.item, temp.amount);
    }


    public bool canUse(ItemObject item, int kadech)
    {
        InventorySlot slot = Container.Items.Find(z => z.item.name.Equals(item.name));

        if (slot == null)
            return false;
        else
        {
            if (slot.amount < kadech)
                return false;
            else
                return true;
        }
        return false;

    }

    public bool useItem(ItemObject itemS, int kadech)
    {
        int slotIndx = Container.Items.FindIndex(z => z.item.Equals(itemS));
        if (slotIndx == -1)
            return false;
        else
        {
            Container.Items[slotIndx].amount -= kadech;
            return true;
        }
    }

    public void RemoveItem(ItemObject _item)
    {
        for (int i = 0; i < Container.Items.Count; i++)
        {
            if (Container.Items[i].item == _item)
            {
                Container.Items[i].UpdateSlot(-1, null, 0);
            }
        }
    }

    [ContextMenu("Save")]
    public void Save()
    {
        //string saveData = JsonUtility.ToJson(this, true);
        //BinaryFormatter bf = new BinaryFormatter();
        //FileStream file = File.Create(string.Concat(Application.persistentDataPath, savePath));
        //bf.Serialize(file, saveData);
        //file.Close();

        IFormatter formatter = new BinaryFormatter();
        Stream stream = new FileStream(string.Concat(Application.persistentDataPath, savePath), FileMode.Create, FileAccess.Write);
        formatter.Serialize(stream, Container);
        stream.Close();
    }
    [ContextMenu("Load")]
    public void Load()
    {
        if (File.Exists(string.Concat(Application.persistentDataPath, savePath)))
        {
            //BinaryFormatter bf = new BinaryFormatter();
            //FileStream file = File.Open(string.Concat(Application.persistentDataPath, savePath), FileMode.Open);
            //JsonUtility.FromJsonOverwrite(bf.Deserialize(file).ToString(), this);
            //file.Close();

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(string.Concat(Application.persistentDataPath, savePath), FileMode.Open, FileAccess.Read);
            Inventory newContainer = (Inventory)formatter.Deserialize(stream);
            for (int i = 0; i < Container.Items.Count; i++)
            {
                Container.Items[i].UpdateSlot(newContainer.Items[i].ID, newContainer.Items[i].item, newContainer.Items[i].amount);
            }
            stream.Close();
        }
    }
    [ContextMenu("Clear")]
    public void Clear()
    {
        Container = new Inventory();
    }
}
[System.Serializable]
public class Inventory
{
    [SerializeField]
    public List<InventorySlot> Items = new List<InventorySlot>();

    public void AddItemSlot(int _id, ItemObject _item, int _amount)
    {
        Items.Add(new InventorySlot(_id, _item, _amount));
    }
}
[System.Serializable]
public class InventorySlot
{
    public int ID = -1;
    public ItemObject item;
    public int amount;
    public InventorySlot()
    {
        ID = -1;
        item = null;
        amount = 0;
    }
    public InventorySlot(int _id, ItemObject _item, int _amount)
    {
        ID = _id;
        item = _item;
        amount = _amount;
    }
    public void UpdateSlot(int _id, ItemObject _item, int _amount)
    {
        ID = _id;
        item = _item;
        amount = _amount;
    }
    public void AddAmount(int value)
    {
        amount += value;
    }
}
