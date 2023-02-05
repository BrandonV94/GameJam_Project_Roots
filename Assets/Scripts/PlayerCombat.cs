using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    private float hitstun;
    public float hurtStun;
    
    private string[] bodyTags = new[]
    {
        "leftHand",
        "rightHand",
        "leftLeg",
        "rightLeg"
    };

    public enum AttackType
    {
        standingPunch,
        standingKick,
        lowKick,
        upperCut,
        none
    }

    public AttackType currentAttack;
    
    public KeyCode punch;
    public KeyCode kick;

    public bool leftLegActive;
    public bool rightLegActive;
    public bool leftArmActive;
    public bool rightArmActive;
    
    private PlayerStats ps;
    private PlayerMovement pm;
    public bool attacking;
    void Start()
    {
        ps = GetComponent<PlayerStats>();
        pm = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        // Low punch and low kick
        if (pm.crouching == true && Input.GetKey(punch) && !attacking)
        {
            attacking = true;
            leftArmActive = true;
            currentAttack = AttackType.upperCut;
            ps.pAnimator.SetTrigger("Low_Punch");
        }
        else if (pm.crouching == true && Input.GetKey(kick) && !attacking)
        {
            attacking = true;
            rightLegActive = true;
            currentAttack = AttackType.lowKick;
            ps.pAnimator.SetTrigger("Low_Kick");
        }
        else if (Input.GetKey(punch) && !attacking)
        {
            attacking = true;
            leftArmActive = true;
            currentAttack = AttackType.standingPunch;
            ps.pAnimator.SetTrigger("Attack_1");
        } 
        else if (Input.GetKey(kick) && !attacking)
        {
            attacking = true;
            rightLegActive = true;
            currentAttack = AttackType.standingKick;
            ps.pAnimator.SetTrigger("Attack_2");
        }
        else
        {
            // DO nothing
        }
    }

    void ReleaseActionLock()
    {
        currentAttack = AttackType.none;
        attacking = false;
        leftArmActive = false;
        rightArmActive = false;
        leftLegActive = false;
        rightLegActive = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<BodyPartColliderScript>()){
            BodyPartColliderScript bps = other.gameObject.GetComponent<BodyPartColliderScript>();
            bool activeHit = false;
            switch(bps.BodyPart){
                case BodyPartColliderScript.bodyPart.leftArm:
                    activeHit = bps.playerReference.leftArmActive;
                    break;
                
                case BodyPartColliderScript.bodyPart.rightArm:
                    activeHit = bps.playerReference.rightArmActive;
                    break;
                
                case BodyPartColliderScript.bodyPart.leftLeg:
                    activeHit = bps.playerReference.leftLegActive;
                    break;
                
                case BodyPartColliderScript.bodyPart.rightLeg:
                    activeHit = bps.playerReference.rightLegActive;
                    break;
            }

            if (activeHit)
            {
                if (hitstun < Time.timeSinceLevelLoad)
                {
                    ReleaseActionLock();
                    switch (bps.playerReference.currentAttack)
                    {
                        case AttackType.lowKick:
                            ps.health -= 30;
                            ps.rb.velocity = bps.playerReference.gameObject.transform.forward * 8;
                            hurtStun = Time.timeSinceLevelLoad + 1;
                            break;
                        
                        case AttackType.standingKick:
                            ps.health -= 20;
                            ps.rb.velocity = bps.playerReference.gameObject.transform.forward * 5;
                            hurtStun = Time.timeSinceLevelLoad + 1;
                            break;
                        
                        case AttackType.upperCut:
                            ps.rb.velocity = bps.playerReference.gameObject.transform.forward * 4;
                            ps.health -= 25;
                            hurtStun = Time.timeSinceLevelLoad + 2;
                            break;
                        
                        case AttackType.standingPunch:
                            ps.rb.velocity = bps.playerReference.gameObject.transform.forward * 3;
                            ps.health -= 10;
                            hurtStun = Time.timeSinceLevelLoad + 1;
                            break;
                    }
                }
            }
        }
    }
}
