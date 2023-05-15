using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private float horizontalInput; // Useing A and D
    private float verticalInput; // Useing W and S
    public float speed = 15.0f; // Speed of player
    public int health = 3; // Add a health variable

    // Update is called once per frame
    void Update()
    {   
        //Moves player left, right, up and down
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        transform.Translate(Vector3.up * verticalInput * Time.deltaTime * speed);
        transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * speed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(); // Call TakeDamage() method only if colliding with enemy
        }
    }

    public void TakeDamage()
    {
        if (gameObject.CompareTag("Player"))
        {
            health--; // Decrease health
            if (health <= 0)
            {
                Die(); // Call Die() method if health is zero or less
            }
        }
    }

    private void Die()
    {
        if (gameObject.CompareTag("Player"))
        {
            health--; // Decrease health
            if (health <= 0)
            {
                Destroy(gameObject);
                Debug.Log("Player died!"); // Replace with your own death code
                LevelManager.manager.GameOver();
            }
        }
        else if (gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
            Debug.Log("Enemy killed!");
            // Add any other code you want to run when an enemy is killed
        }
        else if (gameObject.CompareTag("Projectile"))
        {
            Destroy(gameObject);
        }
    }

}
