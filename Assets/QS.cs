using UnityEngine;

public class QS : MonoBehaviour
{
    private void Start()
    {
        // Load the selected quality level from PlayerPrefs
        int selectedQualityLevel = PlayerPrefs.GetInt("SelectedQualityLevel", 1);

        // Apply the quality level to your desired settings or components
        ApplyQualityLevel(selectedQualityLevel);
    }

    private void ApplyQualityLevel(int qualityLevel)
    {
        // Apply the quality level to your desired settings or components
        switch (qualityLevel)
        {
            case 0:
                // Apply low quality settings
                break;
            case 1:
                // Apply medium quality settings
                break;
            case 2:
                // Apply high quality settings
                break;
        }
    }
}