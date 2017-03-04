using UnityEngine;
using System.Collections;

public class Utils : MonoBehaviour {

    public static void SetLayer(GameObject obj, int layerIndex)
    {
        obj.layer = layerIndex;
        foreach (Transform child in obj.transform)
        {
            child.gameObject.layer = layerIndex;
        }
    }

    public static void SaveTextureToFile( RenderTexture renderTexture, string name)
    {
        RenderTexture currentActiveRT = RenderTexture.active;
        RenderTexture.active = renderTexture;
        Texture2D tex = new Texture2D(renderTexture.width, renderTexture.height);
        tex.ReadPixels(new Rect(0, 0, tex.width, tex.height), 0, 0);
        var bytes = tex.EncodeToPNG();
        System.IO.File.WriteAllBytes(name, bytes);
        UnityEngine.Object.Destroy(tex);
        RenderTexture.active = currentActiveRT;
    }
}
