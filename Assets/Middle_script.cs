using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Middle_script : MonoBehaviour
{
    private Logic_Script logic;

    void Start()
    {
        logic = GameObject.FindWithTag("Logic").GetComponent<Logic_Script>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            logic.addScore();
        }
    }
}
