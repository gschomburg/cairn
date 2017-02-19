using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class PlayerControl2 : MonoBehaviour {
	public float forward_speed = 1.0F;
	public float strafe_speed = 1.0F;
	public float rotation_speed = 100.0F;
	public float anim_scale = 100.0f;
	public float jump_strength = 5.0f;
	public float look_speed = 100.0f;
	public Transform neckBone;
	public Transform torsoBone;
    public Transform headCamera;


	private float headRotationX = 0;
	Rigidbody rigid_body;
	Animator animator;




	void Start () {
		animator = GetComponentInChildren<Animator>();
		rigid_body = GetComponent<Rigidbody>();
	}

	void Update () {
		// handle walk/strafe
		//float forward = Input.GetAxis("Vertical") * forward_speed;
		float forward = InputManager.ActiveDevice.LeftStickY.Value * forward_speed;

		// Debug.Log(InputManager.ActiveDevice.Action1.IsPressed);

	//float left = Input.GetAxis("Horizontal") * strafe_speed;
		float left = InputManager.ActiveDevice.LeftStickX.Value * strafe_speed;

		forward *= Time.deltaTime;
        left *= Time.deltaTime;
        transform.Translate(left, 0, forward);


		// handle turn
		//float rotation = Input.GetAxis("RHorizontal") * rotation_speed;
		float rotation = InputManager.ActiveDevice.RightStickX.Value * rotation_speed;
		rotation *= Time.deltaTime;
		transform.Rotate(0, rotation, 0);

		// handle head up/down
		//float lookUpDown = Input.GetAxis("RVertical") * look_speed;
		float lookUpDown = InputManager.ActiveDevice.RightStickY.Value * look_speed;
		lookUpDown *= Time.deltaTime;
		headRotationX += lookUpDown;
		headRotationX = Mathf.Clamp(headRotationX, -50f, 60f);

		if (headCamera) {
		headCamera.localEulerAngles = new Vector3(headRotationX, 0, 0);
		}
        // handle jumping
        Debug.DrawLine(transform.position + new Vector3(0,.1f,0),
					   transform.position + new Vector3(0,.1f,0) - Vector3.up * .2f
					   , Color.red);
		//if (Input.GetButtonDown ("Jump") && IsGrounded(.2f)) {
		if (InputManager.ActiveDevice.Action1.WasPressed && IsGrounded(.2f)) {
			rigid_body.AddForce (new Vector3 (0, jump_strength, 0), ForceMode.Impulse);
        }



		// update animation states
		animator.SetFloat("Speed", forward * anim_scale, 0.04f, Time.deltaTime);
		animator.SetFloat("Strafe", left * anim_scale, 0.04f, Time.deltaTime);
		animator.SetBool("Grounded",  IsGrounded(.2f));

	}
	void LateUpdate(){
		neckBone.localEulerAngles = new Vector3(-headRotationX * .5f, 180, 0);
		torsoBone.localEulerAngles = new Vector3(90 + headRotationX * .5f, 0, 0);
	}

	 bool IsGrounded(float checkDistance) {
		if (Physics.Raycast(transform.position + new Vector3(0,.1f,0), -Vector3.up, checkDistance))
        {
            return true;
        }

        return false;
	}
}
