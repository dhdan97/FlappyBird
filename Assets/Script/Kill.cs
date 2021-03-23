using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kill : MonoBehaviour
{
    public delegate void KillHandler();
    public static event KillHandler onKill;

    private void OnTriggerEnter2D(Collider2D col) // A Unity event. Called when two colliders with isTrigger is touched
    {
        if (col.tag == "Player") // Use tags to check for player in case there are other collisions in the game like pick ups
        {
            onKill();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
