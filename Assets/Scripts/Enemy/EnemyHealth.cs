using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private Canvas healthCanvas; // Reference to the Canvas containing the health bar
    [SerializeField] private Slider healthSlider; // Reference to the UI Slider for health
    [SerializeField] private Image fillImage; // Reference to the fill image of the slider
    private int maxHealth;
    private int currentHealth;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Initialize(int maxHealth)
    {
        this.maxHealth = maxHealth;
        currentHealth = maxHealth;
        healthSlider.value = 1; // Initialize the slider value
        healthCanvas.worldCamera = Camera.main; // Set the canvas to face the main camera
    }

    public void TakeDamage(int damage, Vector3 hitPoint)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        System.Collections.IEnumerator DieCoroutine()
        {
            animator.SetTrigger("Die");
            yield return new WaitForSeconds(2f); // Wait for the death animation to finish
            Destroy(transform.parent.gameObject); // Destroy the enemy GameObject
        }
        StartCoroutine(DieCoroutine());
    }

    

    private void Update()
    {
        // Make the health bar face the camera
        healthCanvas.transform.LookAt(Camera.main.transform);
        healthCanvas.transform.Rotate(0, 180, 0); // Rotate to face the camera correctly
        healthSlider.value = (float)currentHealth / maxHealth; // Update the slider value each frame
        fillImage.color = Color.Lerp(Color.red, Color.green, (float)currentHealth / maxHealth); // Update the fill color based on health
    }
}
