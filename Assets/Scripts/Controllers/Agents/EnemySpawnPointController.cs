using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawnPointController : MonoBehaviour {

	public GameObject enemyPrefab;
	public float cooldown;
	public float max;

	float spawnCount;
	Room room;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Activate (Room _room) {
		room = _room;
		SpawnEnemy();
	}

	void SpawnEnemy () {
		var position = transform.position;
		var rotation = transform.rotation;
		GameObject enemyObj = (GameObject)Instantiate(enemyPrefab, position, rotation);
		EnemyController enemyController = enemyObj.GetComponent<EnemyController>();
		enemyController.Activate(room);
	}
}
