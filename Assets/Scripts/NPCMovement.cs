using UnityEngine;

public class NPCMovement : MonoBehaviour
{
    public float speed = 2f;         // Speed at which the NPC will move
    public Transform[] patrolPoints; // Array of patrol points

    private Rigidbody2D rb;          // Rigidbody2D component of the NPC
    private int patrolIndex = 0;     // Index of the current patrol point

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Patrol();
    }

    void Patrol()
    {
        Vector2 direction = (patrolPoints[patrolIndex].position - transform.position).normalized;
        rb.velocity = new Vector2(direction.x * speed, rb.velocity.y);

        if (Vector2.Distance(transform.position, patrolPoints[patrolIndex].position) < 0.1f)
        {
            patrolIndex = (patrolIndex + 1) % patrolPoints.Length;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) // Change this to the tag you want to check for
        {
            Destroy(gameObject); // Destroy the NPC when it collides with the specified object
        }
    }
}







