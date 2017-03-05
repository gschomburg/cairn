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

    public enum TargetHand
    {
        Right,
        Left
    }

    public Hand attachHand;
    public TargetHand handId = TargetHand.Right;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        if(attachPoint==null){
            attachPoint = transform;
        }
        //attach to correct hand
        attachHand = Player.instance.GetHand((int)handId);
        transform.parent = attachHand.transform;
        //TODO change this into some sort of call back from the controller when it's ready

        //perhaps
        //OnHandInitialized
        // StartCoroutine(LateStart());
    }
    private void OnHandInitialized(){
        Debug.Log("OnHandInitialized");
        Quaternion rotationOffset = Quaternion.Inverse(attachPoint.rotation) * transform.rotation;
        transform.rotation = attachHand.transform.rotation * rotationOffset;

        transform.position = attachHand.transform.position + (transform.position - attachPoint.position);
        // transform.parent = attachHand.transform;
        //TODO also move all the loose pieces
        

        // hide the attach point
        if(attachPoint != transform){
            attachPoint.gameObject.SetActive(false);
        }
    }
    private IEnumerator LateStart()
    {
        yield return new WaitForSeconds(1);
        //move to hand
        Quaternion rotationOffset = Quaternion.Inverse(attachPoint.rotation) * transform.rotation;
        transform.rotation = attachHand.transform.rotation * rotationOffset;

        transform.position = attachHand.transform.position + (transform.position - attachPoint.position);
        transform.parent = attachHand.transform;
        //TODO also move all the loose pieces
        

        // hide the attach point
        if(attachPoint != transform){
            attachPoint.gameObject.SetActive(false);
        }
        //hide the controller
        // attachHand.controller.
    }
    void FixedUpdate()
    {
        pinchVal = grabVal = .01f; //reset grab and pinch

        if(!attachHand || attachHand.controller == null){
            Debug.Log("controller not found");
            return;
        }else{
             // Vector2 trigger = attachHand.controller.GetAxis(EVRButtonId.k_EButton_Axis1);
            if(pinching){
                pinchVal = attachHand.controller.GetAxis(EVRButtonId.k_EButton_Axis1).x;
            }else{
                grabVal = attachHand.controller.GetAxis(EVRButtonId.k_EButton_Axis1).x;
            }
             if(attachHand.controller.GetPressDown(EVRButtonId.k_EButton_SteamVR_Touchpad)){
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
