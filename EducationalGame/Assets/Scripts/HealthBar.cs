using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class HealthBar : MonoBehaviour
{

    // you set the root image and the other frames in the inspector window
    private Sprite[] barFrames; 
    private Image healthBarImages; 

    public void UpdateHealthBar(int currentHealth , int maxHealth)
    {   
        float healthPercentage = (float)currentHealth / maxHealth;
        int index = Mathf.Clamp(Mathf.FloorToInt((1f - healthPercentage) * barFrames.Length), 0, barFrames.Length - 1);
        healthBarImages.sprite = barFrames[index];
    }
}
