using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinnerController : MonoBehaviour {
	public Rigidbody rb;

	public GameObject container;

	private FixedJoint connectorJoint;
	// Use this for initialization
	void Start () {
		//init the rigid body (maybe unnescesary)
		rb = GetComponent<Rigidbody>();
        rb.centerOfMass = new Vector3(0,0,0);
		rb.inertiaTensorRotation = Quaternion.identity;

		//create a joint to the container
		// connectorJoint = gameObject.AddComponent<FixedJoint> ();
		// connectorJoint.connectedBody = container.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
