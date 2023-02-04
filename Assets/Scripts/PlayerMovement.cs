using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    Animator pAnimator;
    Rigidbody rb;
    BoxCollider collider;

    [SerializeField]
    private bool grounded;
    
    [SerializeField]
    private bool jumping;
    
    void Start()
    {
        collider = GetComponent<BoxCollider>();
        pAnimator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }


    void Update()
    {
        Move();
    }

    private void FixedUpdate()
    {
        grounded = isGrounded();
        if (grounded && jumping)
        {
            jumping = false;
        }
    }

    void Move()
    {
        // Move forward
        if (Input.GetKey(KeyCode.D) && !jumping)
        {
            rb.velocity = new Vector3(1, 0, 0);
            pAnimator.SetBool("Walk Forwards", true);
        }
        else
        {
            pAnimator.SetBool("Walk Forwards", false);
        }

        // Move backwards
        if (Input.GetKey(KeyCode.A) && !jumping)
        {
            rb.velocity = new Vector3(-1, 0, 0);
            pAnimator.SetBool("Walk Backwards", true);
        }
        else
        {
            pAnimator.SetBool("Walk Backwards", false);
        }

        //Jump
        if (Input.GetKey(KeyCode.Space) && grounded && !jumping)
        {
            jumping = true;
            pAnimator.SetBool("Jumping", true);
            rb.velocity = new Vector3(0, 4, 0);
        }
    }

    bool isGrounded()
    {
        RaycastHit[] hits;
        hits = Physics.RaycastAll(collider.bounds.center, Vector3.down, collider.bounds.extents.y);

        for (int i = 0; i < hits.Length; i++)
        {
            RaycastHit hit = hits[i];
            if (hit.collider.gameObject.name == "Platform")
            {
                return true;
            }
        }

        return false;
    }
}
