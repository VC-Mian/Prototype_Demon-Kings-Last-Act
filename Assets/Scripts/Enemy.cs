using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Animator animator;
    public int maxHealth = 100;
    private int currentHealth;
    [SerializeField] private string deathAnimationName = "Death"; // Default value

    void Start()
    {
        currentHealth = maxHealth;
        Debug.Log("Enemy health set to " + currentHealth);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        animator.SetTrigger("Hurt");
        Debug.Log("Hurt trigger set.");

        if (currentHealth <= 0)
        {
            StartCoroutine(Die());
        }
    }

    IEnumerator Die()
    {
        Debug.Log("Enemy Died!");

        animator.SetBool("IsDead", true);

        // Wait for the length of the death animation before disabling the collider
        yield return new WaitForSeconds(GetAnimationClipLength(deathAnimationName));

        // Disable collider and optionally set Rigidbody2D to kinematic
        GetComponent<Collider2D>().enabled = false;
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.isKinematic = true;
        }

        // Destroy the game object after the death animation has finished
        Destroy(gameObject);
    }

    private float GetAnimationClipLength(string clipName)
    {
        foreach (var clip in animator.runtimeAnimatorController.animationClips)
        {
            if (clip.name == clipName)
            {
                return clip.length;
            }
        }
        Debug.LogWarning("Animation clip not found: " + clipName);
        return 0f;
    }
}




