using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goku_phys : MonoBehaviour
{
    public Rigidbody2D MyRigidbody;
    public float fly_strength = 5f;
    private Logic_Script logic;
    private bool goku_is_alive = true;
    private bool game_started = false;

    [Header("Rotation")]
    public float rotateUpSpeed = 8f;
    public float rotateDownSpeed = 4f;
    public float maxUpAngle = 30f;
    public float maxDownAngle = -70f;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        logic = GameObject.FindWithTag("Logic").GetComponent<Logic_Script>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Freeze bird until game starts
        MyRigidbody.simulated = false;
    }

    void Update()
    {
        if (!goku_is_alive) return;

        // Space, left click, or touch to flap
        bool flapInput = Input.GetKeyDown(KeyCode.Space)
            || Input.GetMouseButtonDown(0)
            || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began);

        if (flapInput)
        {
            if (!game_started)
            {
                game_started = true;
                MyRigidbody.simulated = true;
                logic.StartGame();
            }

            MyRigidbody.velocity = Vector2.up * fly_strength;
        }

        // Rotate bird based on velocity
        UpdateRotation();
    }

    private void UpdateRotation()
    {
        float targetAngle;
        if (MyRigidbody.velocity.y > 0)
        {
            targetAngle = maxUpAngle;
        }
        else
        {
            targetAngle = Mathf.Lerp(0f, maxDownAngle, Mathf.Abs(MyRigidbody.velocity.y) / 10f);
        }

        float currentAngle = transform.eulerAngles.z;
        if (currentAngle > 180f) currentAngle -= 360f;

        float speed = MyRigidbody.velocity.y > 0 ? rotateUpSpeed : rotateDownSpeed;
        float newAngle = Mathf.Lerp(currentAngle, targetAngle, Time.deltaTime * speed);
        transform.rotation = Quaternion.Euler(0, 0, newAngle);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!goku_is_alive) return;
        goku_is_alive = false;
        MyRigidbody.velocity = Vector2.zero;
        logic.Game_Over();
    }

    private void OnBecameInvisible()
    {
        if (!goku_is_alive) return;
        goku_is_alive = false;
        logic.Game_Over();
    }
}
