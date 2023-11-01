using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FullscreenToggleScript : MonoBehaviour
{
    private Toggle fullscreenToggle;

    void Start()
    {
        // Get the Toggle component attached to this game object
        fullscreenToggle = GetComponent<Toggle>();

        // Load the toggle state from PlayerPrefs
        bool fullscreenEnabled = PlayerPrefs.GetInt("FullscreenEnabled", 1) == 1;
        fullscreenToggle.isOn = fullscreenEnabled;

        // Add a listener for changes to the toggle state
        fullscreenToggle.onValueChanged.AddListener(OnFullscreenToggleChanged);
    }

    void OnFullscreenToggleChanged(bool value)
    {
        // Update the player preferences with the new toggle state
        int fullscreenEnabled = value ? 1 : 0;
        PlayerPrefs.SetInt("FullscreenEnabled", fullscreenEnabled);

        // Update the screen mode based on the toggle state
        if (value)
        {
            Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
        }
        else
        {
            Screen.fullScreenMode = FullScreenMode.Windowed;
        }
    }
}
