using UnityEngine;
using System.Collections;
using System.IO;

public class Utils : MonoBehaviour {

    public static void SetLayer(GameObject obj, int layerIndex)
    {
        obj.layer = layerIndex;
        foreach (Transform child in obj.transform)
        {
            child.gameObject.layer = layerIndex;
        }
    }

    public static void SaveTextureToFile( RenderTexture renderTexture, string path, string name)
    {
        //make sure directory exists
        try
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

        }
        catch (IOException ex)
        {
            //Console.WriteLine(ex.Message)
            Debug.Log(ex.Message);
        }
        RenderTexture currentActiveRT = RenderTexture.active;
        RenderTexture.active = renderTexture;
        Texture2D tex = new Texture2D(renderTexture.width, renderTexture.height);
        tex.ReadPixels(new Rect(0, 0, tex.width, tex.height), 0, 0);
        var bytes = tex.EncodeToPNG();


        File.WriteAllBytes(path + name, bytes);
        UnityEngine.Object.Destroy(tex);
        RenderTexture.active = currentActiveRT;
    }
}
