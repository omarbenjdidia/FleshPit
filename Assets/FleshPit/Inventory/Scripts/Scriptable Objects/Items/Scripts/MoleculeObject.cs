using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//public class AtomPairValue
//{
//public string keyName;
//public int valAmount;
//}



[CreateAssetMenu(fileName = "New Molecule Object", menuName = "Inventory System/Items/Molecule")]
public class MoleculeObject : ItemObject
{

    public int composition;

    public List<AtomPairValue> atoms;

    private void Awake()
    {
        type = ItemType.Molecule;
    }

}
