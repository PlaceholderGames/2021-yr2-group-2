using UnityEngine;

public class Water : MonoBehaviour
{
	[Header("Water Surface")]
	public int m_Grid = 64;
    public float m_Length = 10f;
	public enum EWave { Row = 0, Circle, OverlayCurve, Gerstner };
	public EWave m_Wave = EWave.OverlayCurve;
	[Header("Row Wave")]
	[Range(0.1f, 2f)] public float m_RowWaveHeight = 0.1f;
	[Range(0.1f, 4f)] public float m_RowWaveSpeed = 1f;
	[Range(0.1f, 8f)] public float m_RowWaveZScale = 1f;
	[Range(0.1f, 8f)] public float m_RowWaveOffsetScale = 1f;
	[Header("Circle Wave")]
	public GameObject m_WaveSource;
	public float m_WaveFrequency = 0.53f;
	[Range(0.1f, 2f)] public float m_WaveHeight = 0.18f;
	[Range(0.46f, 2f)] public float m_WaveLength = 0.71f;
	[Header("Overlay Wave")]
	[Range(0.1f, 2f)] public float m_OverlayWaveScale = 1f;
	[Range(0.1f, 2f)] public float m_OverlayWaveStrength = 1f;
	[Range(1f, 3f)] public float m_OverlayWaveSpeed = 1f;
	[Header("Gerstner Wave")]
	public float m_GerstnerWaveLength = 1f;
	public float m_GerstnerWaveStretch = 5f;
	public float m_GerstnerWaveSpeed = 1f;
	public float m_GerstnerWaveHeight = 0.7f;
	public float m_GerstnerWaveDirection = 145f;
	[Header("Material Parameters")]
	public Material m_MatWater;
	public Color m_ShallowColor = Color.blue;
	public Color m_DeepColor = Color.blue;
	[Range(0.1f, 1f)] public float m_WaterColorInten = 0.5f;
	public Color m_WaterSpecColor = Color.white;
	[Range(0.1f, 2f)] public float m_SpecPower = 0.5f;
	[Range(0.1f, 2f)] public float m_SpecGloss = 0.5f;
	[Range(0f, 1f)] public float m_Reflection = 0.3f;
	[Range(0f, 1f)] public float m_Refraction = 0.3f;
	[Range(1f, 6f)] public float m_Distortion = 3f;
	[Range(0f, 1f)] public float m_Transparency = 1f;
	public Color m_AmbientColor = Color.grey;
	public Texture2D m_ColorTexture;
	[Header("Intersection Edge")]
	public Color m_WaterEdgeColor = Color.white;
	[Range(3f, 10f)] public float m_EdgeDistance = 8f;
	[Range(0f, 1f)] public float m_EdgeFading = 0f;
	public WaterCamera m_WaterCamera;

	private Mesh mesh;
	private Vector3[] verts;

