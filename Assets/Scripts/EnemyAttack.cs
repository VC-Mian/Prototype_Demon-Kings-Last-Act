using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float damage = 5;
    public PlayerHealth playerHealth; // reference to PlayerHealth script

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // Apply damage to the player
            playerHealth.TakeDamage(damage);
        }
    }
}

