using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform[] patrolPoints;
    public float moveSpeed;
    public int patrolDestination;

    private Vector3 originalScale;

    public Transform playerTransform;
    public bool isChasing;
    public float chaseDistance;
    public float maxChaseDistance;

    private Animator animator; // Reference to Animator component

    private void Start()
    {
        // Store the original scale at the start
        originalScale = transform.localScale;

        // Get the Animator component
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isChasing)
        {
            ChasePlayer();
        }
        else
        {
            Patrol();
        }
    }

    private void Patrol()
    {
        // Check if the player is within chase distance
        if (Vector2.Distance(transform.position, playerTransform.position) < chaseDistance)
        {
            isChasing = true;
            animator.SetBool("IsWalk", true); // Set IsWalk to true when chasing
            return;
        }

        // Ensure there are at least two patrol points
        if (patrolPoints.Length < 2) return;

        // Move to the next patrol point
        Transform targetPatrolPoint = patrolPoints[patrolDestination];
        transform.position = Vector2.MoveTowards(transform.position, targetPatrolPoint.position, moveSpeed * Time.deltaTime);

        // Set IsWalk to true when patrolling
        animator.SetBool("IsWalk", true);

        // Check if the enemy reached the current patrol point
        if (Vector2.Distance(transform.position, targetPatrolPoint.position) < .2f)
        {
            // Toggle between patrol points
            patrolDestination = (patrolDestination == 0) ? 1 : 0;

            // Update facing direction based on the next patrol point
            if (patrolPoints[patrolDestination].position.x > transform.position.x)
            {
                // Face right
                transform.localScale = new Vector3(Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
            }
            else
            {
                // Face left
                transform.localScale = new Vector3(-Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
            }

            // Set IsWalk to false when reaching the patrol point
            animator.SetBool("IsWalk", false);
        }
    }

    private void ChasePlayer()
    {
        // Check if the player is within max chase distance
        if (Vector2.Distance(transform.position, playerTransform.position) > maxChaseDistance)
        {
            isChasing = false;
            animator.SetBool("IsWalk", false); // Set IsWalk to false when stopping chase
            return;
        }

        // Move towards the player
        if (transform.position.x > playerTransform.position.x)
        {
            // Face left
            transform.localScale = new Vector3(-Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
            transform.position += Vector3.left * moveSpeed * Time.deltaTime;
        }
        else if (transform.position.x < playerTransform.position.x)
        {
            // Face right
            transform.localScale = new Vector3(Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
            transform.position += Vector3.right * moveSpeed * Time.deltaTime;
        }

        // Set IsWalk to true when chasing
        animator.SetBool("IsWalk", true);
    }
}
