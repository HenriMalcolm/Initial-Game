using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed = 10f;  // bullet speed
    public float lifetime = 2f;  // bullet lifetime

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // Set the bullet velocity
        rb.velocity = transform.up * speed;

        // Destroy the bullet after its lifetime expires
        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerControl player = collision.gameObject.GetComponent<PlayerControl>();
            if (player)
            {
                player.TakeDamage();
                Destroy(gameObject); // Destroy the bullet on impact
            }
        }
    }
}
