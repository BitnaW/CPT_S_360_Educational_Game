using UnityEngine;
using UnityEngine.UI;


public class HealthBar : MonoBehaviour
{

    // you set the root image and the other frames in the inspector window
    [SerializeField] private Sprite[] barFrames; 
    [SerializeField] private Image healthBarImages; 

    public void UpdateHealthBar(int currentHealth , int maxHealth)
    {   
        float healthPercentage = (float)currentHealth / maxHealth;
        int index = Mathf.Clamp(Mathf.FloorToInt((1f - healthPercentage) * barFrames.Length), 0, barFrames.Length - 1);
        Debug.Log("index: " + index);
        healthBarImages.sprite = barFrames[index];
    }
}
