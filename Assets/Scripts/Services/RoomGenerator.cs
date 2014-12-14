using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoomGenerator : GameBehaviour {

	enum TileType {
		Floor,
		Wall
	}

	public GameObject wallContainer;
	public GameObject wallPrefab;
	public Vector3 dimensions;

	Hashtable tiles;

	// Use this for initialization
	void Start () {
//		SeedTiles();
//		CreateWalls();
//		CreatePaths();
//		PlaceObjects();
	}

	void SeedTiles () {
		tiles = new Hashtable();
		for (int x = 0; x <= dimensions.x; x++) {
			for (int z = 0; z <= dimensions.z; z++) {
				Vector3 pos = new Vector3(x, 0f, z);
				tiles[pos] = TileType.Floor;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void CreateWalls () {
		CreateSouthWall(true);
		CreateNorthWall(true);
		CreateEastWall(true);
		CreateWestWall(true);
	}

	void CreatePaths () {

	}

	void PlaceObjects () {
		foreach (DictionaryEntry kv in tiles) {
			Vector3 pos = (Vector3)kv.Key;
			TileType val = (TileType)kv.Value;

			if (val == TileType.Wall) {
				var wallObj = (GameObject)Instantiate(wallPrefab);
				wallObj.transform.SetParent(wallContainer.transform);
				wallObj.transform.position = pos;
			}
		}
	}

	void CreateSouthWall (bool door) {
		Vector3 pos = Vector3.zero;
		for (int x = 0; x <= dimensions.x; x++) {

			pos.x = -dimensions.x/2f + x;
			pos.y = 0f;
			pos.z = -dimensions.z/2f;

			if (door && pos.x >= -1 && pos.x < 1) {
				continue;
			}

			tiles[pos] = TileType.Wall;
		}
	}

	void CreateNorthWall (bool door) {
		Vector3 pos = Vector3.zero;
		for (int x = 0; x <= dimensions.x; x++) {
			pos.x = -dimensions.x/2f + x;
			pos.y = 0f;
			pos.z = dimensions.z/2f;

			if (door && pos.x >= -1 && pos.x < 1) {
				continue;
			}
			tiles[pos] = TileType.Wall;
		}
	}

	void CreateEastWall (bool door) {
		Vector3 pos = Vector3.zero;
		for (int z = 1; z <= dimensions.z; z++) {
			pos.x = -dimensions.x/2f;
			pos.y = 0f;
			pos.z = -dimensions.z/2f + z;

			if (door && pos.z >= -1 && pos.z < 1) {
				continue;
			}

			tiles[pos] = TileType.Wall;
		}
	}

	void CreateWestWall (bool door) {
		Vector3 pos = Vector3.zero;
		for (int z = 1; z <= dimensions.z; z++) {
			pos.x = dimensions.x/2f;
			pos.y = 0f;
			pos.z = -dimensions.z/2f + z;

			if (door && pos.z >= -1 && pos.z < 1) {
				continue;
			}

			tiles[pos] = TileType.Wall;
		}
	}
}
