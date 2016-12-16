using UnityEngine;
using System.Collections;

//[ExecuteInEditMode]

public class ForceField : MonoBehaviour {
	public Material material;

	float scroll = 0.0f;
	public float scroll_speed = .1f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		scroll += scroll_speed;
		if (material) {
			material.SetFloat ("_Scroll", scroll);
		}
	}
}


