using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandPoseManager : MonoBehaviour {
    Animator animator;
    //public bool isGrabbed;
    //public bool isPinched;
    [Range(0.0f,1.0f)]
    public float grabVal;
    public float pinchVal;
    // Use this for initialization
    void Start () {
        animator = GetComponentInChildren<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        animator.SetFloat("Blend_Grab", grabVal, .05f, Time.deltaTime);
        animator.SetFloat("Blend_Pinch", pinchVal, .05f, Time.deltaTime);
    }
}