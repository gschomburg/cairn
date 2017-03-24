using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class ParentBlock : MonoBehaviour {

	// Use this for initialization
	Transform originalParent;
	Rigidbody rb;
	int rotationLayerID = 19;
	// ThrowableScaleableVelocity throwable;

	bool touchingRotationBlock;
	bool isLocked = false;
	Transform touchedPlatform;
	bool attached; //attached to hand

	void Start(){
		originalParent = transform.parent;
		rb = GetComponent<Rigidbody>();
		// throwable = GetComponent<ThrowableScaleableVelocity>();
	}
	//when collided with parent blocks
		//set parent to block
		//set this block as kinematic when at rest if touching a rotation block

	/// <summary>
	/// OnCollisionEnter is called when this collider/rigidbody has begun
	/// touching another rigidbody/collider.
	/// </summary>
	/// <param name="other">The Collision data associated with this collision.</param>
	void OnCollisionEnter(Collision other)
	{
		Debug.Log("OnCollisionEnter");
		//layer 19 RotationPlatforms
		if(other.gameObject.layer == rotationLayerID){
			touchingRotationBlock = true;
			touchedPlatform = other.gameObject.transform;
			if(!attached){
				transform.parent = other.gameObject.transform;
			}
		}
	}
	/// <summary>
	/// OnCollisionStay is called once per frame for every collider/rigidbody
	/// that is touching rigidbody/collider.
	/// </summary>
	/// <param name="other">The Collision data associated with this collision.</param>
	void OnCollisionStay(Collision other)
	{
		// Debug.Log("OnCollisionStay");
		if(!isLocked && touchingRotationBlock && !attached){
			if(rb.velocity.magnitude<.2f){
				// Debug.Log("resting: " + rb.velocity.magnitude);
				//set it kinematic
				LockToPlatform();
			}
		}
	}

	void LockToPlatform(){
		rb.interpolation = RigidbodyInterpolation.None;
		// rb.isKinematic = true;
		isLocked =true;
	}
	/// <summary>
	/// OnCollisionExit is called when this collider/rigidbody has
	/// stopped touching another rigidbody/collider.
	/// </summary>
	/// <param name="other">The Collision data associated with this collision.</param>
	void OnCollisionExit(Collision other)
	{
		Debug.Log("OnCollisionExit");
		touchedPlatform = null;
		touchingRotationBlock = false;
		isLocked = false;
		if(!attached){
			transform.parent = originalParent;
		}
	}

	void OnAttachedToHand( Hand hand )
	{
		attached = true;
		Debug.Log("OnAttachedToHand");
		isLocked = false;
	}

	void OnDetachedFromHand( Hand hand ){
		attached = false;
		rb.interpolation = RigidbodyInterpolation.None;
		//reset parent
		if(touchingRotationBlock && touchedPlatform!=null){
			transform.parent = touchedPlatform;
		}else{
			transform.parent = originalParent;
		}
		Debug.Log("OnDetachedFromHand");
	}
}
