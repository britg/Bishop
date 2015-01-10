using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Room : GameModel {

	public enum TileType {
		Wall,
		Player,
		Enemy,
		Walkable,
		Margin,
		Gold,
		Gem,
		Door,
		Key
	}

	public enum Type {
		Open,
		Unlock
	}

	public int roomCount;

	public Type type;
	public Bounds bounds;
	public Level level;
//	public TileType[,] tiles;
	public Dictionary<Vector3, TileType> tiles;

	public int enemyCount;
	public int goldCount;
	public int gemCount;
}
