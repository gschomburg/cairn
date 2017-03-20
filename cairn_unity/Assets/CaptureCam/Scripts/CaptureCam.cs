using UnityEngine;
using System.Collections;
using System;

//TODO
/*
add menu that activates camera

culling layers for camera, camera doens't display itself or controllers

have camera get created and destroyed when actrivated instead of hiding

make screen attach joints get bigger on trigger/collide

add ui to controller when camera is active

canvas menu
    - toggle look at object
    - screen size
    - pause time
    - FOV slider
    - effect sliders
    - animation path
        - add current position
        - clear positions
        -set animation duration
*/

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
        // displayCamera = Instantiate<Camera>(sceneCamera);
        // Destroy(displayCamera.GetComponent<CaptureCam>());
        // Destroy(displayCamera.GetComponent<LinkedCamera>());
        
        // displayCamera.transform.parent = sceneCamera.transform;
        int s =1;
        int w = 1920 *s;
        int h = 1080 *s; 
        ScreenTexture = new RenderTexture(w, h, 24, RenderTextureFormat.ARGB32, RenderTextureReadWrite.sRGB);
        ScreenTexture.antiAliasing = 8;
        sceneCamera.targetTexture = ScreenTexture;
        //Screen.GetComponent<Renderer>().material.mainTexture = ScreenTexture;
        ui.init(this);
    }

    public void Capture()
    {
        image_number++;
        string path = Application.streamingAssetsPath + "/Captures";
        Utils.SaveTextureToFile(ScreenTexture, path, "/" + startTime + "_" + image_number +".png");
    }
}
