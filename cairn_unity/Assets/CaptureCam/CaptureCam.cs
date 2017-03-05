using UnityEngine;
using System.Collections;
using System;

[RequireComponent( typeof( Camera ) )]
public class CaptureCam : MonoBehaviour
{
    public CaptureCamUI ui;
    public int image_number = 0;

    public Camera sceneCamera;
    public Camera displayCamera;
    // public GameObject Screen;
    string startTime;
    public RenderTexture ScreenTexture; //public just for inspection

    void Start()
    {
        startTime = System.DateTime.UtcNow.ToString("yyyy_mm_dd_HH_mm");
        //base.Start();
        sceneCamera = GetComponent<Camera>();
        displayCamera = Instantiate<Camera>(sceneCamera);
        Destroy(displayCamera.GetComponent<CaptureCam>());
        Destroy(displayCamera.GetComponent<LinkedCamera>());
        
        displayCamera.transform.parent = sceneCamera.transform;

        ScreenTexture = new RenderTexture(1920, 1080, 24, RenderTextureFormat.ARGB32, RenderTextureReadWrite.sRGB);
        ScreenTexture.antiAliasing = 8;
        sceneCamera.targetTexture = ScreenTexture;
        //Screen.GetComponent<Renderer>().material.mainTexture = ScreenTexture;
        ui.init(this);
    }

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
