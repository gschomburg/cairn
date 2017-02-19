using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tumbler : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void FixedUpdate () {
		float a = Mathf.Sin(Time.time);
		// transform.Rotate(Vector3.forward * Time.deltaTime * a * 50);
		// transform.Rotate(Vector3.left * Time.deltaTime * a * 50);

		Vector3 p = new Vector3((Input.mousePosition.x-400)*.005f , (Input.mousePosition.y-400) *.005f, 0.0f);
		transform.position = p;
	}
}
