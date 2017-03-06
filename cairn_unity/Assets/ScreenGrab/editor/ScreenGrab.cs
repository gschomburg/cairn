using UnityEngine;
using UnityEditor;

public class ScreenGrab : MonoBehaviour {

	[MenuItem("ScreenGrab/Save Screengrab")]
	static void interactiveScreenGrab() {
		Debug.Log("Screen Grab");
		Debug.Log(Camera.main);
        Camera cam = Camera.main;
        
		// // create render
		RenderTexture render = RenderTexture.GetTemporary(1920, 1080, 24, RenderTextureFormat.DefaultHDR, RenderTextureReadWrite.Default, 1);
        RenderTexture tempTargetTexture = cam.targetTexture;
		cam.targetTexture = render;
		cam.Render();
		

		// // read render texture pixels
		RenderTexture tempActiveRenderTexture = RenderTexture.active;
        RenderTexture.active = render;
        Texture2D tex = new Texture2D(render.width, render.height);
		// revist: public Texture2D(int width, int height, TextureFormat format, bool mipmap, bool linear);
        tex.ReadPixels(new Rect(0, 0, tex.width, tex.height), 0, 0);
		tex.Apply();

		// // encode as png, save out
        var bytes = tex.EncodeToPNG();
        System.IO.File.WriteAllBytes("render.png", bytes);
        

		Texture2D texHDR = new Texture2D(render.width, render.height, TextureFormat.RGBAHalf, false);
		// revist: public Texture2D(int width, int height, TextureFormat format, bool mipmap, bool linear);
        texHDR.ReadPixels(new Rect(0, 0, tex.width, tex.height), 0, 0);
		texHDR.Apply();

		Color[] colors = texHDR.GetPixels(0);
		Debug.Log(colors[0]);
        MiniEXR.MiniEXR.MiniEXRWriteRGBA("render.exr", 1920, 1080, colors);
        
		// // clean up
		RenderTexture.active = tempActiveRenderTexture;
		RenderTexture.ReleaseTemporary(render);
		cam.targetTexture = tempTargetTexture;
		cam.ResetAspect();

	}
// related http://williamchyr.com/2013/11/unity-shaders-depth-and-normal-textures/

// related http://answers.unity3d.com/questions/9969/convert-a-rendertexture-to-a-texture2d.html

// 	public static RenderTexture Snapshot(int width, int height) {

//        var result = RenderTexture.GetTemporary(width, height, 24);

//        if (CaptureTheGIF.captureCamera != null)
//        {
//            Camera cam = CaptureTheGIF.captureCamera;
//            var old = cam.targetTexture;
//            cam.targetTexture = result;
//            cam.Render();
//            cam.targetTexture = old;
//            cam.ResetAspect();

//            return result;
//        }
       
//        foreach(var cam in Camera.allCameras.OrderBy(c => c.depth)) {

//            var old = cam.targetTexture;
//            cam.targetTexture = result;
//            cam.Render();
//            cam.targetTexture = old;
//            cam.ResetAspect();
//        }
//        return result;
//    }


}
