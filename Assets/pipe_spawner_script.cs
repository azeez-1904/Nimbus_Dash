using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSpawnScript : MonoBehaviour
{
    public GameObject pipe;
    public float spawnRate = 2f;
    private float timer = 0f;
    public float heightOffset = 7;
    private bool is_spawning = false;
    private bool stopped = false;

    [Header("Difficulty Settings")]
    public float difficultyIncreaseRate = 0.02f;
    public float minSpawnRate = 0.8f;

    [Header("Speed Settings")]
    public float initialPipeSpeed = 5f;
    public float maxPipeSpeed = 12f;
    public float speedIncreaseRate = 0.05f;
    private float currentPipeSpeed;

    void Start()
    {
        currentPipeSpeed = initialPipeSpeed;
    }

    void Update()
    {
        if (!is_spawning || stopped) return;

        if (timer < spawnRate)
        {
            timer += Time.deltaTime;
        }
        else
        {
            spawnPipe();
            timer = 0f;
        }

        // Gradual difficulty increase over time
        if (spawnRate > minSpawnRate)
        {
            spawnRate -= difficultyIncreaseRate * Time.deltaTime;
        }

        if (currentPipeSpeed < maxPipeSpeed)
        {
            currentPipeSpeed += speedIncreaseRate * Time.deltaTime;
        }
    }

    // Called by Logic_Script at score milestones for instant difficulty jumps
    public void SetDifficulty(float pipeSpeed, float spawnRate)
    {
        currentPipeSpeed = pipeSpeed;
        this.spawnRate = spawnRate;
    }

    public void BeginSpawning()
    {
        is_spawning = true;
        spawnPipe();
    }

    public void StopSpawning()
    {
        stopped = true;
    }

    void spawnPipe()
    {
        float lowestPoint = transform.position.y - heightOffset;
        float highestPoint = transform.position.y + heightOffset;

        Vector3 spawnPos = new Vector3(
            transform.position.x,
            Random.Range(lowestPoint, highestPoint),
            0
        );

        GameObject newPipe = Instantiate(pipe, spawnPos, transform.rotation);

        Pipe_Script pipeScript = newPipe.GetComponent<Pipe_Script>();
        if (pipeScript != null)
        {
            pipeScript.pipe_speed = currentPipeSpeed;
        }
    }
}
