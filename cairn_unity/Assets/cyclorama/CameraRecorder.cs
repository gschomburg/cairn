using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRecorder : MonoBehaviour
{
    private int real_frame;
    public int start_frame = 1;
    public int end_frame = 60;

    // Use this for initialization
    void Start()
    {

    }

    void Update() {
        if (Input.GetKeyDown("space")){
            Capture();
        }

    }

    void Capture()
    {
        Debug.Log("Capture");
        Application.CaptureScreenshot("render/render.png");
    }


    // Update is called once per frame
    // void OnPostRender()
    // {
    //     real_frame++;
    //     int frameSkip = 2;
    //     if (real_frame % frameSkip > 0)
    //     {
    //         return;
    //     }

    //     string name_prefix = "render/render";
    //     int frame = real_frame / frameSkip;
    //     string screenshot_name = name_prefix + "_" + frame.ToString("000000") + ".png";

    //     if (frame > start_frame && frame < end_frame)
    //     {
    //         // Application.CaptureScreenshot(screenshot_name);
    //         Debug.Log("Render frame: " + frame);

    //         Texture2D tex = new Texture2D(Screen.width, Screen.height);
    //         tex.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
    //         tex.Apply();

    //     	// var bytes = tex.EncodeToPNG();
    //     	// System.IO.File.WriteAllBytes(screenshot_name, bytes);
    //     	// UnityEngine.Object.Destroy(tex);


    //     }
    // }

    // public static void SaveTextureToFile(RenderTexture renderTexture, string name)
    // {
    //     RenderTexture currentActiveRT = RenderTexture.active;
    //     RenderTexture.active = renderTexture;
    //     Texture2D tex = new Texture2D(renderTexture.width, renderTexture.height);
    //     tex.ReadPixels(new Rect(0, 0, tex.width, tex.height), 0, 0);
    //     var bytes = tex.EncodeToPNG();
    //     System.IO.File.WriteAllBytes(name, bytes);
    //     UnityEngine.Object.Destroy(tex);
    //     RenderTexture.active = currentActiveRT;
    // }
}
