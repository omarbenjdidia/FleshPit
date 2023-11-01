using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OverlayOtherScene : MonoBehaviour
{
    // Start is called before the first frame update
 public string overlaySceneName;

    private bool isOverlayActive = false;
    public Scene overlayScene;


    public void ToggleOverlay()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)

            SceneManager.LoadScene(1, LoadSceneMode.Additive);
        else
        {
            SceneManager.LoadScene(0);
            SceneManager.UnloadSceneAsync(1);

        }

            isOverlayActive = true;
        
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleOverlay();
        }
    }


}
