using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UICraftingSlot : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public ItemObject item;

    public ItemDatabaseObject databaseObject;


    [ContextMenu("do the thingy")]
    void doTheThing()
    {
        checkType();
    }

    void checkType()
    {
        if (item.type == ItemType.Atom)
        {
            switch (item.name)
            {
                case "Carbon":
                    duplicate(Vector3.right, 150);
                    duplicate(Vector3.left, 150);
                    duplicate(Vector3.down, 150);
                    duplicate(Vector3.up, 150);
                    Debug.Log("===");

                    break;
                default:
                    break;
            }
        }
        else if (item.type == ItemType.Molecule)
        {
            MoleculeObject molecule = (MoleculeObject)item;

            molecule.atoms.ForEach(atom =>
            {
                //Debug.Log("atom" + atom.keyName.ToString());
                //Debug.Log(atom.valAmount);
            });
            Debug.Log(molecule.ToString());
        }
        else
            Debug.Log("not molecule");
    }

    public void duplicate(Vector3 direction, int gap)
    {
        //if (direction == null)
        //    direction = Vector3.right;
        //int spaceGap = 200;
        GameObject newObject = Instantiate(gameObject, transform.position + direction * gap, Quaternion.identity);
        newObject.transform.SetParent(gameObject.transform.parent);

        //drawLineBetween(transform, newObject.transform);

        drawLine2(newObject.transform);

        //Strech(lineSprite,transform.position,newObject.transform.position,true);

        UICraftingSlot slot = newObject.GetComponent<UICraftingSlot>();
        //slot.item 
        listDatabase();

    }

    void listDatabase()
    {
        Debug.Log(
        databaseObject.Items.Count
            );
    }

    public LineRenderer lr;
    List<Transform> points= new();
    public void drawLineBetween(Transform objectA, Transform objectB)
    {

        // Get the LineRenderer component

        // Set the LineRenderer's position count to 2
        lr.positionCount = 2;

        // Set the first position to objectA's position
        lr.SetPosition(0, objectA.position);

        // Set the second position to objectB's position
        lr.SetPosition(1, objectB.position);

        // Set the line's width and color
        lr.startWidth = 0.5f;
        lr.endWidth = 0.9f;
        lr.startColor = Color.red;
        lr.endColor = Color.blue;

    }

    public void drawLine2(Transform t)
    {

        LineRenderer lr2 = new GameObject().AddComponent<LineRenderer>();
        lr2.transform.SetParent(transform, false);
        // just to be sure reset position and rotation as well
        lr2.gameObject.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);

    }
    public void drawLine(Transform t)
    {
        points.Add(t);
        lr.SetPosition(lr.positionCount, new Vector3(t.position.x, t.position.y, 0));
        lr.positionCount++;

    }

    public GameObject lineSprite;
    public void Strech(GameObject _sprite, Vector3 _initialPosition, Vector3 _finalPosition, bool _mirrorZ)
    {
        Vector3 centerPos = (_initialPosition + _finalPosition) / 2f;
        _sprite.transform.position = centerPos;
        Vector3 direction = _finalPosition - _initialPosition;
        direction = Vector3.Normalize(direction);
        _sprite.transform.right = direction;
        if (_mirrorZ) _sprite.transform.right *= -1f;
        Vector3 scale = new Vector3(5, 0.3f, 0.5f);
        //scale.x = Vector3.Distance(_initialPosition, _finalPosition) ;// Screen.height;
        _sprite.transform.localScale = scale;


        Instantiate(_sprite, _sprite.transform.position, Quaternion.identity);
    }


    [HideInInspector]
    public Transform parent;
    private Image image;

    public void OnBeginDrag(PointerEventData eventData)
    {
        parent = transform.parent;
        //rectTransform.SetSiblingIndex(3);
        transform.SetParent(parent.parent);
        transform.SetAsLastSibling();
        image.raycastTarget = false;
    }


    public void OnDrag(PointerEventData eventData)
    {
        Vector2 position = Input.mousePosition;
        transform.position = position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(parent);
        image.raycastTarget = true;


    }
    // Start is called before the first frame update
    void Start()
    {
        image = gameObject.GetComponent<Image>();
        lr = GetComponent<LineRenderer>();

        parent = transform.parent;
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < points.Count; i++)
        {
            lr.SetPosition(i, points.ElementAt(i).position);
        }

    }
}
