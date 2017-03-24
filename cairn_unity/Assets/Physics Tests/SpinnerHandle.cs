using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class SpinnerHandle : MonoBehaviour {

	public Rigidbody targetRB;
    public Rigidbody rb;
	public SpringJoint dragJoint;
    public FixedJoint lockJoint;
	public Transform jointVisualizer;

    public float jointStrength = 10;
    public float jointDamper = 1;

	[EnumFlags]
	public Hand.AttachmentFlags attachmentFlags = Hand.AttachmentFlags.ParentToHand | Hand.AttachmentFlags.DetachFromOtherHand;
    public string attachmentPoint;
    public bool restoreOriginalParent = false;

    public bool grabbed=false;
    public Hand attachedHand;
    private Vector3 rotationOrigin;
    private float distance; 

	// when grabbed create a joint between the handle and the controller
	void Start () {
		rb = GetComponent<Rigidbody>();
        rotationOrigin = targetRB.gameObject.transform.position;
        rotationOrigin.y = transform.position.y;
        distance = Vector3.Distance( transform.position, rotationOrigin);
	}
	
	private void HandHoverUpdate(Hand hand)
    {
        //Trigger got pressed
        if (hand.GetStandardInteractionButtonDown())
        {
            
            Grab(hand);
			
            // hand.AttachObject(gameObject, attachmentFlags, attachmentPoint);
            //ControllerButtonHints.HideButtonHint(hand, Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger);
			
        }
    }
    // private void OnAttachedToHand(Hand hand)
    // {
    //     attachPosition = transform.position;
    //     attachRotation = transform.rotation;
    // }

    private void Grab(Hand hand){
        Debug.Log("grabbed");
        attachedHand = hand;
        jointVisualizer.position = transform.position;
        grabbed = true;
        // rb.isKinematic = true;
        //move it outside the hinge joint?
        transform.parent = transform.parent.parent;
        ConnectJoint();
    }
    private void Drop(){
        Debug.Log("dropped");
        grabbed = false;
        // rb.isKinematic = false;
        transform.position = jointVisualizer.position;
        transform.parent = jointVisualizer.parent;
        attachedHand = null;
        RemoveJoint();
    }

    private void FixedUpdate()
    {
        if(grabbed){
            //lock the y axis
            
           Vector3 handPosition = new Vector3(attachedHand.transform.position.x, jointVisualizer.position.y, attachedHand.transform.position.z);
           float targetAngle = Mathf.Atan2(handPosition.x, handPosition.z);

           transform.position = new Vector3(Mathf.Sin(targetAngle) * distance, jointVisualizer.position.y, Mathf.Cos(targetAngle) * distance);
           if (!attachedHand.GetStandardInteractionButton())
            {
                StartCoroutine(LateDetach(attachedHand));
            }
        }
    }
	private void ConnectJoint(){
        //fixed joint
        // if(lockJoint!=null) return;
        // lockJoint = gameObject.AddComponent<FixedJoint> ();
		// lockJoint.connectedBody = targetRB;
       
         
		if(dragJoint!=null) return;

		dragJoint = gameObject.AddComponent<SpringJoint> ();
		dragJoint.connectedBody = targetRB;
		//configure joint
		dragJoint.damper = jointDamper;
		dragJoint.maxDistance = .2f;
		dragJoint.spring = jointStrength;
	}
	private void RemoveJoint(){
        
        //  Destroy(lockJoint);
		Destroy(dragJoint);
	}
    private void HandAttachedUpdate(Hand hand)
    {
        if (!hand.GetStandardInteractionButton())
        {
            // Detach ourselves late in the frame.
            // This is so that any vehicles the player is attached to
            // have a chance to finish updating themselves.
            // If we detach now, our position could be behind what it
            // will be at the end of the frame, and the object may appear
            // to teleport behind the hand when the player releases it.
            StartCoroutine(LateDetach(hand));
        }
    }

    //-------------------------------------------------
    private IEnumerator LateDetach(Hand hand)
    {
        yield return new WaitForEndOfFrame();
        Drop();
		// RemoveJoint();
        // hand.DetachObject(gameObject, restoreOriginalParent);
		// transform.position = jointVisualizer.position;
    }
}
