using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinnerContainer : MonoBehaviour {
	public Rigidbody rb;


	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
        rb.centerOfMass = new Vector3(0,0,0);
		rb.inertiaTensorRotation = Quaternion.identity;
	}
	
	// Update is called once per frame

}
