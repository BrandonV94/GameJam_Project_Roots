using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    public KeyCode forward;
    public KeyCode backward;
    public KeyCode jump;
    public KeyCode crouch;
    public KeyCode block;
    private PlayerStats ps;
    private PlayerCombat pc;

    [SerializeField]
    private bool grounded;
    
    [SerializeField]
    private bool jumping;

    [SerializeField]
    public bool crouching;
    
    [SerializeField]
    public bool blocking;

    void Start()
    {
        ps = GetComponent<PlayerStats>();
        pc = GetComponent<PlayerCombat>();
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
            ps.pAnimator.SetBool("Jumping", false);
            jumping = false;
        }
    }

    void Move()
    {
        // Move forward
        if (Input.GetKey(forward) && !jumping && !pc.attacking)
        {
            ps.rb.velocity =  new Vector3(transform.forward.x * 2, 0, 0);
            ps.pAnimator.SetBool("Walk Forwards", true);
        }
        else
        {
            ps.pAnimator.SetBool("Walk Forwards", false);
        }

        // Move backwards
        if (Input.GetKey(backward) && !jumping && !pc.attacking)
        {
            ps.rb.velocity = new Vector3(transform.forward.x * -2, 0, 0);
            ps.pAnimator.SetBool("Walk Backwards", true);
        }
        else
        {
            ps.pAnimator.SetBool("Walk Backwards", false);
        }

        //Jump
        if (Input.GetKey(jump) && grounded && !jumping & !pc.attacking)
        {
            jumping = true;
            ps.pAnimator.SetBool("Jumping", true);
            ps.rb.velocity = new Vector3(0, 4, 0);
        }

        // Crouch
        if (Input.GetKey(crouch) && grounded && !jumping && !blocking)
        {
            crouching = true;
            ps.pAnimator.SetBool("Crouching", true);
        }
        else
        {
            crouching = false;
            ps.pAnimator.SetBool("Crouching", false);
        }

        if (crouching)
        {
            ps.collider.center = new Vector3(3.63432192e-16f, 0.565289557f, 0.525574148f);
            ps.collider.size = new Vector3(1, 1.02932751f, 2.05114746f);
        }
        else
        {
            ps.collider.center = new Vector3(0, 1.01000631f, 0);
            ps.collider.size = new Vector3(1, 1.91876101f, 1);
        }
        
        // Block
        if (Input.GetKey(block) && grounded && !jumping & !pc.attacking && !crouching)
        {
            blocking = true;
            ps.pAnimator.SetBool("Blocking", true);
        }
        else
        {
            blocking = false;
            ps.pAnimator.SetBool("Blocking", false);
        }
    }

    bool isGrounded()
    { 
        RaycastHit[] hits;
        hits = Physics.RaycastAll(ps.collider.bounds.center, Vector3.down, ps.collider.bounds.extents.y + 0.1f);

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
