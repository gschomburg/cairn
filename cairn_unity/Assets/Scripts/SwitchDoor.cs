using UnityEngine;
using System.Collections;

public class SwitchDoor : MonoBehaviour {
    bool isOpen;
    //// Use this for initialization
    //void Start () {

    //}

    //// Update is called once per frame
    //void Update () {

    //}
    float openDistance = .2f;
    public void setState(bool val)
    {
        if (isOpen == val) return;
        isOpen = val;
        if (isOpen)
        {
            open();
        }
        else
        {
            close();
        }
    }
    public void open()
    {
        transform.localPosition = transform.localPosition + new Vector3(0f, openDistance, 0f);
    }
    public void close()
    {
        transform.localPosition = transform.localPosition + new Vector3(0f, -openDistance, 0f);
    }
}
