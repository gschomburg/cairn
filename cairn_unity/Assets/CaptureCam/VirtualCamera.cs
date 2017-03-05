using UnityEngine;
using System.Collections;
using System;

public class VirtualCamera : MonoBehaviour
{
    
    public int image_number = 0;

    public Camera SceneCamera;
    public Camera displayCamera;
    public GameObject Screen;
    string startTime;
    public RenderTexture ScreenTexture; //public just for inspection
    //// Use this for initialization
    void Start()
    {
        startTime = System.DateTime.UtcNow.ToString("yyyy_mm_dd_HH_mm");
        //base.Start();

        displayCamera = Instantiate<Camera>(SceneCamera);
        Destroy(displayCamera.GetComponent<VirtualCamera>());
        Destroy(displayCamera.GetComponent<LinkedCamera>());
        
        displayCamera.transform.parent = SceneCamera.transform;

        ScreenTexture = new RenderTexture(1920, 1080, 24, RenderTextureFormat.ARGB32, RenderTextureReadWrite.sRGB);
        ScreenTexture.antiAliasing = 8;
        SceneCamera.targetTexture = ScreenTexture;
        Screen.GetComponent<Renderer>().material.mainTexture = ScreenTexture;
    }

    //// Update is called once per frame
    //void Update () {

    //}

    //void OnPreRender()
    //{
    //    SceneCamera.targetTexture = ScreenTexture;
    //}
    //void OnPostRender()
    //{
    //    //Debug.Log("OnPostRender");
    //    SceneCamera.targetTexture = null;
    //}


    //public override void UseButtonDown()
    //{
    //    base.UseButtonDown();

    //    //take a photo
    //    Debug.Log("screenshot");
    //}
    public void Capture()
    {
        image_number++;
        string path = Application.streamingAssetsPath + "/Captures";
        Utils.SaveTextureToFile(ScreenTexture, path, "/" + startTime + "_" + image_number +".png");
    }
}
