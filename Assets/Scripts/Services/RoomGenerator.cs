using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoomGenerator {

	float Y = 0f;
	Room room;

	public float keyRoomChance;

	Vector3 size {
		get {
			return room.bounds.size;
		}
	}

	Vector3 extents {
		get {
			return room.bounds.extents;
		}
	}

	public RoomGenerator () {

	}

	public RoomGenerator (int seed) {
		Random.seed = seed;
	}

	public Room Generate (Room _room) {
		room = _room;
		room.tiles = new Dictionary<Vector3,Room.TileType>();
		DefineWalls();
		DefineMaze();
		DefineEnemies();
		DefinePickup(room.goldCount, Room.TileType.Gold);
		DefinePickup(room.gemCount, Room.TileType.Gem);
		EnsurePath();
		DefineKeyRoom();
		return room;
	}

	void ChooseBounds () {
		// TODO bounds should be based on the level
	}

	Vector3 RandomTile () {
		var r = new Vector3(Random.Range(-(int)extents.x, (int)extents.x), Y, Random.Range(-(int)extents.z, (int)extents.z));
		return r + new Vector3(0.5f, 0f, 0.5f);
	}

	void DefineWalls () {
		for (int x = -(int)extents.x; x <= (int)extents.x; x++) {
			for (int z = -(int)extents.z; z <= (int)extents.z; z++) {
				bool lr = x == -(int)extents.x || x == (int)extents.x;
				bool tb = z == -(int)extents.z || z == (int)extents.z;
				var tile = new Vector3(x, Y, z);
				if (lr) {
					SetWallTile(tile);
				} 
			}
		}
	}

	Vector3 tl = new Vector3(-0.5f, 0f, 0.5f);
	Vector3 tr = new Vector3(0.5f, 0f, 0.5f);
	Vector3 bl = new Vector3(-0.5f, 0f, -0.5f);
	Vector3 br = new Vector3(0.5f, 0f, -0.5f);
	void SetWallTile (Vector3 center) {
		room.tiles[center] = Room.TileType.Wall;
		room.tiles[center + tl] = Room.TileType.Margin;
		room.tiles[center + tr] = Room.TileType.Margin;
		room.tiles[center + bl] = Room.TileType.Margin;
		room.tiles[center + br] = Room.TileType.Margin;
	}


	List<Vector3> pillars = new List<Vector3>();
	List<Vector3> connectedPillars = new List<Vector3>();
	void DefineMaze () {
		float spacing = 3f;
		for (float x = 0f; x <= size.x; x += spacing) {
			for (float z = spacing; z <= size.z; z += spacing) {
				Vector3 tile = new Vector3(x, Y, z) - extents;
				pillars.Add(tile);
				SetWallTile(tile);
			}
		}

		ConnectAllPillars();
	}

	void ConnectRandomPillars () {
		for (int i = 0; i < size.x; i++) {
			int r = Random.Range(0, pillars.Count-1);
			ConnectPillar(pillars[r]);
		}
	}

	void ConnectAllPillars () {
		foreach (Vector3 pillar in pillars) {
			ConnectPillar(pillar);
		}
	}


	List<Vector3> dirs = new List<Vector3>{ Vector3.forward, Vector3.right, Vector3.back, Vector3.left };
	void ConnectPillar (Vector3 pillar) {
		Vector3 rdir = dirs[Random.Range(0, dirs.Count-1)];
		Vector3 neighbor = rdir * 3f + pillar;
		if (connectedPillars.Contains(neighbor)) {
			return;
		}

		if (pillars.Contains(neighbor)) {
			var one = pillar + rdir;
			var two = pillar + rdir*2f;
			SetWallTile(one);
			SetWallTile(two);
			connectedPillars.Add(neighbor);
		}
	}

	void DefineWaypoints () {
		for (float x = 0.5f; x < size.x; x += 1f) {
			for (float z = 0.5f; z < size.z; z += 1f) {
				var tile = new Vector3(x, Y, z) - extents;
				if (!room.tiles.ContainsKey(tile)) {
					room.tiles[tile] = Room.TileType.Walkable;
				}
			}
		}
	}


	void DefineEnemies () {
		for (int i = 0; i < room.enemyCount; i++) {
			DefineRandomEnemy();
		}
	}

	void DefineRandomEnemy () {
		var randomTile = RandomTile();
		if (randomTile.z < -extents.z + 5f) {
			DefineRandomEnemy();
			return;
		}
		if (room.tiles.ContainsKey(randomTile) && room.tiles[randomTile] != Room.TileType.Walkable) {
			DefineRandomEnemy();
			return;
		}
		room.tiles[randomTile] = Room.TileType.Enemy;
	}

	void DefinePickup (int count, Room.TileType type) {
		for (int i = 0; i < count; i++) {
			DefineRandomPickup(type);
		}
	}

	void DefineRandomPickup (Room.TileType type) {
		var randomTile = RandomTile();
		if (randomTile.z < -extents.z + 5f) {
			DefineRandomPickup(type);
			return;
		}
		if (room.tiles.ContainsKey(randomTile) && room.tiles[randomTile] != Room.TileType.Walkable) {
			DefineRandomPickup(type);
			return;
		}
		room.tiles[randomTile] = type;
	}

	void EnsurePath () {
		int startX = -(int)extents.x+1;
		Vector3 tile;
		for (int z = -(int)extents.z; z < (int)extents.z; z++) {
			tile = new Vector3(startX, Y, z);
			TestStartWall(tile);
		}
	}

	bool wallHasSpace = false;
	List<Vector3> wallTestTiles = new List<Vector3>();
	void TestStartWall (Vector3 tile) {
		Room.TileType type;

		if (room.tiles.ContainsKey(tile)) {
			type = room.tiles[tile];
			if (type == Room.TileType.Wall) {
				Vector3 oneLeft = tile+Vector3.left;
				wallTestTiles = new List<Vector3>{ oneLeft, tile };
				wallHasSpace = false;
				while (!wallHasSpace) {
					ContinueWallTest();
				}
			}
		}
	}

	void ContinueWallTest () {
		Vector3 prevTile = wallTestTiles[wallTestTiles.Count-2];
		Vector3 currentTile = wallTestTiles[wallTestTiles.Count-1];
		List<Vector3> toTest = new List<Vector3>{ currentTile+Vector3.forward,
												  currentTile+Vector3.back,
												  currentTile+Vector3.right,
												  currentTile+Vector3.left };
													
		toTest.Remove(prevTile);
		bool atLeastOneWall = false;
		Vector3 nextTestTile = currentTile + Vector3.right;
		foreach (Vector3 testTile in toTest) {
			if (room.tiles.ContainsKey(testTile) && room.tiles[testTile] == Room.TileType.Wall) {
				atLeastOneWall = true;
				nextTestTile = testTile;
				if (nextTestTile.x == extents.x) {
					Debug.Log ("Complete wall! " + nextTestTile);
					wallTestTiles.Add(nextTestTile);
					BreakWall();
					return;
				}
			}
		}

		if (atLeastOneWall) {
			wallTestTiles.Add(nextTestTile);
		} else {
			wallHasSpace = true;
		}
	}

	void BreakWall () {
		room.tiles[wallTestTiles[1]] = Room.TileType.Walkable;
		room.tiles[wallTestTiles[2]] = Room.TileType.Walkable;
		wallHasSpace = true;
	}

	void DefineKeyRoom () {
		if (!GameBehaviour.Roll(keyRoomChance)) {
			return;
		}

		Debug.Log ("Generating a Key Room");
		float z = extents.z;
		Vector3 tile;
		for (float x = -extents.x; x <= extents.x; x += 1f) {
			tile = new Vector3(x, Y, z);
			room.tiles[tile] = Room.TileType.Wall;
		}

		Vector3 doorStartTile = new Vector3(0f, Y, z);
		List<Vector3> doorTiles = new List<Vector3>{
			doorStartTile,
			doorStartTile + Vector3.right,
			doorStartTile + Vector3.right + Vector3.back,
			doorStartTile + Vector3.right + Vector3.back*2,
			doorStartTile + Vector3.back,
			doorStartTile + Vector3.back*2
		};

		foreach (Vector3 doorTile in doorTiles) {
			room.tiles[doorTile] = Room.TileType.Walkable;
		}

		room.tiles[doorStartTile - new Vector3(0.5f, 0f, 0f)] = Room.TileType.Door;
		DefinePickup(1, Room.TileType.Key);
	}

}
