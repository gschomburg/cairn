using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl2 : MonoBehaviour {
	public float forward_speed = 1.0F;
	public float strafe_speed = 1.0F;
	public float rotation_speed = 100.0F;
	public float anim_scale = 100.0f;

	Animator animator;
	// Use this for initialization
	void Start () {
		animator = GetComponentInChildren<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		float forward = Input.GetAxis("Vertical") * forward_speed;
        float left = Input.GetAxis("Horizontal") * strafe_speed;
        forward *= Time.deltaTime;
        left *= Time.deltaTime;
        transform.Translate(left, 0, forward);


		float rotation = Input.GetAxis("RHorizontal") * rotation_speed;
		rotation *= Time.deltaTime;
		transform.Rotate(0, rotation, 0);


		animator.SetFloat("Speed", forward * anim_scale, 0.04f, Time.deltaTime);
		animator.SetFloat("Strafe", left * anim_scale, 0.04f, Time.deltaTime);
		

	}
}
