using UnityEngine;
//public class AtomPairValue
//{
//    public string key;
//    public int val;
//}

[CreateAssetMenu(fileName = "New AtomObject", menuName = "Inventory System/Items/Atom")]
public class AtomObject : ItemObject
{
    int electronLinks = 0;

    //Color color = Color.magenta;

    private void Awake()
    {
        type = ItemType.Atom;
    }
}
