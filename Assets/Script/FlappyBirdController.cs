using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlappyBirdController : MonoBehaviour
{
    public float flapPower;
    bool movementDisabled;

    Rigidbody2D rb; // Create a Rigidbody object that will represent the RigidBody attached to flappyBird sprite

    private void Awake()
    {
        movementDisabled = false;
        Kill.onKill += OnDeath; // Subscribe our Ondeath function to the onKIll event
    }

    // Start is called before the first frame update, use this for initalization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Attached FlappyBird's RigidBody2D object to rb
        
    }

    // Update is called once per frame
    void Update()
    {
        if (movementDisabled) // Return from the update function if movement is disabled so we can't take input anymore
        {
            return;
        }

        if(Input.GetKeyDown(KeyCode.Space)) // If spacebar is pressed down, call the following once
        {
            rb.velocity = new Vector2(rb.velocity.x, flapPower); // Adds an upwards velocity to the Rigidbody
        }
        
    }

    void OnDeath()
    {
        movementDisabled = true;
        GetComponent<BoxCollider2D>().enabled = false; // we don't want flappybird to touch the AddScore box Collider when it dies
    }

    private void OnDestroy()
    {
        Kill.onKill -= OnDeath;
    }
}
