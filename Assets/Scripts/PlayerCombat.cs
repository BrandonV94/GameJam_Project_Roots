using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    private float hitstun;
    
    private string[] bodyTags = new[]
    {
        "leftHand",
        "rightHand",
        "leftLeg",
        "rightLeg"
    };
    
    public KeyCode punch;
    public KeyCode kick;

    public bool leftLegActive;
    public bool rightLegActive;
    public bool leftArmActive;
    public bool rightArmActive;
    
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
            leftArmActive = true;
            ps.pAnimator.SetTrigger("Attack_1");
        } 
        
        if (Input.GetKey(kick) && !attacking)
        {
            attacking = true;
            rightLegActive = true;
            ps.pAnimator.SetTrigger("Attack_2");
        } 
    }

    void ReleaseActionLock()
    {
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
                    Debug.Log(gameObject.name + " hit by " + other.gameObject.tag);
                    hitstun = Time.timeSinceLevelLoad + 0.5f;
                }
            }
        }
    }
}