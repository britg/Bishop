using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Room : GameModel {

	public static Bounds DEFAULT_BOUNDS = new Bounds(Vector3.zero, new Vector3(18f, 0, 31f));
	public static Vector3 INSET = new Vector3(0f, 0f, 3f);

	public enum Type {
		Open,
		Unlock
	}

	public Type type;
	public Vector3 dimensions; // default dimensions: (15, 0, 30)

	public int enemyCount;
	public int swordCount;
	public int streamChance;
	public int streamLength;

	public Bounds bounds;

	public List<Vector3> waypoints;

	Bounds fillableBounds;
	public Bounds FillableBounds {
		get {
			if (fillableBounds.size.x < 1f) {
				Vector3 size = bounds.size - Room.INSET;
				fillableBounds = new Bounds(Vector3.zero, size);
			}
			return fillableBounds;
		}
	}

}
