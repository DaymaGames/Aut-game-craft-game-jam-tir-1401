using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class HealthBar : MonoBehaviour
{
    [Header("Health Bar Image below")]
    [SerializeField] Image healthbarImage;
    [SerializeField] Health health;
    void Update()
    {
        SetHealthBarValue();
    }
    void SetHealthBarValue()
    {
        if (health == null || healthbarImage == null)
            return;

        int currentHealth = health.currentHealth;
        int maxHealth = health.maxHealth;

        healthbarImage.fillAmount = (float) currentHealth / maxHealth;
    }
}
