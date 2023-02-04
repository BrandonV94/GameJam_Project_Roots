using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Animator pAnimator;

    void Start()
    {
        pAnimator = GetComponent<Animator>();
    }


    void Update()
    {
        Move();
    }

    void Move()
    {
        // Move forward
        if (Input.GetKey(KeyCode.D))
        {
            Debug.Log("Moving forward");
            pAnimator.SetBool("Walk", true);
        }
        else 
            pAnimator.SetBool("Walk", false);

        // Move backwards
        if (Input.GetKey(KeyCode.A))
        {
            Debug.Log("Walk back.");
            pAnimator.SetBool("Walk Backwards", true);
        }
            
        else
            pAnimator.SetBool("Walk Backwards", false);
    }
}
