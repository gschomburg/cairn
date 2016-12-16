using UnityEngine;
using System.Collections;

public class KeySlot : MonoBehaviour {
    public GameObject door;
    public GameObject key;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	//void Update () {
	
	//}
    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("OnTriggerEnter: " + other.gameObject);
        if (other.gameObject == key)
        {
            door.SetActive(false);
        }
    }

    //void OnTriggerExit(Collider other)
    //{

    //    if (other.gameObject == lockPoint)
    //    {
    //        lockPoint = null;
    //    }
    //}
}
