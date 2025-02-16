using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Player playerHealth;
     
    public Image totalHealthBar;

    public Image currentHealthBar;

    private void Start()
    {
        totalHealthBar.fillAmount = playerHealth.currentHealth / 10;
    }

    private void Update()
    {
        currentHealthBar.fillAmount = playerHealth.currentHealth / 10;
    }

    public void UpdateHealthBar()
    {
        totalHealthBar.fillAmount = playerHealth.currentHealth / 10;
    }
}

