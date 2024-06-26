using UnityEngine;

public class Coin : MonoBehaviour
{
    private bool isCollected = false; // Add a flag

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isCollected && other.CompareTag("Player"))
        {
            isCollected = true; // Set the flag to true
            CoinManager.instance.AddCoins(1);
            Destroy(gameObject); // Destroy the coin after collecting
        }
    }
}


