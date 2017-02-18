using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class SallyHandController : MonoBehaviour {
    Animator animator;
    public float grabVal;
    public float pinchVal;
    public bool pinching = false;
    public List<GameObject> pinchOnGrabObjects;
    public enum Hands
    {
        Right,
        Left
    }

    public Hands hand = Hands.Right;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        GetComponentInParent<VRTK_ControllerEvents>().AliasGrabOn += new ControllerInteractionEventHandler(DoGrabOn);
        GetComponentInParent<VRTK_ControllerEvents>().AliasGrabOff += new ControllerInteractionEventHandler(DoGrabOff);
        GetComponentInParent<VRTK_ControllerEvents>().AliasUseOn += new ControllerInteractionEventHandler(DoUseOn);
        GetComponentInParent<VRTK_ControllerEvents>().AliasUseOff += new ControllerInteractionEventHandler(DoUseOff);
        GetComponentInParent<VRTK_ControllerEvents>().TriggerAxisChanged += new ControllerInteractionEventHandler(DoTriggerAxisChanged);

        GetComponentInParent<VRTK_InteractTouch>().ControllerTouchInteractableObject += new ObjectInteractEventHandler(OnTouchInteractable);
        GetComponentInParent<VRTK_InteractTouch>().ControllerUntouchInteractableObject += new ObjectInteractEventHandler(OnUnTouchInteractable);
    }

    //private void InversePosition(Transform givenTransform)
    //{
    //    givenTransform.localPosition = new Vector3(givenTransform.localPosition.x * -1, givenTransform.localPosition.y, givenTransform.localPosition.z);
    //    givenTransform.localEulerAngles = new Vector3(givenTransform.localEulerAngles.x, givenTransform.localEulerAngles.y * -1, givenTransform.localEulerAngles.z);
    //}

    private void DoTriggerAxisChanged(object sender, ControllerInteractionEventArgs e)
    {
        //rcCarScript.SetTriggerAxis(e.buttonPressure);
        if (pinching)
        {
            pinchVal = e.buttonPressure;
            grabVal = 0.01f;
        }
        else
        {
            grabVal = e.buttonPressure;
            pinchVal = 0.01f;
        }
        
    }

    private void OnTouchInteractable(object sender, ObjectInteractEventArgs e)
    {
        if (e.target)
        {
            if (pinchOnGrabObjects.Contains(e.target))
            {
                pinching = true;
                Debug.Log("PINCH");
            }
            else
            {
                pinching = false;
            }
        }
        Debug.Log("OnTouchInteractable");
    }
    private void OnUnTouchInteractable(object sender, ObjectInteractEventArgs e)
    {
        //if (e.target)
        //{
        //    if (pinchOnGrabObjects.Contains(e.target))
        //    {
        //        pinching = true;
        //    }
        //}
        pinching = false;
        Debug.Log("OnUnTouchInteractable");
    }

    private void DoGrabOn(object sender, ControllerInteractionEventArgs e)
    {
        //targetGripRotation = maxRotation;
        //grabVal = 1;
    }

    private void DoGrabOff(object sender, ControllerInteractionEventArgs e)
    {
        //targetGripRotation = originalGripRotation;
        //grabVal = 0;
    }

    private void DoUseOn(object sender, ControllerInteractionEventArgs e)
    {
        //targetPointerRotation = maxRotation;
    }

    private void DoUseOff(object sender, ControllerInteractionEventArgs e)
    {
        //targetPointerRotation = originalPointerRotation;
    }

    private void Update()
    {
        //pointerFinger.localEulerAngles = new Vector3(targetPointerRotation, 0f, 0f);
        //gripFingers.localEulerAngles = new Vector3(targetGripRotation, 0f, 0f);
        
        animator.SetFloat("Blend_Grab", grabVal, .05f, Time.deltaTime);
        animator.SetFloat("Blend_Pinch", pinchVal, .05f, Time.deltaTime);
    }
}
