using UnityEngine;
using System.Collections;

public class LinkedCamera : MonoBehaviour
{
    // What transform to chase:
    public Transform target;

    // Debug keyboard controls:
    public KeyCode modifier;
    public KeyCode incSmooth;
    public KeyCode decSmooth;

    private Vector3 moveVel;
    private Vector3 rotVel;

    public float companionCamSmoothingValue = .2f;

    void Awake()
    {
        moveVel = Vector3.zero;
        rotVel = Vector3.zero;
    }

    void Update()
    {
        if (modifier == KeyCode.None || Input.GetKey(modifier))
        {
            if (Input.GetKey(incSmooth))
            {
                companionCamSmoothingValue += 0.001f;
            }
            if (Input.GetKey(decSmooth))
            {
                companionCamSmoothingValue -= 0.001f;
                if (companionCamSmoothingValue < 0) companionCamSmoothingValue = 0;
                if (companionCamSmoothingValue < 0) companionCamSmoothingValue = 0;
            }
        }

        float deltaTime = Time.fixedDeltaTime;

        transform.position = Vector3.SmoothDamp(transform.position, target.position, ref moveVel, companionCamSmoothingValue, Mathf.Infinity, deltaTime);

        Vector3 eulerAngles = this.transform.rotation.eulerAngles;
        Vector3 targetEulerAngles = target.eulerAngles;
        eulerAngles.x = Mathf.SmoothDampAngle(eulerAngles.x, targetEulerAngles.x, ref rotVel.x, companionCamSmoothingValue, Mathf.Infinity, deltaTime);
        eulerAngles.y = Mathf.SmoothDampAngle(eulerAngles.y, targetEulerAngles.y, ref rotVel.y, companionCamSmoothingValue, Mathf.Infinity, deltaTime);
        eulerAngles.z = Mathf.SmoothDampAngle(eulerAngles.z, targetEulerAngles.z, ref rotVel.z, companionCamSmoothingValue, Mathf.Infinity, deltaTime);

        this.transform.rotation = Quaternion.Euler(eulerAngles);
    }
}