using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class MonsterHealth : MonoBehaviour
{
    [Header("Monster Health")]
    public float maxHealth = 400f;
    public float currentHealth;
    public bool isDead = false;

    [Header("Health Bar")]
    public Image healthBarUI;
    public TextMeshProUGUI healthText;
    private Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        currentHealth = maxHealth;
        if (healthBarUI != null)
        {
            UpdateHealthBar();
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            TakeDamage(10f); // For testing, reduce health by 10 when H is pressed
        }
        if (cam == null)
        {
            cam = Camera.main;
        }
        //transform.forward = cam.transform.forward;
        transform.LookAt(cam.transform);
        transform.Rotate(0, 180, 0);
    }
    public void TakeDamage(float damage)
    {
        if (isDead) return;

        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
        UpdateHealthBar();
    }
void Die()
{
    isDead = true;

    GameManager gm = FindObjectOfType<GameManager>();
    if (gm != null)
    {
        gm.RemoveMonster(transform.parent.gameObject); // Notify GameManager before destroying
    }

    Destroy(transform.parent.gameObject);
}

    void UpdateHealthBar()
    {
        // Debug.Log("Updating health bar: " + currentHealth + "/" + maxHealth);
        if (currentHealth < 0) currentHealth = 0; // Ensure health doesn't go below 0
        if (healthBarUI == null) return;

        Image healthBarImage = healthBarUI.GetComponent<Image>();
        if (healthBarImage != null)
        {
            float healthPercentage = currentHealth / maxHealth;
            healthBarImage.fillAmount = healthPercentage;
            healthText.text = $"{currentHealth}/{maxHealth}";
        }
    }

}
