using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class UIAtomSlot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler , IPointerExitHandler
{

    //public AtomObject atom;
    public ItemObject atom;

    public TextMeshProUGUI symbole;
    public TextMeshProUGUI amount;

    public Camera cam;

    [ContextMenu("Color")]
    void changeColor()
    {
        if (atom != null)
        {
            Image image = GetComponentInChildren<Image>();
            if (image != null)
            {
                image.color = atom.color;
            }

            symbole.text = atom.name.Substring(0,1);
            amount.text = (420).ToString();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Image clicked!");
    }

    bool onHover = false;
    public void OnPointerEnter(PointerEventData eventData)
    {
        onHover= true;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        onHover= false;
 
    }
    public GameObject prefab;
    void throwObj()
    {
        
            Ray r = cam.ScreenPointToRay(Input.mousePosition);

            Vector3 dir = r.GetPoint(1) - r.GetPoint(0);

            // position of spanwed object could be 'GetPoint(0).. 1.. 2' half random choice ;)
            GameObject bullet = Instantiate(prefab, r.GetPoint(2), Quaternion.LookRotation(dir));

            bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 20;
            Destroy(bullet, 3);
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (onHover&&Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log("throw "+ atom.name);
            throwObj();
        }
    }
}
