using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    
    public Animator pAnimator;
    public Rigidbody rb;
    public BoxCollider collider;
    
    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<BoxCollider>();
        pAnimator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }
}
