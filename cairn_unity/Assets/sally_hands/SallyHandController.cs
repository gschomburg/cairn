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
    // public Hand.AttachmentFlags attachmentFlags = Hand.AttachmentFlags.ParentToHand | Hand.AttachmentFlags.DetachFromOtherHand;
    // public string attachmentPoint;

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

        //GetComponentInParent<VRTK_ControllerEvents>().AliasGrabOn += new ControllerInteractionEventHandler(DoGrabOn);
        //GetComponentInParent<VRTK_ControllerEvents>().AliasGrabOff += new ControllerInteractionEventHandler(DoGrabOff);
        //GetComponentInParent<VRTK_ControllerEvents>().AliasUseOn += new ControllerInteractionEventHandler(DoUseOn);
        //GetComponentInParent<VRTK_ControllerEvents>().AliasUseOff += new ControllerInteractionEventHandler(DoUseOff);
        //GetComponentInParent<VRTK_ControllerEvents>().TriggerAxisChanged += new ControllerInteractionEventHandler(DoTriggerAxisChanged);
        //GetComponentInParent<VRTK_ControllerEvents>().AliasUseOff += new ControllerInteractionEventHandler(togglePinch);

        //GetComponentInParent<VRTK_InteractTouch>().ControllerTouchInteractableObject += new ObjectInteractEventHandler(OnTouchInteractable);
        //GetComponentInParent<VRTK_InteractTouch>().ControllerUntouchInteractableObject += new ObjectInteractEventHandler(OnUnTouchInteractable);
        StartCoroutine(LateStart());
    }
    private IEnumerator LateStart()
    {
        yield return new WaitForSeconds(1);
        //move to hand
        Quaternion rotationOffset = Quaternion.Inverse(attachPoint.rotation) * transform.rotation;
        transform.rotation = attachHand.transform.rotation * rotationOffset;

        transform.position = attachHand.transform.position + (transform.position- attachPoint.position);
        transform.parent = attachHand.transform;
        //also move all the loose pieces
        // attachHand.AttachObject(gameObject, attachmentFlags, attachmentPoint);
    }

    //private void InversePosition(Transform givenTransform)
    //{
    //    givenTransform.localPosition = new Vector3(givenTransform.localPosition.x * -1, givenTransform.localPosition.y, givenTransform.localPosition.z);
    //    givenTransform.localEulerAngles = new Vector3(givenTransform.localEulerAngles.x, givenTransform.localEulerAngles.y * -1, givenTransform.localEulerAngles.z);
    //}

    //private void DoTriggerAxisChanged(object sender, ControllerInteractionEventArgs e)
    //{
    //    //rcCarScript.SetTriggerAxis(e.buttonPressure);
    //    if (pinching)
    //    {
    //        pinchVal = e.buttonPressure;
    //        grabVal = 0.01f;
    //    }
    //    else
    //    {
    //        grabVal = e.buttonPressure;
    //        pinchVal = 0.01f;
    //    }

    //}
    void FixedUpdate()
    {
        if(!attachHand || attachHand.controller == null){
            Debug.Log("controller not found");
            return;
        }
        Vector2 trigger = attachHand.controller.GetAxis(EVRButtonId.k_EButton_Axis1);
        Debug.Log(trigger);
    }
    private void togglePinch()
    {
        pinching = !pinching;
    }
    //private void OnTouchInteractable(object sender, ObjectInteractEventArgs e)
    //{
    //    if (e.target)
    //    {
    //        if (pinchOnGrabObjects.Contains(e.target))
    //        {
    //            pinching = true;
    //            Debug.Log("PINCH");
    //        }
    //        else
    //        {
    //            pinching = false;
    //        }
    //    }
    //    Debug.Log("OnTouchInteractable");
    //}
    //private void OnUnTouchInteractable(object sender, ObjectInteractEventArgs e)
    //{
    //    //if (e.target)
    //    //{
    //    //    if (pinchOnGrabObjects.Contains(e.target))
    //    //    {
    //    //        pinching = true;
    //    //    }
    //    //}
    //    pinching = false;
    //    Debug.Log("OnUnTouchInteractable");
    //}

    //private void DoGrabOn(object sender, ControllerInteractionEventArgs e)
    //{
    //    //targetGripRotation = maxRotation;
    //    //grabVal = 1;
    //}

    //private void DoGrabOff(object sender, ControllerInteractionEventArgs e)
    //{
    //    //targetGripRotation = originalGripRotation;
    //    //grabVal = 0;
    //}

    //private void DoUseOn(object sender, ControllerInteractionEventArgs e)
    //{
    //    //targetPointerRotation = maxRotation;
    //}

    //private void DoUseOff(object sender, ControllerInteractionEventArgs e)
    //{
    //    //targetPointerRotation = originalPointerRotation;
    //}

    private void Update()
    {
        //pointerFinger.localEulerAngles = new Vector3(targetPointerRotation, 0f, 0f);
        //gripFingers.localEulerAngles = new Vector3(targetGripRotation, 0f, 0f);
        
        animator.SetFloat("Blend_Grab", grabVal, .05f, Time.deltaTime);
        animator.SetFloat("Blend_Pinch", pinchVal, .05f, Time.deltaTime);
    }
}
