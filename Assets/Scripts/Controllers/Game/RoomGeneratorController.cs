using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoomGeneratorController : GameController {

	Player player;

	RoomGenerator roomGenerator;
	Room nextRoom;
	float nextRoomOffset = 0f;
	GameObject roomObj;
	GameObject wallContainer;
	GameObject spawnContainer;
	GameObject waypointContainer;

	public Room roomTemplate;

	public float fogHeight = 3f;
	public GameObject roomPrefab;
	public GameObject rail;


	public GameObject wallPrefab;
	public GameObject enemySpawnerPrefab;
	public GameObject waypointPrefab;
	public GameObject goldPrefab;

	public GameObject leftFog;
	public GameObject topFog;
	public GameObject rightFog;
	public GameObject bottomFog;


	// Use this for initialization
	void Start () {
		player = GetPlayer();
		BuildRoom();
		PlacePlayer(new Vector3(0f, 0f, -nextRoom.bounds.extents.z));
//		for (int i = 0; i < 10; i++) {
//			BuildRoom();
//		}
//		InvokeRepeating("BuildRoom", 10f, 10f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void BuildRoom () {
		roomGenerator = new RoomGenerator();
		nextRoom = roomGenerator.Generate(roomTemplate);
		PlaceRoomTemplate();
		AdjustFog();
		PlaceTiles();
		roomObj.GetComponent<RoomController>().Activate(nextRoom);
		nextRoomOffset += roomTemplate.bounds.size.z;
	}

	void PlaceRoomTemplate () {
		roomObj = (GameObject)Instantiate(roomPrefab);
		roomObj.transform.position = new Vector3(0f, 0f, nextRoomOffset);
		wallContainer = roomObj.transform.FindChild("Walls").gameObject;
		spawnContainer = roomObj.transform.FindChild("EnemySpawns").gameObject;
		waypointContainer = roomObj.transform.FindChild("Waypoints").gameObject;
	}

	void AdjustFog () {
		leftFog.transform.position = new Vector3(-nextRoom.bounds.extents.x-0.5f, fogHeight, 0f);
		rightFog.transform.position = new Vector3(nextRoom.bounds.extents.x+0.5f, fogHeight, 0f);
		topFog.transform.position = new Vector3(0f, fogHeight, nextRoom.bounds.extents.z + nextRoomOffset + 0.5f);
		bottomFog.transform.position = new Vector3(0f, fogHeight, -nextRoom.bounds.extents.z-0.5f);
	}

	void PlaceTiles () {
		Room.TileType type;
		foreach (KeyValuePair<Vector3, Room.TileType> kv in nextRoom.tiles) {
			var pos = kv.Key;
			pos.z += nextRoomOffset;
			if (kv.Value == Room.TileType.Wall) {
				PlaceWall(pos);
			}

			if (kv.Value == Room.TileType.Enemy) {
				PlaceEnemySpawner(pos);
				PlaceWaypoint(pos);
			}

			if (kv.Value == Room.TileType.Gold) {
				PlaceGold(pos);
				PlaceWaypoint(pos);
			}

			if (kv.Value == Room.TileType.Walkable) {
				PlaceWaypoint(pos);
			}

		}
	}

	void PlaceWall (Vector3 pos) {
		GameObject wall = (GameObject)Instantiate(wallPrefab, pos, Quaternion.identity);
		wall.transform.parent = wallContainer.transform;
		wall.name = string.Format("{0},{1}", pos.x, pos.z);
	}

	void PlacePlayer (Vector3 pos) {
		player.go.transform.position = pos;
		rail.transform.position = pos;
	}

	void PlaceEnemySpawner (Vector3 pos) {
		GameObject enemySpawner = (GameObject)Instantiate(enemySpawnerPrefab, pos, Quaternion.identity);
		enemySpawner.transform.parent = spawnContainer.transform;
	}

	void PlaceWaypoint (Vector3 pos) {
		GameObject waypoint = (GameObject)Instantiate(waypointPrefab, pos, Quaternion.identity);
		waypoint.transform.parent = waypointContainer.transform;
	}

	void PlaceGold (Vector3 pos) {
		GameObject gold = (GameObject)Instantiate(goldPrefab, pos, Quaternion.identity);
		gold.transform.SetParent(waypointContainer.transform);
	}
}
