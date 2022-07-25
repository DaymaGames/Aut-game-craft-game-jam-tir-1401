using UnityEngine;
using UnityEngine.UI;


public class HealthBar : MonoBehaviour
{
    
    [Header("Health Bar Image below")]
    [SerializeField]Image healthbarImage;
    [SerializeField]Health playerHealth;  
    void Update()
    {
        SetHealthBarValue();
    }
    void SetHealthBarValue()
    {
        healthbarImage.fillAmount = (float)playerHealth.currentHealth/100;
    }
}
