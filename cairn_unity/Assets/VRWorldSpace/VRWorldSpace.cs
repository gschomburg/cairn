using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class VRWorldSpace : MonoBehaviour
{
	public Camera m_camera;
	public bool m_flip;
	public float m_minX = -1;
	public float m_maxX = 1;
	public float m_minY = -1;
	public float m_maxY = 1;
	public float m_minZ = -1;
	public float m_maxZ = 1;

	private Material m_material;
	private Shader m_shader;

	void Awake ()
	{
		m_shader = Shader.Find ("Hidden/VRWorldSpace");
		m_material = new Material (m_shader);
		Camera.main.depthTextureMode = DepthTextureMode.Depth;
	}

	void OnRenderImage (RenderTexture source, RenderTexture dest)
	{
		m_material.SetFloat ("_flip", m_flip ? -1 : 1);
		m_material.SetFloat ("_minX", m_minX);
		m_material.SetFloat ("_maxX", m_maxX);
		m_material.SetFloat ("_minY", m_minY);
		m_material.SetFloat ("_maxY", m_maxY);
		m_material.SetFloat ("_minZ", m_minZ);
		m_material.SetFloat ("_maxZ", m_maxZ);
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
