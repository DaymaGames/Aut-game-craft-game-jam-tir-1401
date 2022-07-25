using UnityEngine;
using UnityEngine.UI;


public class HealthBar : MonoBehaviour
{
    
    [Header("Health Bar Image below")]
    [SerializeField]Image healthbarImage;
    [SerializeField]Health playerHealth;
    // Start is called before the first frame update
    void Awake()
    {
        if (healthbarImage.type != Image.Type.Filled)
        {
            healthbarImage.type = Image.Type.Filled;
            healthbarImage.fillMethod = Image.FillMethod.Horizontal;
        }


    }

    
    void Update()
    {
        SetHealthBarValue();
    }
    void SetHealthBarValue()
    {
        healthbarImage.fillAmount = (float)playerHealth.currentHealth/100;
    }
}
