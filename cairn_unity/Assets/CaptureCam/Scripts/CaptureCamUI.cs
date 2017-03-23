using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using Valve.VR.InteractionSystem;
// using Valve.VR;

public class CaptureCamUI : MonoBehaviour {

    public CaptureCam captureCam;
    public GameObject screen;
    public bool isActive;
    //
   
    // private Vector3 attachPosition;
    // private Quaternion attachRotation;
    // Use this for initialization
    void Start () {    
        //wait for cam init 
	}
    public void init(CaptureCam _captureCam){
        captureCam = _captureCam;
        screen.GetComponent<Renderer>().material.mainTexture = captureCam.ScreenTexture;
        if(!isActive){
            hide();
        }
    }
	
	// Update is called once per frame
	//void Update () {
	
    void Update () {

        // if(targetHand.controller.GetPressDown(EVRButtonId.k_EButton_SteamVR_Touchpad)){
        //     toggleActive();
        // }
        for ( int i = 0; i < Player.instance.handCount; i++ )
        {
            Hand hand = Player.instance.GetHand( i );

            if ( hand.controller != null )
            {
                if ( hand.controller.GetPressDown( Valve.VR.EVRButtonId.k_EButton_ApplicationMenu ) )
                {
                    toggleActive();
                }
            }
        }
    
    }

    void toggleActive(){
        isActive = !isActive;
        if(isActive){
            show();
        }else{
            hide();
        }
        // Debug.Log("toggleActive");
    }
    void show(){
        GetComponent<BoxCollider>().enabled=true;
        GetComponent<SimpleGrabbable>().enabled=true;
        GetComponent<Interactable>().enabled=true;
        for(int i =0; i<transform.childCount; i++)
        {
             transform.GetChild(i).gameObject.SetActive(true);
        }
        captureCam.gameObject.SetActive(true);
    }
    void hide(){
        GetComponent<BoxCollider>().enabled=false;
        GetComponent<SimpleGrabbable>().enabled=false;
        GetComponent<Interactable>().enabled=false;
        for(int i =0; i<transform.childCount; i++)
        {
             transform.GetChild(i).gameObject.SetActive(false);
        }
        captureCam.gameObject.SetActive(false);
    }

	//}
    public void Capture(){
        captureCam.Capture();
    }

    // private void OnHandHoverBegin(Hand hand)
    // {
    //     Debug.Log("OnHandHoverBegin");
    // }
    // private void OnHandHoverEnd(Hand hand)
    // {
    //     Debug.Log("OnHandHoverEnd");
    // }
    // private void HandHoverUpdate(Hand hand)
    // {
    //     //Trigger got pressed
    //     if (hand.GetStandardInteractionButtonDown())
    //     {
    //         Debug.Log("grabbed");
    //         hand.AttachObject(gameObject, attachmentFlags, attachmentPoint);
    //         //ControllerButtonHints.HideButtonHint(hand, Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger);
    //     }
    // }
    // private void OnAttachedToHand(Hand hand)
    // {
    //     attachPosition = transform.position;
    //     attachRotation = transform.rotation;
    // }
    // private void HandAttachedUpdate(Hand hand)
    // {
    //     //Trigger got released
    //     if (!hand.GetStandardInteractionButton())
    //     {
    //         // Detach ourselves late in the frame.
    //         // This is so that any vehicles the player is attached to
    //         // have a chance to finish updating themselves.
    //         // If we detach now, our position could be behind what it
    //         // will be at the end of the frame, and the object may appear
    //         // to teleport behind the hand when the player releases it.
    //         StartCoroutine(LateDetach(hand));
    //     }
    // }

    // //-------------------------------------------------
    // private IEnumerator LateDetach(Hand hand)
    // {
    //     yield return new WaitForEndOfFrame();

    //     hand.DetachObject(gameObject, restoreOriginalParent);
    // }
}
