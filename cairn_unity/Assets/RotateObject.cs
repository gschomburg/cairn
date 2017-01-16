using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour {
    public float speed = .1f;
    //public Vector3 rotation = new Vector3(0,0,0);
    Rigidbody rb;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        // transform.localEulerAngles = transform.localEulerAngles + new Vector3(0, speed, 0);
        //Quaternion deltaRotation = Quaternion.Euler(eulerAngleVelocity * Time.deltaTime);
        //rb.MoveRotation(rb.rotation * deltaRotation);
        rb.AddTorque(0, speed, 0);
    }
}
