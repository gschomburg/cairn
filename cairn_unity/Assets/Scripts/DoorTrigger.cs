using UnityEngine;
using System.Collections;

public class DoorTrigger : MonoBehaviour {
    public GameObject player;
    // Use this for initialization
    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("OnTriggerEnter: " + other.gameObject);
        if (other.gameObject == player)
        {
            open();
        }
    }
    void open()
    {
        transform.parent.gameObject.SetActive(false);
    }
}
