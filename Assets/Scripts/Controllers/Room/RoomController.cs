using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoomController : GameController {

	public GameObject enemySpawnContainer;
	public GameObject waypointsContainer;
	public GameObject waypointPrefab;

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
	
	}

	public void Activate (Room room) {
		foreach (Transform enemySpawnPoint in enemySpawnContainer.transform) {
			var controller = enemySpawnPoint.GetComponent<EnemySpawnPointController>();
			controller.Activate(room);
		}
	}

}
