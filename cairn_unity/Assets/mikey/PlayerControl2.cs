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
	private Animator animator;
	private Rigidbody rb;



	void Start () {
		rb = GetComponent<Rigidbody>();
		animator = GetComponentInChildren<Animator>();
	}

	void FixedUpdate () {
		
		// handle walk/strafe	
		float forward = InputManager.ActiveDevice.LeftStickY.Value * forward_speed;
		float left = InputManager.ActiveDevice.LeftStickX.Value * strafe_speed;
		Vector3 deltaPosition =  transform.rotation * new Vector3(left, 0, forward) * Time.deltaTime;
		rb.MovePosition(rb.position + deltaPosition);

		
		// handle turn
		float r = InputManager.ActiveDevice.RightStickX.Value * rotation_speed * Time.deltaTime;
        Quaternion deltaRotation = Quaternion.Euler(0f, r, 0f);
		rb.MoveRotation(rb.rotation * deltaRotation);
	
		// handle head up/down
		float lookUpDown = InputManager.ActiveDevice.RightStickY.Value * look_speed;
		lookUpDown *= Time.deltaTime;
		headRotationX += lookUpDown;
		headRotationX = Mathf.Clamp(headRotationX, -50f, 60f);

		// move camera
		if (headCamera) {
			headCamera.localEulerAngles = new Vector3(headRotationX, 0, 0);
		}
        
		
		// handle jumping
		if (InputManager.ActiveDevice.Action1.WasPressed && IsGrounded(.2f)) {
			Debug.Log("jump");
			rb.AddForce (new Vector3 (0, jump_strength, 0), ForceMode.Impulse);
        }
		

		// update animation states
		animator.SetFloat("Speed", forward * anim_scale * Time.deltaTime, 0.04f, Time.deltaTime);
		animator.SetFloat("Strafe", left * anim_scale * Time.deltaTime, 0.04f, Time.deltaTime);
		animator.SetBool("Grounded",  IsGrounded(.2f));

        // Debug.DrawLine(transform.position + new Vector3(0,.1f,0),
		// 			   transform.position + new Vector3(0,.1f,0) - Vector3.up * .2f
		// 			   , Color.red);
	}


	void LateUpdate() {
		// bone manipulation should be in LateUpdate() so that it happens after the Animator moves the armature
		neckBone.localEulerAngles = new Vector3(-headRotationX * .5f, 180, 0);
		torsoBone.localEulerAngles = new Vector3(90 + headRotationX * .5f, 0, 0);
	}

	 bool IsGrounded(float checkDistance) {
		return Physics.Raycast(transform.position + new Vector3(0,.1f,0), -Vector3.up, checkDistance);
	}
}
