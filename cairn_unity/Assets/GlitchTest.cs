using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlitchTest : MonoBehaviour {

	// Use this for initialization
	float deltaS =0;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.localPosition = new Vector3(Mathf.Sin(deltaS+=.01f)*1.5f, 1.5f, .2f);
	}
}
