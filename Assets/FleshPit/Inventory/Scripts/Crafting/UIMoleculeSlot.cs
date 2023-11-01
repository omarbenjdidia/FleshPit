using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIMoleculeSlot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    //public ItemObject molecule;
    public MoleculeObject molecule;

    public TextMeshProUGUI nameTxt;
    public TextMeshProUGUI amount;

    public CraftingSystem craftingSystem;

    public UIAtomPanel atomPanel;
    public void OnPointerClick(PointerEventData eventData)
    {
        craftingSystem.CraftMolecule(molecule);
        int k = Convert.ToInt32(amount.text);
        k++;
        amount.text = k.ToString();
        atomPanel.updateAmouts();
    }

    bool onHover = false;
    public void OnPointerEnter(PointerEventData eventData)
    {
        onHover = true;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        onHover = false;

    }
    [ContextMenu("Update Slot")]
    void setUI()
    {
        if (molecule != null)
        {
            Image image = GetComponentInChildren<Image>();
            if (image != null)
            {
                image.sprite = molecule.uiDisplay;
            }
        }
        nameTxt.text = molecule.name;
        amount.text = (420).ToString();
    }
    // Start is called before the first frame update
    void Start()
    {
        setUI();

    }

    // Update is called once per frame
    void Update()
    {

    }
}
