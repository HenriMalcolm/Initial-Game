using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Public variables that can be set in the Inspector
    public Transform target; // The target for the enemy to chase (usually the player)
    public float speed = 3f; // The speed at which the enemy moves
    public float rotateSpeed = 0.0025f; // The speed at which the enemy rotates to face its target
    public float bullet;

    // Private variables that are not visible in the Inspector
    private Rigidbody2D rb; // The Rigidbody2D component attached to this enemy

    // Start is called before the first frame update
    private void Start()
    {
        // Get a reference to the Rigidbody2D component attached to this enemy
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        // If the enemy doesn't have a target, find one
        if (!target)
        {
            GetTarget();
        }
        else
        {
            // Otherwise, rotate towards the target
            RotateTowardsTarget();
        }
    }

    // FixedUpdate is called at a fixed interval (default: 50 times per second)
    private void FixedUpdate()
    {
        // If the enemy is not overlapping with a player on the "Player" layer...
        if (!Physics2D.OverlapCircle(transform.position, 0.5f, LayerMask.GetMask("Player")))
        {
            // ...move the enemy in its forward direction at its set speed
            rb.velocity = transform.up * speed;
        }
        else
        {
            // Otherwise, stop moving
            rb.velocity = Vector2.zero;
        }
    }

    // Rotate the enemy towards its target
    private void RotateTowardsTarget()
    {
        // Calculate the direction from the enemy to its target
        Vector2 targetDirection = target.position - transform.position;

        // Calculate the angle needed to face the target
        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg - 90f;

        // Create a Quaternion that represents the desired rotation
        Quaternion q = Quaternion.Euler(new Vector3(0, 0, angle));

        // Rotate the enemy towards the target using Slerp (spherical interpolation)
        transform.localRotation = Quaternion.Slerp(transform.localRotation, q, rotateSpeed);
    }

    // Find the nearest player and set it as the enemy's target
    private void GetTarget()
    {
        // Find the nearest GameObject with the "Player" tag
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        // If a player was found and is active in the scene...
        if (playerObject && playerObject.activeSelf)
        {
            // ...set it as the enemy's target
            target = playerObject.transform;
        }
    }

    // Handle collisions with other GameObjects
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerControl player = other.gameObject.GetComponent<PlayerControl>();
            if (player)
            {
                player.TakeDamage();
            }
            Destroy(gameObject);
            target = null;
        }
        else if (other.gameObject.CompareTag("Bullet"))
        {
            LevelManager.manager.IncreaseScore(1);
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}