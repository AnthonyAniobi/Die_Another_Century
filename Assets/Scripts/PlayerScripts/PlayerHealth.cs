using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private Slider healthBarSlider;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private Image healthBarFillImage;
    //
    private int maxHealth;
    private int currentHealth;
    public void Initialize(int maxHealth)
    {
        this.maxHealth = maxHealth;
        this.currentHealth = maxHealth;
        UpdateHealthUI();
    }


    public void TakeDamage(int damage, Vector3 hitPoint)
    {
        currentHealth -= damage;
        if (currentHealth < 0) currentHealth = 0;
        UpdateHealthUI();
    }

    // Update is called once per frame
    void UpdateHealthUI()
    {
        healthBarSlider.value = (float)currentHealth / maxHealth;
        healthText.text = $"{currentHealth}/{maxHealth}";
        healthBarFillImage.color = Color.Lerp(Color.red, Color.green, (float)currentHealth / maxHealth);
    }
}