	void TurnMeshToLowPoly (MeshFilter mf)
	{
		mesh = mf.mesh;
		Vector3[] oldVerts = mesh.vertices;
		Vector2[] oldUvs = mesh.uv;
		int[] triangles = mesh.triangles;
		Vector3[] vertices = new Vector3[triangles.Length];
		Vector2[] uvs = new Vector2[triangles.Length];
		for (int i = 0; i < triangles.Length; i++)
		{
			vertices [i] = oldVerts [triangles [i]];
			uvs [i] = oldUvs [triangles [i]];
			triangles [i] = i;
		}
		mesh.vertices = vertices;
		mesh.uv = uvs;
		mesh.triangles = triangles;
		mesh.RecalculateBounds ();
		mesh.RecalculateNormals ();
		verts = mesh.vertices;
	}
	void Start ()
	{
		Camera.main.depthTextureMode |= DepthTextureMode.Depth;	
		WaterUtils.CreateGridPlane (this.gameObject, m_MatWater, m_Grid, m_Length, m_Length, new Vector3(-m_Length / 2f, 0, -m_Length / 2f));
		
		MeshFilter mf = GetComponent<MeshFilter> ();
		TurnMeshToLowPoly (mf);
		mesh = mf.mesh;
		verts = mesh.vertices;
	}
	void Update ()
	{ 
		CalcWave ();
		m_WaterCamera.DoRender ();
		
		RenderSettings.ambientLight = m_AmbientColor;
		
		Renderer rd = GetComponent<Renderer> ();
		rd.material.SetColor ("_ShallowColor", m_ShallowColor);
		rd.material.SetColor ("_DeepColor", m_DeepColor);
		rd.material.SetColor ("_EdgeColor", m_WaterEdgeColor);
		rd.material.SetColor ("_SpecColor", m_WaterSpecColor);
		rd.material.SetFloat ("_SpecPower", m_SpecPower);
		rd.material.SetFloat ("_SpecGloss", m_SpecGloss);
		rd.material.SetFloat ("_WaterColorIntensity", m_WaterColorInten);
		rd.material.SetFloat ("_Transparency", m_Transparency);
		rd.material.SetTexture ("_ReflectionTex", m_WaterCamera.m_RTReflection);
		rd.material.SetTexture ("_RefractionTex", m_WaterCamera.m_RTRefraction);
		rd.material.SetFloat ("_ReflectionIntensity", m_Reflection);
		rd.material.SetFloat ("_RefractionIntensity", m_Refraction);
		rd.material.SetFloat ("_Distortion", m_Distortion);
		rd.material.SetFloat ("_EdgeDistance", m_EdgeDistance);
		rd.material.SetFloat ("_EdgeIntensity", m_EdgeFading);
		rd.material.SetTexture ("_ColorTex", m_ColorTexture);
	}
	void CalcWave ()
	{
		if (m_Wave == EWave.Circle)
		{
			for (int i = 0; i < verts.Length; i++)
			{
				Vector3 v = verts [i];
				v.y = 0f;
				float dist = Vector3.Distance (v, m_WaveSource.transform.position);
				dist = (dist % m_WaveLength) / m_WaveLength;
				v.y = m_WaveHeight * Mathf.Sin (Time.time * Mathf.PI * 2f * m_WaveFrequency + (Mathf.PI * 2f * dist));
				verts [i] = v;
			}
		}
		else if (m_Wave == EWave.Row)
		{
			for (int i = 0; i < verts.Length; i++)
			{
				Vector3 v = verts [i];
				float phase = Time.time * m_RowWaveSpeed;
				float offset = (v.x + (v.z * m_RowWaveZScale)) * m_RowWaveOffsetScale;
				v.y = Mathf.Sin (phase + offset) * m_RowWaveHeight;
				verts [i] = v;
			}
		}
		else if (m_Wave == EWave.OverlayCurve)
		{
			for (int i = 0; i < verts.Length; i++)
			{
				Vector3 v = verts [i];
				float t = Time.time * m_OverlayWaveSpeed;
				float offset = 0f;
				float scl = m_OverlayWaveScale;
				offset += (Mathf.Sin (v.x * 1f / scl + t * 1f) + Mathf.Sin (v.x * 2.3f / scl + t * 1.5f) + Mathf.Sin (v.x * 3.3f / scl + t * 0.4f)) / 3f;
				offset += (Mathf.Sin (v.z * 0.5f / scl + t * 1.8f) + Mathf.Sin (v.z * 1.8f / scl + t * 1.8f) + Mathf.Sin (v.z * 2.8f / scl + t * 0.8f)) / 3f;
				v.y = m_OverlayWaveStrength * offset;
				verts [i] = v;
			}
		}
		else if (m_Wave == EWave.Gerstner)
		{
			for (int i = 0; i < verts.Length; i++)
			{
				float angle = Mathf.Deg2Rad * m_GerstnerWaveDirection;
				float cos = Mathf.Cos (angle);
				float sin = Mathf.Sin (angle);
				float phase = Time.time * m_GerstnerWaveSpeed;
			
				Vector3 v = verts [i];
				float x = v.x * cos - v.z * sin;
				float z = v.z * cos + v.x * sin;
				float n = Mathf.PerlinNoise (x / m_GerstnerWaveStretch, z / m_GerstnerWaveLength + phase);
				v.y = m_GerstnerWaveHeight * n;
				//float steepness = m_GerstnerWaveSteepness * m_GerstnerWaveLength;
				//v.x = n * sin * steepness;
				//v.z = n * cos * steepness;
				verts [i] = v;
			}
		}
		mesh.vertices = verts;
		mesh.RecalculateNormals (); 
		mesh.MarkDynamic ();
		GetComponent<MeshFilter> ().mesh = mesh;
	}
}