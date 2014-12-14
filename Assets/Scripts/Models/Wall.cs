using UnityEngine;
using System.Collections;

public struct Wall {

	public enum Shape {
		L,
		I,
		T,
		O
	}

	public Vector3 from;
	public Vector3 to;
	public bool door;

}
