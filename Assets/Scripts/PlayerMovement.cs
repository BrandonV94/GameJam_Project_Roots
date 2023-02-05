using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    public KeyCode left;
    public KeyCode right;
    public KeyCode jump;
    public KeyCode crouch;
    private PlayerStats ps;
    private PlayerCombat pc;

    [SerializeField]
    private bool grounded;
    
    [SerializeField]
    private bool jumping;

    [SerializeField]
    public bool crouching;

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
        if (Input.GetKey(right) && !jumping && !pc.attacking)
        {
            ps.rb.velocity =  new Vector3(transform.forward.x * 2, 0, 0);
            ps.pAnimator.SetBool("Walk Forwards", true);
        }
        else
        {
            ps.pAnimator.SetBool("Walk Forwards", false);
        }

        // Move backwards
        if (Input.GetKey(left) && !jumping && !pc.attacking)
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
        if (Input.GetKey(crouch) && grounded && !jumping & !pc.attacking)
        {
            crouching = true;
            ps.pAnimator.SetBool("Crouching", true);
        }
        else
        {
            ps.pAnimator.SetBool("Crouching", false);
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
