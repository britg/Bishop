using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MeshGenerator {

	public Transform meshContainer;

	static int VERTS_PER_WALL = 8;

	MeshFilter wallsMeshFilter;
	Mesh mesh;
	Vector3[] verts;
	int[] tris;

	public void Generate (List<Vector3> centerPoints) {
		mesh = new Mesh();
		verts = new Vector3[centerPoints.Count * VERTS_PER_WALL];

		// get all verts
		// get all tris

		mesh.vertices = verts;
		mesh.triangles = tris;


	}
}
