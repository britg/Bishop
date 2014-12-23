using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoomController : GameController {

	public Room room;

	GameObject goldPrefab;
	GameObject enemyPrefab;
	List<Vector3> freeSpots;
	int enemiesSpawned = 0;

	// Use this for initialization
	void Start () {
		goldPrefab = ItemReferences.goldPrefab;
		enemyPrefab = ItemReferences.enemyPrefab;
		freeSpots = new List<Vector3>();
		Fill();
		SpawnEnemies();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void Fill () {
		var fillableX = room.dimensions.x / 2f - 0.5f;
		var startZ = -1.5f;
		var endZ = room.dimensions.z + startZ - 2f;
		var roomPos = transform.position;

		for (var x = -fillableX; x <= fillableX; x++) {
			for (var z = startZ; z <= endZ; z++) {
				var spot = new Vector3(x, 0f, z) + roomPos;
				FillSpot(spot);
			}
		}
	}

	void FillSpot (Vector3 spot) {
		if (SpotFree(spot)) {
			Instantiate(goldPrefab, spot, goldPrefab.transform.rotation);
			freeSpots.Add(spot);
		}
	}

	void SpawnEnemies () {
		for (int i = 0; i < room.enemyCount; i++) {
			var r = Random.Range(0, freeSpots.Count-1);
			var pos = freeSpots[r];
			SpawnEnemy(pos);
		}
	}

	void SpawnEnemy (Vector3 pos) {
		Instantiate(enemyPrefab, pos, Quaternion.identity);
	}

	bool SpotFree (Vector3 spot) {
		var castFrom = spot;
		castFrom.y = 2f;
		var hits = Physics.SphereCastAll(castFrom, 0.1f, Vector3.down, 2f);
		foreach (var hit in hits) {
			if (GameLayer.isWall(hit)) {
				return false;
			}
		}

		return true;
	}

}
