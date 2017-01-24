using UnityEngine;
using System.Collections;
[ExecuteInEditMode]
public class WorldSpacePostProcess : MonoBehaviour
{

	public Camera m_camera;

	private Material m_material;
	private Shader m_shader;
	
	void Awake()
	{
		m_shader = Shader.Find("PostProcess/WorldSpace");
		m_material = new Material(m_shader);
	}

	void OnRenderImage(RenderTexture source, RenderTexture dest)
	{
		Camera.main.depthTextureMode = DepthTextureMode.Depth;
		// Set public values
		//m_material.SetTexture("_MainTex", source);
		// Debug.Log(Camera.main == m_camera);

		// Calculate inverse view matrix inverse and pass it to the shader
		m_material.SetMatrix("_InverseViewMatrix", Camera.main.worldToCameraMatrix.inverse);

		// draw
		Graphics.Blit(source, dest, m_material);
	}

}