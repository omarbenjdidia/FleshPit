using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QualitySelectorScript : MonoBehaviour
{
    public Button lowButton;
    public Button mediumButton;
    public Button highButton;

    private int selectedQualityLevel = 1; // default to medium quality

    void Start()
    {
        selectedQualityLevel = PlayerPrefs.GetInt("SelectedQualityLevel", 1); // default to medium quality
        ApplyQualityLevel();
        ColorButtons();
    }

    public void SetQualityLow()
    {
        selectedQualityLevel = 0;
        SaveQualityLevel();
        ApplyQualityLevel();
        ColorButtons();
    }

    public void SetQualityMedium()
    {
        selectedQualityLevel = 1;
        SaveQualityLevel();
        ApplyQualityLevel();
        ColorButtons();
    }

    public void SetQualityHigh()
    {
        selectedQualityLevel = 2;
        SaveQualityLevel();
        ApplyQualityLevel();
        ColorButtons();
    }

    private void ApplyQualityLevel()
    {
        QualitySettings.SetQualityLevel(selectedQualityLevel);
    }

    private void SaveQualityLevel()
    {
        PlayerPrefs.SetInt("SelectedQualityLevel", selectedQualityLevel);
    }

    private void ColorButtons()
    {
        ColorBlock lowColors = lowButton.colors;
        ColorBlock mediumColors = mediumButton.colors;
        ColorBlock highColors = highButton.colors;

        switch (selectedQualityLevel)
        {
            case 0:
                lowColors.normalColor = Color.green;
                mediumColors.normalColor = Color.white;
                highColors.normalColor = Color.white;
                break;
            case 1:
                lowColors.normalColor = Color.white;
                mediumColors.normalColor = Color.green;
                highColors.normalColor = Color.white;
                break;
            case 2:
                lowColors.normalColor = Color.white;
                mediumColors.normalColor = Color.white;
                highColors.normalColor = Color.green;
                break;
        }

        lowButton.colors = lowColors;
        mediumButton.colors = mediumColors;
        highButton.colors = highColors;
    }
}