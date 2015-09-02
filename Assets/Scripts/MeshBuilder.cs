using UnityEngine;
using System.Collections;

public class MeshBuilder : MonoBehaviour {

	public Vector3[] vertices;
	public Vector2[] UV;
	public int[] numOfTriangles;

	// Use this for initialization
	void Start () {
		Mesh mesh = new Mesh ();
		GetComponent<MeshFilter> ().mesh = mesh;
		mesh.vertices = vertices;
		mesh.uv = UV;
		mesh.triangles = numOfTriangles;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
