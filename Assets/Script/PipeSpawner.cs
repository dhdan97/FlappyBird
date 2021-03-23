using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    public GameObject pipePrefab; // Our pipe prefab from the prefab folder
    public float moveSpeed; // speed of our pipe
    public float spawnDelay; // The delay between each pipe spawn

    public float minOffset; // The lowest the pipes can go
    public float maxOffset; // The highest the pipes can go

    private void Awake()
    {
        Kill.onKill += OnFlappyBirdDeath; // Subscribe to onDeath event
    }
    // Start is called before the first frame update
    void Start()
    {
        StartSpawn(); // We will start the spawn when the game starts
    }

    public void StartSpawn()
    {
        StartCoroutine("StartSpawnCo"); // Starts the spawn coroutine
    }

    public void StopSpawn()
    {
        StopCoroutine("StartSpawnCo"); // Stops the spawn coroutine
    }

    IEnumerator StartSpawnCo() //Spawn pipe and set it's RigidBody2D's velocity
    {
        while(true)
        {
            float randOffset = Random.Range(minOffset, maxOffset); // Randomize a value between the min and max offset, then we will add it to the Y axis of the pipe

            Vector3 pipeSpawnPosition = new Vector3(transform.position.x, transform.position.y + randOffset, transform.position.z); // Creating a new vector with the offset

            // TODO: move this outside of the IEnumerator scope to change it's velocity after flappyBird death
            GameObject pipe = Instantiate(pipePrefab, // The gameobject you want to spawn
                pipeSpawnPosition, // The Vector3 position you spawning it at, the gameobject's positions that this script is attached to
                Quaternion.identity); // The rotation you want, in this case it means "no rotation"

            pipe.GetComponent<Rigidbody2D>().velocity = Vector2.left * moveSpeed; // First you want to grab reference to the pipe's RigidBody2D, then you set it's velocity in the direction of left and moveSpeed

            yield return new WaitForSeconds(spawnDelay); // All coroutines must yield return at the end, especially in a while loop or else game will crash
                                                         // In this case we will yield return a wait for our spawn delay
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnFlappyBirdDeath()
    {
        StopSpawn();
        GameObject[] Pipes = GameObject.FindGameObjectsWithTag("pipe");
        foreach(GameObject pipe in Pipes)
        {
            pipe.GetComponent<Rigidbody2D>().velocity = Vector2.left * 0;
        }
    }

    private void OnDestroy()
    {
        Kill.onKill -= OnFlappyBirdDeath; // Unsubscribe when gameobject is destroyed
    }
}
