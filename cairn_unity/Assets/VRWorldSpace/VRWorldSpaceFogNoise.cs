using UnityEngine;
using System.Collections;


[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class VRWorldSpaceFogNoise : MonoBehaviour
{

	// device properties
	[Header("Device Settings")]
	[SerializeField][Tooltip("Flip the image horizontally?")]
	public bool m_horizontal_flip;

	// global fog properties
	[Header("Global Fog Settings")]
	[SerializeField][Tooltip("Color of fog. Use alpha to control max strength.")]
	private Color m_fog_color = new Color(1.0f, 0.0f, 0.0f);

	// distance fog properties
	[Header("Distance Fog Settings")]
	[SerializeField][Tooltip("Distance from world center where fog should begin")]
	private float m_distance_min = 0;
	[SerializeField][Tooltip("Distance from world center where fog should reach max opacity")]
	private float m_distance_max = 10;

	// ground fog properties
	[Header("Ground Fog Settings")]
	[SerializeField][Tooltip("Altitude where fog should begin")]
	private float m_height_min = 5;
	[SerializeField][Tooltip("Altitude where fog should reach max opacity")]
	private float m_height_max = 0;

	// texture fog properties
	[Header("Texture Fog Settings")]
	[SerializeField]
	private Cubemap m_primary_noise;
	[SerializeField]
	private Cubemap m_secondary_noise;
	[SerializeField]
	private float m_noise_speed = 1;
	[SerializeField]
	private float m_noise_intensity = 1;


	private Camera m_camera;
	private Shader m_shader;
	private Material m_material;



	void Awake ()
	{
		m_camera = GetComponent<Camera>();
		m_shader = Shader.Find ("Hidden/VRWorldSpaceFogNoise");
		m_material = new Material (m_shader);
		Camera.main.depthTextureMode = DepthTextureMode.Depth;
	}

	[ImageEffectOpaque]
	void OnRenderImage (RenderTexture source, RenderTexture dest)
	{
		m_material.SetFloat ("_flip", m_horizontal_flip ? -1 : 1);

		m_material.SetColor ("m_fog_color", m_fog_color);

		m_material.SetFloat ("m_distance_min", m_distance_min);
		m_material.SetFloat ("m_distance_max", m_distance_max);

		m_material.SetFloat ("m_height_min", m_height_min);
		m_material.SetFloat ("m_height_max", m_height_max);

		m_material.SetTexture ("m_primary_noise", m_primary_noise);
		m_material.SetTexture ("m_secondary_noise", m_secondary_noise);
		m_material.SetFloat ("m_noise_speed", m_noise_speed);
		m_material.SetFloat ("m_noise_intensity", m_noise_intensity);

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
