using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformParenter : MonoBehaviour {

	// Use this for initialization
	public bool resetParentOnExit;
	public int parentLayerID = 19;
	Transform originalParent;

	void Start () {
		originalParent = transform.parent;
	}

	void OnCollisionEnter(Collision other)
	{
		//check layer id
		if(other.gameObject.layer == parentLayerID){
			transform.parent = findSpinnerParent(other.gameObject); //other.gameObject.transform;
		}
	}
	Transform findSpinnerParent(GameObject obj){
		SpinnerContainer spinner;
		spinner = obj.GetComponentInParent<SpinnerContainer>();
		if(spinner != null){
			// Debug.Log("spinner found!");
			return spinner.gameObject.transform;
		}
		// Debug.Log("spinner not found");
		return obj.transform;
	}

	void OnCollisionExit(Collision other)
	{
		if(other.gameObject.transform == transform.parent && other.gameObject.layer == parentLayerID && resetParentOnExit ){
			transform.parent = originalParent;
		}
	}
}
