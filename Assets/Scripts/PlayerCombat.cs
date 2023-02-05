using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public KeyCode punch;
    public KeyCode kick;
    private PlayerStats ps;
    private float combatActionTimer; 
    public bool attacking;
    void Start()
    {
        ps = GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(punch) && !attacking)
        {
            attacking = true;
            ps.pAnimator.SetTrigger("Attack_1");
        } 
        
        if (Input.GetKey(kick) && !attacking)
        {
            attacking = true;
            ps.pAnimator.SetTrigger("Attack_2");
        } 
    }

    void ReleaseActionLock()
    {
        attacking = false;
    }
}
