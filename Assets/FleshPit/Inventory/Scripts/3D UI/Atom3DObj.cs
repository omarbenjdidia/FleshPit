
using UnityEngine;

public class Atom3DObj : MonoBehaviour
{
    public ItemObject atom;
    public Material material;
    private Material materialAux;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Collider>().isTrigger = true;
        //changeMaterielColor();
    }

    [ContextMenu("Change")]
    public void changeMaterielColor()
    {
        Debug.Log("d5alt ll change");
        materialAux = new Material(material);

        GameObject child = transform.GetChild(0).gameObject;

        var renderer = child.GetComponent<MeshRenderer>();
        materialAux.color = atom.color;

        renderer.material = materialAux;

        //materials[0] = materialAux;
        //renderer.sharedMaterials = materials; // assign the array back to the property to actually apply the changes
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggring UI3d atom");
        UI3DelementSlot x = other.gameObject.GetComponent<UI3DelementSlot>();
        UI3DelementSlot parent = other.gameObject.GetComponentInParent<UI3DelementSlot>();
        if (x)
        {
            if (x.itemObject = parent.itemObject)
            {
                Debug.Log("Added");

                parent.inventorySlot.amount += x.inventorySlot.amount;
                parent.updateAmount();  
            }
        }
    }
    void OnCollisionEnter(Collision other)
    {
        Debug.Log("coll");
        if (other.gameObject.tag == "test")
        {
            Debug.Log("Triggring UI3d atom");


            UI3DelementSlot x = other.gameObject.GetComponent<UI3DelementSlot>();
            UI3DelementSlot parent = other.gameObject.GetComponentInParent<UI3DelementSlot>();
            if (x)
            {
                if (x.itemObject = parent.itemObject)
                {
                    Debug.Log("Added");

                    parent.inventorySlot.amount += x.inventorySlot.amount;
                    parent.updateAmount();
                }
            }
        }
    }

}
