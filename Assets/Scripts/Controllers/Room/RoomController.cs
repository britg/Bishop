using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoomController : GameController {

	public Room room;

	GameObject goldPrefab;
	GameObject enemyPrefab;
	GameObject swordPrefab;
	GameObject keyPrefab;
	GameObject doorPrefab;

	List<Vector3> freeSpots;
	List<GameObject> goldFromPool;

	Vector3 RoomPosition {
		get {
			return transform.position;
		}
	}

	// Use this for initialization
	void Start () {
		goldPrefab = ItemReferences.goldPrefab;
		enemyPrefab = ItemReferences.enemyPrefab;
		swordPrefab = ItemReferences.swordPrefab;
		keyPrefab = ItemReferences.keyPrefab;
		doorPrefab = ItemReferences.doorPrefab;

		Invoke ("Initialize", 1f);
	}

	void Initialize () {
		freeSpots = new List<Vector3>();
		Scan();

		if (room.type == Room.Type.Unlock) {
			PlaceKey(RandomSpot());
			PlaceDoor();
		}

		PlaceSwords(room.swordCount);
//		FillGold();
//		PlaceEnemies();
		ReplaceEnemyPlaceholders();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void Scan () {
		for (var x = room.FillableBounds.min.x; x <= room.FillableBounds.max.x; x++) {
			for (var z = room.FillableBounds.min.z; z <= room.FillableBounds.max.z; z++) {
				var spot = new Vector3(x, 0f, z) + RoomPosition;
				if (SpotFree(spot)) {
					freeSpots.Add(spot);
				}
			}
		}
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

	Vector3 RandomSpot () {
		var r = Random.Range (0, freeSpots.Count - 1);
		return freeSpots[r];
	}

	Vector3 RandomSpot (int minZ) {
		var r = RandomSpot();
		if (r.z < minZ) {
			return RandomSpot(minZ);
		}

		return r;
	}

	void PlaceEnemies () {
		for (int i = 0; i < room.enemyCount; i++) {
			PlaceEnemy(RandomSpot((int)(RoomPosition.z - room.bounds.extents.z)));
		}
	}

	void FillGold() {
		goldFromPool = ObjectPool.GetGoldAmount(freeSpots.Count);

		bool inStream = false;
		int streamCount = 0;
		Vector3 pos;
		GameObject gold;

		for(int i = 0; i < freeSpots.Count; i++) {
			pos = freeSpots[i];

			if (!inStream) {
				var r = Random.Range(0, 100);
				if (r <= room.streamChance) {
					inStream = true;
					streamCount = 0;
				}
			}

			if (inStream) {
				gold = goldFromPool[i];
				ActivateGold(pos, gold);
				streamCount++;
			}

			if (streamCount > room.streamLength) {
				inStream = false;
			}
		}
	}

	void PlaceKey (Vector3 pos) {
		var key = (GameObject)Instantiate(keyPrefab, pos, keyPrefab.transform.rotation);
		key.name = "Key";
		key.transform.SetParent(transform);
		freeSpots.Remove(pos);
	}

	void PlaceDoor () {
		var pos = new Vector3(-1f, 0f, 15.5f) + RoomPosition;
		var door = (GameObject)Instantiate(doorPrefab, pos, doorPrefab.transform.rotation);
		door.transform.SetParent(transform);
	}

	void PlaceGold (Vector3 pos) {
		var gold = (GameObject)Instantiate(goldPrefab, pos, goldPrefab.transform.rotation);
		gold.name = "Gold";
		gold.transform.SetParent(transform);
	}

	void ActivateGold (Vector3 pos, GameObject goldObj) {
		goldObj.transform.position = pos;
//		goldObj.transform.SetParent(transform);
		goldObj.SetActive(true);
	}

	void PlaceEnemy (Vector3 pos) {
		var enemy = (GameObject)Instantiate(enemyPrefab, pos, Quaternion.identity);
		enemy.transform.SetParent(transform);
	}

	void PlaceSwords (int count) {
		for (var i = 0; i < count; i++) {
			PlaceSword(RandomSpot());
		}
	}

	void PlaceSword (Vector3 pos) {
		var sword = (GameObject)Instantiate(swordPrefab, pos, swordPrefab.transform.rotation);
		sword.name = "Sword";
		sword.transform.SetParent(transform);
		freeSpots.Remove(pos);
	}

	void OnDestroy () {
		if (ObjectPool == null) {
			return;
		}
		ObjectPool.ReturnGold(goldFromPool);
	}

	void ReplaceEnemyPlaceholders () {
		foreach (Transform child in transform) {
			if (child.name == "EnemyPlaceholder") {
				ReplaceEnemyPlaceholder(child.gameObject);
			}
		}
	}

	void ReplaceEnemyPlaceholder (GameObject placeholder) {
		var position = placeholder.transform.position;
		var rotation = placeholder.transform.rotation;
		GameObject enemyObj = (GameObject)Instantiate(enemyPrefab, position, rotation);
		EnemyController enemyController = enemyObj.GetComponent<EnemyController>();
		enemyController.traversableSpots = freeSpots;
		Destroy(placeholder);
	}
}
