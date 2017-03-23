using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class ShutterButton : MonoBehaviour {
    //public bool clicked;
    public Hand activeHand;
    public float startScale;
    public float targScale;

    public CaptureCam cam;
    // Use this for initialization
    void Start () {
        startScale = transform.localScale.x;

    }
    // private void HandHoverUpdate(Hand hand)
    // {
    //     //Trigger got pressed
    //     if (hand.GetStandardInteractionButtonDown())
    //     {
    //         //Debug.Log("button pressed");
    //         //activeHand = hand;
    //         //clicked = true;
    //         //hand.AttachObject(gameObject, attachmentFlags, attachmentPoint);
    //         //ControllerButtonHints.HideButtonHint(hand, Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger);
    //         cam.Capture();
    //     }
    //     //if (hand.GetStandardInteractionButtonDown())
    //     //{
    //     //    Debug.Log("GetStandardInteractionButtonDown");
    //     //}
    // }
    // Update is called once per frame
    //void Update () {
    //    if (clicked && activeHand && !activeHand.GetStandardInteractionButtonDown()) {
    //        clicked = false;
    //        activeHand = null;
    //        Debug.Log("button released");
    //    }
    //    if (clicked)
    //    {
    //        targScale = startScale * .5f;
    //    }
    //    else
    //    {
    //        targScale = startScale;
    //    }
    //    transform.localScale = new Vector3(targScale, targScale, targScale); // Vector3.Lerp(transform.localScale, new Vector3(targScale, targScale, targScale), Time.deltaTime);
        
    //}
}
