using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockSlide : MonoBehaviour {
	private Vector3 positionOffset;
	public Transform target;

	[Range(.001f,.5f)]
	public float position_stiff = .04f;
	[Range(.001f,.5f)]
	public float rotation_stiff = .01f;


	[Range(1.0f,100.0f)]
	public float follow_speed = 10.0f;

	private Rigidbody rb;

	// Use this for initialization
	void Start () {
        //positionOffset = transform.position - target.position;

        positionOffset = target.worldToLocalMatrix.MultiplyPoint3x4(transform.position);

        //positionOffset = Random.onUnitSphere * .10f;

        follow_speed *= Random.value + .1f;
		rb = GetComponent<Rigidbody>();
	}

	// Update is called once per frame
	void FixedUpdate () {

		Vector3 targetPosition = target.localToWorldMatrix.MultiplyPoint3x4(positionOffset);


        // immediate
        // transform.position = targetPosition;
        // transform.rotation = target.rotation * Quaternion.AngleAxis(-90.0f, Vector3.right);

        // lerp
        // transform.position = (Vector3.Lerp(transform.position, targetPosition, position_stiff));
        // transform.rotation = (Quaternion.Slerp(transform.rotation, target.rotation, rotation_stiff));

        // lerp physics
        //rb.MovePosition(Vector3.Lerp(transform.position, targetPosition, position_stiff));
        rb.MoveRotation(Quaternion.Slerp(transform.rotation, target.rotation * Quaternion.AngleAxis(-45.0f, Vector3.right), rotation_stiff));

        // max distance
        Vector3 fix = targetPosition - transform.position;
        if (fix.magnitude > 0.5f)
        {
            rb.MovePosition(targetPosition - fix.normalized * 0.5f);
        }

        // force
        // rb.AddForce((targetPosition - transform.position).normalized * 100.0f);
        rb.AddForce(
            (targetPosition - transform.position).normalized *
            Vector3.Distance(targetPosition, transform.position) * follow_speed

        );


        // Vector3 x = Vector3.Cross(transform.forward, target.forward);
        // rb.AddTorque(x * 10.0f, ForceMode.Force);
    }
}
