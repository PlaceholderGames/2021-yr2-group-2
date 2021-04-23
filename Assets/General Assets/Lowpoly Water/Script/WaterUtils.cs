using UnityEngine;

public class WaterUtils
{
	static public void CreateGridPlane (GameObject empty, Material mat, int grid, float w, float h, Vector3 offset)
    {
		Vector2 cellSize = new Vector2(w / (grid - 1), h / (grid - 1));
		Vector3[] vertices = new Vector3[grid * grid];
		Vector2[] uvs = new Vector2[grid * grid];
		for (int r = 0; r < grid; r++)
		{
			for (int c = 0; c < grid; c++)
			{
				vertices[r * grid + c].x = cellSize.x * c + offset.x;
				vertices[r * grid + c].y = 0 + offset.y;
				vertices[r * grid + c].z = cellSize.y * r + offset.z;
				uvs[r * grid + c].x = (float)r / (float)(grid - 1);
				uvs[r * grid + c].y = (float)c / (float)(grid - 1);
			}
		}
		int[] indices = new int[(grid - 1) * (grid - 1) * 6];
		int n = 0;
		for (int r = 0; r < grid - 1 ; r++)
		{
			for (int c = 0; c < grid - 1; c++)
			{
				indices[n] = r * grid + c;
				indices[n + 1] = (r + 1) * grid + c;
				indices[n + 2] = r * grid + c + 1;
				indices[n + 3] = r * grid + c + 1;
				indices[n + 4] = (r + 1) * grid + c;
				indices[n + 5] = (r + 1) * grid + c + 1;
				n += 6;
			}
		}
		Mesh mesh = new Mesh ();
		mesh.name = "Grid Plane";
		mesh.vertices = vertices;
		mesh.uv = uvs;
		mesh.triangles = indices;
		MeshFilter filter = empty.AddComponent<MeshFilter> ();
		filter.mesh = mesh;
		MeshRenderer rd = empty.AddComponent<MeshRenderer> ();
		rd.material = mat;
    }
	static public void CalculateReflectionMatrix (ref Matrix4x4 m, Vector4 plane)
	{
		m.m00 = (1f - 2f * plane[0] * plane[0]);
	    m.m01 = (   - 2f * plane[0] * plane[1]);
	    m.m02 = (   - 2f * plane[0] * plane[2]);
	    m.m03 = (   - 2f * plane[3] * plane[0]);

	    m.m10 = (   - 2f * plane[1] * plane[0]);
	    m.m11 = (1f - 2f * plane[1] * plane[1]);
	    m.m12 = (   - 2f * plane[1] * plane[2]);
	    m.m13 = (   - 2f * plane[3] * plane[1]);
	
    	m.m20 = (   - 2f * plane[2] * plane[0]);
    	m.m21 = (   - 2f * plane[2] * plane[1]);
    	m.m22 = (1f - 2f * plane[2] * plane[2]);
    	m.m23 = (   - 2f * plane[3] * plane[2]);

    	m.m30 = 0f;
    	m.m31 = 0f;
    	m.m32 = 0f;
    	m.m33 = 1f;
	}
	static public Vector4 CameraSpacePlane (Camera cam, Vector3 pos, Vector3 normal, float sideSign, float clipPlaneOffset)
	{
		Vector3 offsetPos = pos + normal * clipPlaneOffset;
		Matrix4x4 m = cam.worldToCameraMatrix;
		Vector3 cpos = m.MultiplyPoint (offsetPos);
		Vector3 cnormal = m.MultiplyVector (normal).normalized * sideSign;
		return new Vector4 (cnormal.x, cnormal.y, cnormal.z, -Vector3.Dot (cpos, cnormal));
	}
}
