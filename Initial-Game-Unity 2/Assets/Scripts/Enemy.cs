using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
<<<<<<< HEAD
    public Transform target;
    public float speed = 3f;
    public float rotateSpeed = 0.0025f;
    private Rigidbody2D rb;
=======
    // Public variables that can be set in the Inspector
    public Transform target; // The target for the enemy to chase (usually the player)
    public float speed = 3f; // The speed at which the enemy moves
    public float rotateSpeed = 0.0025f; // The speed at which the enemy rotates to face its target
    public float bullet;
>>>>>>> 479037160724293fc7d4122677c155acfa075fcc

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (!target)
        {
            GetTarget();
        }
        else
        {
            RotateTowardsTarget();
        }
    }

    private void FixedUpdate()
    {
        if (!Physics2D.OverlapCircle(transform.position, 0.5f, LayerMask.GetMask("Player")))
        {
            rb.velocity = transform.up * speed;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    private void RotateTowardsTarget()
    {
        Vector2 targetDirection = target.position - transform.position;
        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg - 90f;
        Quaternion q = Quaternion.Euler(new Vector3(0, 0, angle));
        transform.localRotation = Quaternion.Slerp(transform.localRotation, q, rotateSpeed);
    }

    private void GetTarget()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject && playerObject.activeSelf)
        {
            target = playerObject.transform;
        }
    }

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
<<<<<<< HEAD
        } else if (other.gameObject.CompareTag("Bullet")) {
=======
        }
        else if (other.gameObject.CompareTag("Bullet"))
        {
            LevelManager.manager.IncreaseScore(1);
>>>>>>> 479037160724293fc7d4122677c155acfa075fcc
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }

}
