using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
public class SimpleGrabbable : MonoBehaviour {

	[EnumFlags]
	public Hand.AttachmentFlags attachmentFlags = Hand.AttachmentFlags.ParentToHand | Hand.AttachmentFlags.DetachFromOtherHand;
    public string attachmentPoint;
    public bool restoreOriginalParent = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	private void HandHoverUpdate(Hand hand)
    {
        //Trigger got pressed
        if (hand.GetStandardInteractionButtonDown())
        {
            Debug.Log("grabbed");
            hand.AttachObject(gameObject, attachmentFlags, attachmentPoint);
            //ControllerButtonHints.HideButtonHint(hand, Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger);
        }
    }
    // private void OnAttachedToHand(Hand hand)
    // {
    //     attachPosition = transform.position;
    //     attachRotation = transform.rotation;
    // }
    private void HandAttachedUpdate(Hand hand)
    {
        //Trigger got released
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

        hand.DetachObject(gameObject, restoreOriginalParent);
    }
}
