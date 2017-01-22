using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl2 : MonoBehaviour {
	public float forward_speed = 1.0F;
	public float strafe_speed = 1.0F;
	public float rotation_speed = 100.0F;
	public float anim_scale = 100.0f;
	public float jump_strength = 5.0f;

	Rigidbody rigid_body;
	Animator animator;
	// Use this for initialization
	void Start () {
		animator = GetComponentInChildren<Animator>();
		rigid_body = GetComponent<Rigidbody>();
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

		if (Input.GetButtonDown ("Jump") && IsGrounded(.2f)) {
			rigid_body.AddForce (new Vector3 (0, jump_strength, 0), ForceMode.Impulse);  
        }

		Debug.DrawLine(transform.position + new Vector3(0,.1f,0),
					   transform.position + new Vector3(0,.1f,0) - Vector3.up * .2f
					   , Color.red);
		 
		

		animator.SetFloat("Speed", forward * anim_scale, 0.04f, Time.deltaTime);
		animator.SetFloat("Strafe", left * anim_scale, 0.04f, Time.deltaTime);
		animator.SetBool("Grounded",  IsGrounded(.2f));

	}

	  bool IsGrounded(float checkDistance) {
		
       
	   
		if (Physics.Raycast(transform.position + new Vector3(0,.1f,0), -Vector3.up, checkDistance))
        {
            return true;
        }

        return false;
	}
}
