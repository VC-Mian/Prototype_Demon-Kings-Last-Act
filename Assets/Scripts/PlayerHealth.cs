using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    public Animator animator;
    public float maxHealth = 100;
    public float health;
    public Image healthBar;
    public TextMeshProUGUI healthText;  // Reference to the Text component

    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        UpdateHealthText();  // Update health text at the start
    }

    void Update()
    {
        // Controls health bar based on current health numerically
        if (healthBar != null)
        {
            healthBar.fillAmount = Mathf.Clamp(health / maxHealth, 0, 1);
        }
        UpdateHealthText();  // Update health text in Update method
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        animator.SetTrigger("Hurt"); // triggers hurt animation

        if (health <= 0)
        {
            health = 0; // Ensure health doesn't go below zero
            audioManager.PlaySFX(audioManager.death); // Play the death sound
            SceneController.instance.RestartCurrentScene(); // Restart the scene
        }

        UpdateHealthText();  // Update health text when taking damage
    }

    private void UpdateHealthText()
    {
        if (healthText != null)
        {
            healthText.text = health.ToString("0");  // Display the health as an integer
        }
    }
}


