using UnityEngine;
using System.Collections;

public class FloorSwitch : MonoBehaviour {
    public GameObject player;
    public bool pressurePlate;
    public bool isOn;

    public SwitchDoor target;

    public Color onColor;
    Color offColor;

    // Use this for initialization
    void Start () {
        offColor = GetComponent<Renderer>().material.GetColor("_Color");
    }
    
    void setState(bool val)
    {
        //send out messages
        if (val != isOn)
        {
            isOn = val;
            //send the message to the game object
            target.setState(isOn);
            if (isOn)
            {
                GetComponent<Renderer>().material.SetColor("_Color", onColor);
            }
            else
            {
                GetComponent<Renderer>().material.SetColor("_Color", offColor);
            }
        }
    }
    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("OnTriggerEnter: " + other.gameObject);
        if (other.gameObject == player)
        {
            setState(!isOn);
        }
    }
    void OnTriggerExit(Collider other)
    {
        //Debug.Log("OnTriggerEnter: " + other.gameObject);
        if (other.gameObject == player && pressurePlate)
        {
            setState(false);
        }
    }
}
