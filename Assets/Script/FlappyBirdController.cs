using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlappyBirdController : MonoBehaviour
{
    public float flapPower;
    public float flapRotation; // Z Axis rotation of bird when flapping
    public float fallRotation; // Z Axis rotation of bird when falling
    public float rotationSpeed;
    bool movementDisabled;

    public AudioClip deathSoundEffect; // The audio clip from music folder
    public AudioClip flapSoundEffect;

    AudioSource audio1; // The AudioSource component attached to flappybird, will be used to play the audio clip

    Rigidbody2D rb; // Create a Rigidbody object that will represent the RigidBody attached to flappyBird sprite
    Animator anim; // The Animator component attached to our flappyBird

    private void Awake()
    {
        movementDisabled = false;
        Kill.onKill += OnDeath; // Subscribe our Ondeath function to the onKIll event
    }

    // Start is called before the first frame update, use this for initalization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Attached FlappyBird's RigidBody2D object to rb
        anim = GetComponent<Animator>(); // Gets reference to flappybird's Animator once the game starts
        
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
            audio1.clip = flapSoundEffect;
            audio1.Play();
        }

        SetAnim();
        SetTilt();
        
    }

    void OnDeath()
    {
        movementDisabled = true;
        GetComponent<BoxCollider2D>().enabled = false; // we don't want flappybird to touch the AddScore box Collider when it dies
        audio1.clip = deathSoundEffect;
        audio1.Play();
    }

    private void OnDestroy()
    {
        Kill.onKill -= OnDeath;
    }

    void SetAnim()
    {
        anim.SetFloat("YVelocity", rb.velocity.y);
    }

    void SetTilt()
    {
        if (rb.velocity.y <= 0)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, fallRotation), Time.deltaTime * rotationSpeed);
        }
        else
        {
            transform.localEulerAngles = new Vector3(0, 0, flapRotation);
        }
    }
}
