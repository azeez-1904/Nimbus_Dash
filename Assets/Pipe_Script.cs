using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe_Script : MonoBehaviour
{
    public float pipe_speed = 5;
    public float dead_zone = -45;
    private bool stopped = false;

    void Update()
    {
        if (stopped) return;

        transform.position = transform.position + (Vector3.left * pipe_speed) * Time.deltaTime;

        if (transform.position.x < dead_zone)
        {
            Destroy(gameObject);
        }
    }

    public void StopMoving()
    {
        stopped = true;
    }
}
