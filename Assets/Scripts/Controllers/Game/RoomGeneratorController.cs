using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoomGeneratorController : GameController {

	Player player;

	RoomGenerator roomGenerator;
	Room currentRoom;
	float nextBuildOffset = 0f;
	GameObject currentRoomObj;
	GameObject wallContainer;
	GameObject spawnContainer;
	GameObject waypointContainer;

	public Room roomTemplate;

	public float fogHeight = 3f;
	public GameObject roomPrefab;
	public int roomCountToGenerate;
	public GameObject rail;
	float nextRoomTrigger = 0f;
	List<GameObject> roomObjs;


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
		roomObjs = new List<GameObject>();
		for (int i = 0; i < roomCountToGenerate; i++) {
			BuildRoom();
		}
		nextRoomTrigger = roomTemplate.bounds.size.z * (roomCountToGenerate-2);
		PlacePlayer(new Vector3(0f, 0f, -currentRoom.bounds.extents.z));
		StartFog(roomTemplate);
//		ActivateNextRoom();
	}
	
	// Update is called once per frame
	void Update () {
		CheckRailPosition();
	}

	void CheckRailPosition () {
		if (rail.transform.position.z >= nextRoomTrigger) {
//			ActivateNextRoom();
			BuildRoom();
			CullRoom();
		}
	}

	void ActivateNextRoom () {
		Debug.Log ("Activating next room at " + nextRoomTrigger);
		GameObject nextRoomObj = roomObjs[0];
		roomObjs.Remove(nextRoomObj);
		nextRoomObj.SetActive(true);
		nextRoomTrigger += nextRoomObj.transform.position.z;
		ExtendFog(nextRoomTrigger + roomTemplate.bounds.extents.z);
	}

	void BuildRoom () {
		roomGenerator = new RoomGenerator();
		currentRoom = roomGenerator.Generate(roomTemplate);
		PlaceRoomTemplate();
		PlaceTiles();
		currentRoomObj.GetComponent<RoomController>().Activate(currentRoom);
		nextBuildOffset += roomTemplate.bounds.size.z;
		nextRoomTrigger += roomTemplate.bounds.size.z;
		roomObjs.Add(currentRoomObj);
	}

	void CullRoom () {
		GameObject firstRoom = roomObjs[0];
		if (firstRoom.transform.position.z < (rail.transform.position.z - 1.5*roomTemplate.bounds.size.z)) {
			roomObjs.Remove(firstRoom);
			Destroy(firstRoom);
		}
	}

	void PlaceRoomTemplate () {
		currentRoomObj = (GameObject)Instantiate(roomPrefab);
		currentRoomObj.transform.position = new Vector3(0f, 0f, nextBuildOffset);
		wallContainer = currentRoomObj.transform.FindChild("Walls").gameObject;
		spawnContainer = currentRoomObj.transform.FindChild("EnemySpawns").gameObject;
		waypointContainer = currentRoomObj.transform.FindChild("Waypoints").gameObject;
	}


	void PlaceTiles () {
		Room.TileType type;
		foreach (KeyValuePair<Vector3, Room.TileType> kv in currentRoom.tiles) {
			var pos = kv.Key;
			pos.z += nextBuildOffset;
			if (kv.Value == Room.TileType.Wall) {
				PlaceWall(pos);
			}

			if (kv.Value == Room.TileType.Enemy) {
				PlaceEnemySpawner(pos);
//				PlaceWaypoint(pos);
			}

			if (kv.Value == Room.TileType.Gold) {
				PlaceGold(pos);
//				PlaceWaypoint(pos);
			}

			if (kv.Value == Room.TileType.Walkable) {
//				PlaceWaypoint(pos);
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
//		GameObject gold = ObjectPool.GetGold();
		gold.transform.SetParent(waypointContainer.transform);
	}

	void StartFog (Room room) {
		leftFog.transform.position = new Vector3(-room.bounds.extents.x-0.5f, fogHeight, 0f);
		rightFog.transform.position = new Vector3(room.bounds.extents.x+0.5f, fogHeight, 0f);
//		topFog.transform.position = new Vector3(0f, fogHeight, room.bounds.extents.z + 0.5f);
		bottomFog.transform.position = new Vector3(0f, fogHeight, -room.bounds.extents.z-0.5f);
	}
	
	void ExtendFog (float z) {
//		topFog.transform.position = new Vector3(0f, fogHeight, z);
	}
}
