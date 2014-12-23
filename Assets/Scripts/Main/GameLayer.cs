using UnityEngine;
using System.Collections;

public static class GameLayer {

	public static string Walls {
		get {
			return "Walls";
		}
	}

	public static bool isWall (int layer) {
		return layer == LayerMask.NameToLayer(Walls);
	}

	public static bool isWall (GameObject go) {
		return isWall(go.layer);
	}

	public static bool isWall (Collider collider) {
		return isWall(collider.gameObject);
	}

	public static bool isWall (RaycastHit hit) {
		return isWall(hit.collider);
	}

}
