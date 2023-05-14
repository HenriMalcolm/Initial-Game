using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private float horizontalInput;
    private float verticalInput;
    public float speed = 15.0f;
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

    public void TakeDamage()
    {
        health--; // Decrease health
        if (health <= 0)
        {
            Die(); // Call Die() method if health is zero or less
        }
    }

    private void Die()
    {
        Debug.Log("Player died!"); // Replace with your own death code
        Destroy(gameObject);
    }
}
