using UnityEngine;
using System.Collections; 

[RequireComponent(typeof(Camera))]
public class WaterCamera : MonoBehaviour
{
	public GameObject m_WaterPlane;
	public RenderTexture m_RTReflection;
	public RenderTexture m_RTRefraction;
	private Camera m_CamReflection;
	private Camera m_CamRefraction;
	private float m_ClipPlaneOffset = 0.07f;
	
	void Start ()
	{
		CreateInternalMaps ();
		CreateInternalCameras (gameObject.transform);
	}
	public void DoRender ()
	{
		if (!enabled)
			return;

		Camera cam = GetComponent<Camera> ();
		if (cam)
		{
			SyncCameraParameters (cam, m_CamReflection);
			RenderToReflectionMap ();
			SyncCameraParameters (cam, m_CamRefraction);
			RenderToRefractionMap ();
		}
	}
	void CreateInternalMaps ()
	{
		m_RTReflection = new RenderTexture (256, 256, 16);
		m_RTReflection.name = "Reflection";
		m_RTReflection.autoGenerateMips = false;
		m_RTReflection.isPowerOfTwo = true;
		
		m_RTRefraction = new RenderTexture (256, 256, 16);
		m_RTRefraction.name = "Refraction";
		m_RTRefraction.autoGenerateMips = false;
		m_RTRefraction.isPowerOfTwo = true;
	}
	void CreateInternalCameras (Transform parent)
	{
		// reflection camera
		GameObject obj = new GameObject ("Reflection", typeof (Camera), typeof (Skybox));
		obj.transform.parent = parent;
		m_CamReflection = obj.GetComponent<Camera> ();
		m_CamReflection.enabled = false;
		m_CamReflection.transform.position = transform.position;
		m_CamReflection.transform.rotation = transform.rotation;
		m_CamReflection.targetTexture = m_RTReflection;
		m_CamReflection.cullingMask = ~(1 << LayerMask.NameToLayer ("Water"));  // not render water self
		
		// refraction camera
		obj = new GameObject ("Refraction", typeof (Camera), typeof (Skybox));
		obj.transform.parent = parent;
		m_CamRefraction = obj.GetComponent<Camera> ();
		m_CamRefraction.enabled = false;
		m_CamRefraction.transform.position = transform.position;
		m_CamRefraction.transform.rotation = transform.rotation;
		m_CamRefraction.targetTexture = m_RTRefraction;
		m_CamRefraction.cullingMask = ~(1 << LayerMask.NameToLayer ("Water"));  // not render water self
	}
	void RenderToReflectionMap ()
	{
		// reflection plane's position and normal in world space
		Vector3 pos = m_WaterPlane.transform.position;
		Vector3 normal = m_WaterPlane.transform.up;
			
		// Reflect camera around reflection plane
		float d = -Vector3.Dot (normal, pos) - m_ClipPlaneOffset;
		Vector4 reflectionPlane = new Vector4 (normal.x, normal.y, normal.z, d);
		
		Matrix4x4 reflection = Matrix4x4.zero;
		WaterUtils.CalculateReflectionMatrix (ref reflection, reflectionPlane);
		Camera cam = GetComponent<Camera> ();
		Vector3 oldpos = cam.transform.position;
		Vector3 newpos = reflection.MultiplyPoint (oldpos);
		m_CamReflection.worldToCameraMatrix = cam.worldToCameraMatrix * reflection;
		
		// render objects up side of water plane
		Vector4 clipPlane = WaterUtils.CameraSpacePlane (m_CamReflection, pos, normal, 1f, m_ClipPlaneOffset);
		m_CamReflection.projectionMatrix = cam.CalculateObliqueMatrix (clipPlane);
		GL.invertCulling = true;
		m_CamReflection.transform.position = newpos;
		Vector3 euler = cam.transform.eulerAngles;
		m_CamReflection.transform.eulerAngles = new Vector3 (-euler.x, euler.y, euler.z);
		m_CamReflection.Render ();
		m_CamReflection.transform.position = oldpos;
		GL.invertCulling = false;
	}
	void RenderToRefractionMap ()
	{
		Vector3 pos = m_WaterPlane.transform.position;
		Vector3 normal = m_WaterPlane.transform.up;
		
		Camera cam = GetComponent<Camera> ();
		m_CamRefraction.worldToCameraMatrix = cam.worldToCameraMatrix;
		
		// render objects bottom side of water plane
		Vector4 clipPlane = WaterUtils.CameraSpacePlane (m_CamRefraction, pos, normal, -1f, m_ClipPlaneOffset);
		m_CamRefraction.projectionMatrix = cam.CalculateObliqueMatrix (clipPlane);
		m_CamRefraction.transform.position = cam.transform.position;
		m_CamRefraction.transform.rotation = cam.transform.rotation;
		m_CamRefraction.Render ();
	}
	void SyncCameraParameters (Camera src, Camera dst)
	{
		if (dst == null)
			return;

		if (src.clearFlags == CameraClearFlags.Skybox)
		{
			Skybox sky = src.GetComponent (typeof (Skybox)) as Skybox;
			Skybox mysky = dst.GetComponent (typeof (Skybox)) as Skybox;
			if (!sky || !sky.material)
			{
				mysky.enabled = false;
			}
			else
			{
				mysky.enabled = true;
				mysky.material = sky.material;
			}
		}
		dst.clearFlags = src.clearFlags;
		dst.backgroundColor = src.backgroundColor;
		dst.farClipPlane = src.farClipPlane;
		dst.nearClipPlane = src.nearClipPlane;
		dst.orthographic = src.orthographic;
		dst.fieldOfView = src.fieldOfView;
		dst.aspect = src.aspect;
		dst.orthographicSize = src.orthographicSize;
	}
}