using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class VRWorldSpaceFog : MonoBehaviour
{
	public Camera m_camera;
	public bool m_flip;

	public float m_distance_min = 0;
	public float m_distance_max = 10;
	public float m_height_min = 5;
	public float m_height_max = 0;
	public Color m_fog_color = new Color(1.0f, 0.0f, 0.0f);

	private Material m_material;
	private Shader m_shader;

	void Awake ()
	{
		m_shader = Shader.Find ("Hidden/VRWorldSpaceFog");
		m_material = new Material (m_shader);
		Camera.main.depthTextureMode = DepthTextureMode.Depth;
	}

	void OnRenderImage (RenderTexture source, RenderTexture dest)
	{
		m_material.SetFloat ("_flip", m_flip ? -1 : 1);
		m_material.SetFloat ("m_distance_min", m_distance_min);
		m_material.SetFloat ("m_distance_max", m_distance_max);
		m_material.SetFloat ("m_height_min", m_height_min);
		m_material.SetFloat ("m_height_max", m_height_max);
		m_material.SetColor ("m_fog_color", m_fog_color);

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
