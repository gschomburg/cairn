using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamScreen : MonoBehaviour {

	public List<Collider> dropTargets;
	public Collider currentDropTarget;
	// Use this for initialization
	void Start () {
		
	}
	void OnDetachedFromHand(){
		//check if it's close to the drop targets
		if(currentDropTarget!=null){
			//snap to target
			transform.parent =currentDropTarget.gameObject.transform;
		}else{
			//it's free to move around
			transform.parent = null;
		}
	}
	void OnTriggerEnter (Collider other){
		Debug.Log("OnTriggerEnter");
		if(dropTargets.Contains(other)){
			currentDropTarget = other;
		}
	}
	void OnTriggerExit (Collider other){
		Debug.Log("OnTriggerExit");
		if(other == currentDropTarget){
			currentDropTarget=null;
		}
	}
}
