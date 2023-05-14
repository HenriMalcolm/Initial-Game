using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    public Transform target;
    public float speed = 3f;
    public float rotateSpeed = 0.0025f;
    public GameObject bulletPrefab;
    public float fireRate = 1f; // In seconds

    private Rigidbody2D rb;
    private float lastFiredTime;

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

        if (Time.time - lastFiredTime >= fireRate)
        {
            lastFiredTime = Time.time;
            Shoot();
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

    private void Shoot()
    {
        Vector3 spawnPosition = transform.position + transform.up * 0.5f;
        GameObject bullet = Instantiate(bulletPrefab, spawnPosition, transform.rotation);

        // Check if the bullet will collide with the enemy itself
        Collider2D enemyCollider = GetComponent<Collider2D>();
        Collider2D bulletCollider = bullet.GetComponent<Collider2D>();
        Physics2D.IgnoreCollision(enemyCollider, bulletCollider);
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
        }
        else if (other.gameObject.CompareTag("Bullet"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
