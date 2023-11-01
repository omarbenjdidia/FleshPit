using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RamyMain : MonoBehaviour
{
    public Transform root;
    public Transform UI;
    public Transform D3Inventory;
    bool is3DInv = false;
    // Start is called before the first frame update
    void Start()
    {
        //LoadInventory();
    }

    [ContextMenu("ToggelInv")]
    void Toggle3DInventory()
    {
        updateInventory();
        UI.gameObject.SetActive(!is3DInv);
        D3Inventory.gameObject.SetActive(is3DInv);

        is3DInv= !is3DInv;
    }

    void updateInventory()
    {
        D3Inventory.gameObject.GetComponentInChildren<threeDGridPanel>().updateForInventory();
    }

    public void LoadInventory()
    {
        SceneManager.LoadScene("3D_Inventory", LoadSceneMode.Additive);

        // Find the root object of the added scene
        //GameObject addedSceneRoot = SceneManager.GetSceneByName("3D_Inventory").GetRootGameObjects()[0];
        Scene loadedScene = SceneManager.GetSceneByName("3D_Inventory");
        if (loadedScene.isLoaded)
        {
            GameObject[] rootObjects = loadedScene.GetRootGameObjects();
            foreach (GameObject rootObject in rootObjects)
            {
                SceneManager.MoveGameObjectToScene(rootObject, SceneManager.GetActiveScene());
                rootObject.transform.position = root.position;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            updateInventory();
        if (Input.GetKeyDown(KeyCode.E))
        {
            Toggle3DInventory();
        }
    }
}
