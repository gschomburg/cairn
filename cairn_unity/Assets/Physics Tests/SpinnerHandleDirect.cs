using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class SpinnerHandleDirect : MonoBehaviour {

	public Transform targetSpinner;
	public float n = 0.0f;
    private bool dragging;
    private Hand attachedHand;

    private float startAngle;
    private float oldAngle, deltaAngle = 0;
    void Start () {		
	}

	private void HandHoverUpdate(Hand hand)
    {
        if (hand.GetStandardInteractionButtonDown())
        {
            Grab(hand);		
        }
    }
    
    private void Grab(Hand hand){
        attachedHand = hand;
        dragging = true;
       // startAngle = Mathf.Rad2Deg * Mathf.Atan2(transform.localPosition.x, transform.localPosition.z) ;

       Vector3 handPosition = new Vector3(attachedHand.transform.position.x, 0.0f, attachedHand.transform.position.z);
       handPosition = targetSpinner.InverseTransformPoint(handPosition);
       startAngle = Mathf.Rad2Deg * Mathf.Atan2(handPosition.x, handPosition.z);
    }
    
	
	private void Drag()
    {
        Vector3 handPosition = new Vector3(attachedHand.transform.position.x, 0.0f, attachedHand.transform.position.z);
        float targetAngle = Mathf.Rad2Deg * Mathf.Atan2(handPosition.x, handPosition.z);
        targetSpinner.rotation = Quaternion.AngleAxis(targetAngle - startAngle, Vector3.up);
        if (!attachedHand.GetStandardInteractionButton())
        {
            Drop();
        }
    }

	private void Drop(){
        Debug.Log("Drop");
        dragging = false;   
        attachedHand = null;
     }

    private void Update()
    {
        if(dragging)
        {
            Drag();
            float newAngle = transform.rotation.eulerAngles.y;
            if (Mathf.Abs(newAngle - oldAngle) < 180) {
                // if delta angle is > 180 it a mistake as the angle loops from 0 to 360, just don't count it.
                deltaAngle =  newAngle - oldAngle;
            }
            oldAngle = transform.rotation.eulerAngles.y;
        } else {
             targetSpinner.Rotate(new Vector3(0,deltaAngle,0));
             deltaAngle *= .97f;
        }
    }
}
