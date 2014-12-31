using UnityEngine;
using System.Collections;

public static class GameLayer {

	public static string Walls {
		get {
			return "Walls";
		}
	}

	public static string Player {
		get {
			return "Player";
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

	public static bool isPlayer (int layer) {
		return layer == LayerMask.NameToLayer(Player);
	}

	public static bool isPlayer (GameObject go) {
		return isPlayer(go.layer);
	}

	public static bool isPlayer (Collider collider) {
		return isPlayer(collider.gameObject);
	}

	public static bool isPlayer (RaycastHit hit) {
		return isPlayer(hit.collider);
	}

}
