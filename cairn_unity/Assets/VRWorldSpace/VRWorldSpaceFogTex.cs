using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class VRWorldSpaceFogTex : MonoBehaviour
{
	public Camera m_camera;
	public bool m_flip;


	public Texture2D m_fog_texture;
	public float m_texture_scale = 1;


	private Material m_material;
	private Shader m_shader;
	
	void Awake ()
	{
		m_shader = Shader.Find ("Hidden/VRWorldSpaceFogTex");
		m_material = new Material (m_shader);
		Camera.main.depthTextureMode = DepthTextureMode.Depth;

	}

	void OnRenderImage (RenderTexture source, RenderTexture dest)
	{

		m_material.SetFloat ("_flip", m_flip ? -1 : 1);
		m_material.SetTexture ("m_fog_texture", m_fog_texture);
		m_material.SetFloat ("m_texture_scale", 1/m_texture_scale);
		setClipToWorld ();

		Graphics.Blit (source, dest, m_material);
	}


	void setClipToWorld ()
	{
		// this function and comments take from
		// http://gamedev.stackexchange.com/questions/131978/shader-reconstructing-position-from-depth-in-vr-through-projection-matrix


		// To investigate: do we need to use non-jittered version for antialiasing effects?
		var p = m_camera.projectionMatrix;

		// Undo some of the weird projection-y things so it's more intuitive to work with.
		p [2, 3] = p [3, 2] = 0.0f;
		p [3, 3] = 1.0f;

		// I'll confess I don't understand entirely why this is right,
		// I just kept fiddling with numbers until it worked.
		p = Matrix4x4.Inverse (p * m_camera.worldToCameraMatrix)
			* Matrix4x4.TRS (new Vector3 (0, 0, -p [2, 2]), Quaternion.identity, Vector3.one);

		m_material.SetMatrix ("_ClipToWorld", p);
	}
}
