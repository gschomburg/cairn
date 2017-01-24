using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class PPVRWorldSpace : MonoBehaviour
{
	public Camera m_camera;

	private Material m_material;
	private Shader m_shader;

	void Awake()
	{
		m_shader = Shader.Find("Hidden/PPVRWorldSpace");
		m_material = new Material(m_shader);
	}

	void OnRenderImage(RenderTexture source, RenderTexture dest)
	{
		Camera.main.depthTextureMode = DepthTextureMode.Depth;
		// Set public values
		//m_material.SetTexture("_MainTex", source);

		// Debug.Log(Camera.main);
		// Calculate inverse view matrix inverse and pass it to the shader
		// m_material.SetMatrix("_InverseViewMatrix", Camera.main.worldToCameraMatrix.inverse);

		setClipToWorld();





		// draw
		Graphics.Blit(source, dest, m_material);
	}


	void setClipToWorld(){
		// this function and comments take from
		// http://gamedev.stackexchange.com/questions/131978/shader-reconstructing-position-from-depth-in-vr-through-projection-matrix


		// To investigate: do we need to use non-jittered version for antialiasing effects?
		var p = m_camera.projectionMatrix;
		// Undo some of the weird projection-y things so it's more intuitive to work with.
		p[2, 3] = p[3, 2] = 0.0f;
		p[3, 3] = 1.0f;

		// I'll confess I don't understand entirely why this is right,
		// I just kept fiddling with numbers until it worked.
		p = Matrix4x4.Inverse(p * m_camera.worldToCameraMatrix) 
			* Matrix4x4.TRS(new Vector3(0, 0, -p[2,2]), Quaternion.identity, Vector3.one);

		m_material.SetMatrix("_ClipToWorld", p);
	}

}


