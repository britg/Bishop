using UnityEngine;
using System.Collections;

[System.Serializable]
public class Room : GameModel {

	public enum Type {
		Open,
		Unlock
	}

	public Type type;
	public Vector3 dimensions; // default dimensions: (15, 0, 30)

	public int enemyCount;
	public int swordCount;

}
