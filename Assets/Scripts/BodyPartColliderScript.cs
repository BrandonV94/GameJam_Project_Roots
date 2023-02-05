using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPartColliderScript : MonoBehaviour
{
    public PlayerCombat playerReference;
    public bodyPart BodyPart;
    
    public enum bodyPart
    {
        leftArm,
        rightArm,
        leftLeg,
        rightLeg
    }
}
