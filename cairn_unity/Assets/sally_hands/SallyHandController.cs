using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using Valve.VR;
public class SallyHandController : MonoBehaviour {
    Animator animator;
    public Transform attachPoint; //attachpoint should be direct child of the transform
    public float grabVal;
    public float pinchVal;
    public bool pinching = false;
    public List<GameObject> pinchOnGrabObjects;

    public enum HandSide
    {
        Right,
        Left
    }

    public Hand targetHand;
    public HandSide handSide = HandSide.Right;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        if(attachPoint==null){
            attachPoint = transform;
        }
        //attach to correct hand
        targetHand = Player.instance.GetHand((int)handSide);
        transform.parent = targetHand.transform;
    }

    private void OnHandInitialized(){
        //rotate first
        Quaternion rotationOffset = Quaternion.Inverse(attachPoint.rotation) * transform.rotation;
        transform.rotation = targetHand.transform.rotation * rotationOffset;
        //then position
        transform.position = targetHand.transform.position + (transform.position - attachPoint.position);

        //TODO also move all the loose pieces
        
        // hide the attach point
        if(attachPoint != transform){
            attachPoint.gameObject.SetActive(false);
        }
    }
    void FixedUpdate()
    {
        pinchVal = grabVal = .01f; //reset grab and pinch

        if(!targetHand || targetHand.controller == null){
            Debug.Log("controller not found");
            return;
        }else{
             // Vector2 trigger = attachHand.controller.GetAxis(EVRButtonId.k_EButton_Axis1);
            if(pinching){
                pinchVal = targetHand.controller.GetAxis(EVRButtonId.k_EButton_Axis1).x;
            }else{
                grabVal = targetHand.controller.GetAxis(EVRButtonId.k_EButton_Axis1).x;
            }
             if(targetHand.controller.GetPressDown(EVRButtonId.k_EButton_SteamVR_Touchpad)){
                togglePinch();
            }
        }

        animator.SetFloat("Blend_Grab", grabVal, .05f, Time.deltaTime);
        animator.SetFloat("Blend_Pinch", pinchVal, .05f, Time.deltaTime);
        // Debug.Log(trigger);
    }
    private void togglePinch()
    {
        pinching = !pinching;
    }

    // private void Update()
    // {   
    //     animator.SetFloat("Blend_Grab", grabVal, .05f, Time.deltaTime);
    //     animator.SetFloat("Blend_Pinch", pinchVal, .05f, Time.deltaTime);
    // }
}
