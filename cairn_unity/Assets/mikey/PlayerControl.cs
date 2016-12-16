using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {
    Rigidbody rb;
    Transform originalParent;
    Transform parentPlatform;

    public Animation CharacterAnim;
    // Use this for initialization
    
  
	void Awake () {
        rb = GetComponent<Rigidbody>();
        originalParent = transform.parent;
        CharacterAnim.Play("Armature|Standing");
        CharacterAnim["Armature|Standing"].wrapMode = WrapMode.Loop;
        CharacterAnim.Play("Armature|Walk");
        CharacterAnim["Armature|Walk"].wrapMode = WrapMode.Loop;
        CharacterAnim.Play("Armature|Strafe");
        CharacterAnim["Armature|Strafe"].wrapMode = WrapMode.Loop;
    }

    public Transform headCamera;
    public Transform neckBone;
    public Transform torsoBone;
    public float headRotationOffsetX = 0;
    public float headRotationOffsetY = 0;

    public float forward_speed = 1.0F;
	public float strafe_speed = 1.0F;

	public float rotation_speed = 100.0F;
	public float look_speed = 100.0F;
	public float jump_strength = 3.0f;

    public bool isJumping = false;
    /*
     * Animation a = GetComponent<Animation> ();

			a["Armature|Grip"].speed = -1;

			if (!a.IsPlaying ("Armature|Grip")) {
				a ["Armature|Grip"].time = a ["Armature|Grip"].length;
				a.Play ("Armature|Grip");
			}
            */

	void Update() {

		// rotate body
		float rotation = Input.GetAxis("RHorizontal") * rotation_speed;
		rotation *= Time.deltaTime;
		transform.Rotate(0, rotation, 0);

		// look up/down
		float head_rotation = Input.GetAxis("RVertical") * look_speed;
		head_rotation *= Time.deltaTime;

        headRotationOffsetX += head_rotation;

        headRotationOffsetX = Mathf.Clamp(headRotationOffsetX, -50f, 60f);

        headRotationOffsetY += rotation*2;
        float maxHeadTurnY = 40;
        headRotationOffsetY = Mathf.Clamp(headRotationOffsetY, -maxHeadTurnY, maxHeadTurnY); //20;
        headRotationOffsetY *= .9f;

        //head.Rotate(head_rotation, 0, 0);
        headCamera.localEulerAngles= new Vector3(headRotationOffsetX, 0, 0);
        //neckBone.localEulerAngles = new Vector3(headRotationOffsetX, 0, 0);

        //hold on to platform
        if (Input.GetAxis("ADS") == 1f && parentPlatform !=null && IsGrounded(.1f))
        {
            rb.isKinematic = true;
            transform.parent = parentPlatform;
            return;
        }
        else
        {
            rb.isKinematic = false;
            transform.parent = originalParent;
        }

        // translate body
        float forward = Input.GetAxis("Vertical") * forward_speed;
        float left = Input.GetAxis("Horizontal") * strafe_speed;
        forward *= Time.deltaTime;
        left *= Time.deltaTime;
        transform.Translate(left, 0, forward);

        UpdateAnimations(forward*150, left*150);

        if (isJumping && rb.velocity.y < 1 && IsGrounded(.4f))
        {
            isJumping = false;
        }

        if (Input.GetButtonDown ("Jump") && IsGrounded(.7f)) {
			rb.AddForce (new Vector3 (0, jump_strength, 0), ForceMode.Impulse);
            isJumping = true;
        }
        //Debug.Log("velocity" + rb.velocity);
        
        
    }
    void LateUpdate()
    {
        neckBone.localEulerAngles = new Vector3(-headRotationOffsetX *.6f, 180 + headRotationOffsetY, 0);
        float torsoX = 90;
        torsoBone.localEulerAngles = new Vector3(torsoX + (headRotationOffsetX*.4f), headRotationOffsetY*.5f, 0);
    }

    void UpdateAnimations(float forward, float left)
    {
        //CharacterAnim.Play("Armature|"+ animName);


        if (isJumping)
        {
            CharacterAnim.CrossFade("Armature|Jump", .05f);
        }
        else
        {

            CharacterAnim["Armature|Walk"].speed = forward;
            CharacterAnim["Armature|Strafe"].speed = -left;
            if (Mathf.Abs(forward) > .1 || Mathf.Abs(left) > .1)
            {
                
                CharacterAnim.CrossFade("Armature|Walk", .2f);
                //CharacterAnim.CrossFade("Armature|Strafe", .2f);
                //blend in strafe
                //float blend = Mathf.Abs(left*.2f);
                //Debug.Log("bland amount" + blend);


                Vector2 walkSpeed = new Vector2(forward, left);
                float forwardWeight = forward / walkSpeed.magnitude;
                float strafeWeight = left / walkSpeed.magnitude;

                Debug.Log("forwardWeight:" + forwardWeight + "    strafeWeight:" + strafeWeight);
                CharacterAnim.Blend("Armature|Walk", Mathf.Abs(forwardWeight), .1f);
                CharacterAnim.Blend("Armature|Strafe", Mathf.Abs(strafeWeight));
            }
            else
            {
                CharacterAnim.CrossFade("Armature|Standing", .2f);
            }
        }

        //Animation animation = GetComponentInChildren<Animation>();
        //animation.Play("Armature|Walk");
        ////a.Play("Armature|Standing");
        //animation["Armature|Walk"].wrapMode = WrapMode.Loop;
    }

    void OnCollisionEnter(Collision collision)
    {
        // || collision.gameObject.layer == 17 
        //grab onto regular platforms too
        if (collision.gameObject.layer == 19)
        {
            Debug.Log("landed on platform");
            parentPlatform = collision.gameObject.transform;
        }
    }
    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == 19 && collision.gameObject.transform == parentPlatform)
        {
            Debug.Log("left platform");
            parentPlatform = null;
        }
    }


    bool IsGrounded(float padding) {
		float distToGround = GetComponent<Collider>().bounds.extents.y;
        float offset = .25f;
        //center
        if (Physics.Raycast(transform.position, -Vector3.up, distToGround + padding))
        {
            return true;
        }
        //corners
        if (Physics.Raycast(transform.position + new Vector3(offset, 0, -offset), -Vector3.up, distToGround + padding)) return true;
        if (Physics.Raycast(transform.position + new Vector3(offset, 0, offset), -Vector3.up, distToGround + padding)) return true;
        if (Physics.Raycast(transform.position + new Vector3(-offset, 0, -offset), -Vector3.up, distToGround + padding)) return true;
        if (Physics.Raycast(transform.position + new Vector3(-offset, 0, offset), -Vector3.up, distToGround + padding)) return true;

        return false; // Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.5f);
	}
}
