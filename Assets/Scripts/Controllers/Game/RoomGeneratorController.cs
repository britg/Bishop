using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoomGeneratorController : GameController {

	Player player;

	RoomGenerator roomGenerator;
	Room currentRoom;
	float nextBuildOffset = 0f;
	int roomCount = 0;
	GameObject currentRoomObj;
	GameObject wallContainer;
	GameObject spawnContainer;
	GameObject waypointContainer;
	float nextRoomTrigger = 0f;
	List<GameObject> roomObjs;

	int seed;
	public Room roomTemplate;

	public float fogHeight = 3f;
	public GameObject roomPrefab;
	public int roomCountToGenerate;
	public GameObject rail;
	public int enemyIncreasePerRoom = 2;
	public float keyRoomChance;

	public GameObject wallPrefab;
	public GameObject enemySpawnerPrefab;
	public GameObject waypointPrefab;
	public GameObject goldPrefab;
	public GameObject gemPrefab;
	public GameObject doorPrefab;
	public GameObject keyPrefab;

	// Use this for initialization
	void Start () {
		player = GetPlayer();
		roomObjs = new List<GameObject>();
		BuildNormal();
	}

	public void BuildNormal () {
		seed = System.Guid.NewGuid().GetHashCode();
		RemoveExistingRooms();
		BuildRooms();
		PlacePlayer(new Vector3(0f, 0f, -currentRoom.bounds.extents.z));
		ShowNormal();
	}

	public void BuildDaily () {
		seed = ES2.Load<int>(DailyApiController.DAILY_SEED);
		RemoveExistingRooms();
		BuildRooms();
		PlacePlayer(new Vector3(0f, 0f, -currentRoom.bounds.extents.z));
		ShowDaily();
	}

	void RemoveExistingRooms () {
		roomObjs = new List<GameObject>();
		foreach (Transform child in transform) {
			Destroy(child.gameObject);
		}

		nextBuildOffset = 0f;
		roomTemplate.enemyCount = 0;
		roomCount = 0;
	}

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
		GameObject nextRoomObj = roomObjs[0];
		roomObjs.Remove(nextRoomObj);
		nextRoomObj.SetActive(true);
		nextRoomTrigger += nextRoomObj.transform.position.z;
	}

	void BuildRooms () {
		for (int i = 0; i < roomCountToGenerate; i++) {
			BuildRoom();
		}
		nextRoomTrigger = roomTemplate.bounds.size.z * (roomCountToGenerate-2);
	}


	void BuildRoom () {
		Debug.Log ("Room seed: " + seed);
		roomCount++;
		roomTemplate.enemyCount += enemyIncreasePerRoom;
		roomTemplate.roomCount = roomCount;
		roomGenerator = new RoomGenerator(seed);

        // Force a door ever 3 rooms
        if (roomCount % 3 == 0) {
            keyRoomChance = 100f;
        } else {
            keyRoomChance = 0f;
        }
		roomGenerator.keyRoomChance = keyRoomChance;
		currentRoom = roomGenerator.Generate(roomTemplate);
		PlaceRoomTemplate(seed);
		PlaceTiles();
		currentRoomObj.GetComponent<RoomController>().Activate(currentRoom);
		nextBuildOffset += roomTemplate.bounds.size.z;
		nextRoomTrigger += roomTemplate.bounds.size.z;
		roomObjs.Add(currentRoomObj);
        //CombineMeshes();
		seed++;
	}

	void CullRoom () {
		GameObject firstRoom = roomObjs[0];
		if (firstRoom.transform.position.z < (rail.transform.position.z - 1.5*roomTemplate.bounds.size.z)) {
			roomObjs.Remove(firstRoom);
			RemoveRoomObject(firstRoom);
		}
	}

	void RemoveRoomObject (GameObject roomObj) {
		Transform walls = roomObj.transform.FindChild("Walls");
		foreach (Transform wall in walls) {
			ObjectPool.ReturnWall(wall.gameObject);
		}
		Destroy(roomObj);
	}

	void PlaceRoomTemplate (int seed) {
		currentRoomObj = (GameObject)Instantiate(roomPrefab);
		currentRoomObj.name = "Room: " + seed;
		currentRoomObj.transform.SetParent(transform);
		currentRoomObj.transform.position = new Vector3(0f, 0f, nextBuildOffset);
		wallContainer = currentRoomObj.transform.FindChild("Walls").gameObject;
		spawnContainer = currentRoomObj.transform.FindChild("EnemySpawns").gameObject;
		waypointContainer = currentRoomObj.transform.FindChild("Waypoints").gameObject;
	}


	void PlaceTiles () {
		Vector3 pos;
		Room.TileType type;
		List<Vector3> wallCenterPoints = new List<Vector3>();
		foreach (KeyValuePair<Vector3, Room.TileType> kv in currentRoom.tiles) {
			pos = kv.Key;
			type = kv.Value;
			pos.z += nextBuildOffset;
			if (type == Room.TileType.Wall) {
				PlaceWall(pos);
				wallCenterPoints.Add(pos);
			}

			if (type == Room.TileType.Enemy) {
				PlaceEnemySpawner(pos);
//				PlaceWaypoint(pos);
			}

			if (type == Room.TileType.Gold) {
				PlaceGold(pos);
			}

			if (type == Room.TileType.Gem) {
				PlaceGem(pos);
			}

			if (type == Room.TileType.Door) {
				PlaceDoor(pos);
			}

			if (type == Room.TileType.Key) {
				PlaceKey(pos);
			}
		}

		MeshGenerator meshGenerator = new MeshGenerator();
		meshGenerator.meshContainer = transform;
		meshGenerator.Generate(wallCenterPoints);
	}

	void PlaceWall (Vector3 pos) {
//		GameObject wall = (GameObject)Instantiate(wallPrefab, pos, Quaternion.identity);
		GameObject wall = ObjectPool.GetWall();
		wall.transform.position = pos;
		wall.transform.parent = wallContainer.transform;
		wall.name = string.Format("{0},{1}", pos.x, pos.z);
		wall.SetActive(true);
	}

	void PlacePlayer (Vector3 pos) {
		//player.go.transform.position = pos;
		//rail.transform.position = pos + new Vector3(0f, 0f, 10f);
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

	void PlaceGem (Vector3 pos) {
		GameObject gem = (GameObject)Instantiate(gemPrefab, pos, Quaternion.identity);
		gem.transform.SetParent(waypointContainer.transform);
	}

	void PlaceDoor (Vector3 pos) {
		GameObject door = (GameObject)Instantiate(doorPrefab, pos, Quaternion.identity);
		door.transform.SetParent(waypointContainer.transform);
	}

	void PlaceKey (Vector3 pos) {
		GameObject key = (GameObject)Instantiate(keyPrefab, pos, Quaternion.identity);
		key.transform.SetParent(waypointContainer.transform);
	}

    void CombineMeshes () {
        MeshFilter[] meshFilters = wallContainer.GetComponentsInChildren<MeshFilter>();
        CombineInstance[] combine = new CombineInstance[meshFilters.Length];
        int i = 0;
        while (i < meshFilters.Length) {
            combine[i].mesh = meshFilters[i].sharedMesh;
            combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
            Destroy(meshFilters[i].gameObject);
            i++;
        }
        var meshFilter = wallContainer.AddComponent<MeshFilter>();
        meshFilter.mesh = new Mesh();
        meshFilter.mesh.CombineMeshes(combine, true);

        wallContainer.AddComponent<MeshCollider>();
        //wallContainer.transform.gameObject.SetActive(true);
    }

}
