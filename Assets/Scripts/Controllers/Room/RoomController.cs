using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoomController : GameController {

	public Room room;

	public GameObject enemySpawnContainer;
	public GameObject waypointsContainer;
	public GameObject waypointPrefab;

	// Use this for initialization
	void Start () {
		room.go = gameObject;
		room.waypoints = PathingService.GetRoomWaypoints(room);
		SpawnWaypoints();
		ActivateSpawners();
	}

	// Update is called once per frame
	void Update () {
	
	}

	void SpawnWaypoints () {
		foreach (Vector3 waypoint in room.waypoints) {
			var wp = (GameObject)Instantiate(waypointPrefab, waypoint, Quaternion.identity);
			wp.transform.SetParent(waypointsContainer.transform);
		}
	}

	void ActivateSpawners () {
		foreach (Transform enemySpawnPoint in enemySpawnContainer.transform) {
			var controller = enemySpawnPoint.GetComponent<EnemySpawnPointController>();
			controller.Activate(room);
		}
	}

}
