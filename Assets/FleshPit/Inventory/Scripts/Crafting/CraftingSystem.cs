using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CraftingSystem : MonoBehaviour
{
    public InventoryObject inventory;

    public ItemDatabaseObject atomDatabase;
    public ItemDatabaseObject moleculesDatabase;

    CraftingRecepies craftingRecepies;

    Dictionary<string, int> atoms = new Dictionary<string, int>();



    private void Start()
    {
    }
    [ContextMenu("Craft X")]
    public void craft()
    {
        CraftMolecule((MoleculeObject)x);
    }

    public ItemObject x;
    [ContextMenu("Use X")]
    void use()
    {
        inventory.useItem(x, 3);
    }
    public List<ItemObject> craftableMolecules()
    {
        List<ItemObject> items = new List<ItemObject>();

        foreach (MoleculeObject molecule in moleculesDatabase.Items)
        {
            if (canCraft(molecule))
            {
                items.Add(molecule);
            }
        }

        return items;
    }
    public List<ItemObject> craftableMolecules(List<ItemObject> atoms)
    {
        List<ItemObject> items = new List<ItemObject>();
        List<string> atomNames = atoms.Select(z => z.name).ToList();

        Debug.Log(atomNames     + " : OMAAAR");


        foreach (MoleculeObject molecule in moleculesDatabase.Items)
        {
            bool can = true;
            List<string> names = molecule.atoms.Select(z => z.keyName).ToList();

            foreach (string name in names)
            {
                if (!atomNames.Contains(name))
                {
                    can = false;
                    break;
                }
            }
            if (can)
                items.Add(molecule);
        }
        return items;
    }
    public bool canCraft(MoleculeObject molecule)
    {
        bool canCraft = true;

        Debug.Log("test type: "+ molecule.name+molecule.atoms.Count);

        molecule.atoms.ForEach(atom =>
        {
            ItemObject ItemObjAtom = atomDatabase.Items.Find(x => x.name == atom.keyName);

            if (!inventory.canUse(ItemObjAtom, atom.valAmount))
            {
                canCraft = false;
            }
        });
        return canCraft;
    }
    public void CraftMolecule(MoleculeObject molecule)
    {
        int done = 0;

        if (canCraft(molecule))
        {
            molecule.atoms.ForEach(atom =>
            {
                ItemObject ItemObjAtom = atomDatabase.Items.Find(x => x.name == atom.keyName);

                if (inventory.useItem(ItemObjAtom, atom.valAmount))
                    done++;
                else
                    Debug.Log("cant make");


            });
            inventory.AddItem(molecule, 1);
        }
        else
            Debug.Log("cant use");

    }

}
