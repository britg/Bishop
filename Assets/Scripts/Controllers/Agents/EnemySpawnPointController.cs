using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawnPointController : MonoBehaviour {

	Player player;

	public GameObject enemyPrefab;
	public float cooldown;
	public float max;
	public Agent.State initialState;

	float spawnCount;
	Room room;
	GameObject currentEnemy;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Activate (Room _room) {
		room = _room;
//		var rand = Random.Range((int)cooldown, (int)max);
//		InvokeRepeating("AttemptSpawn", 0f, rand);
		SpawnEnemy();
	}

	void SpawnEnemy () {
		var position = transform.position;
		var rotation = transform.rotation;
		currentEnemy = (GameObject)Instantiate(enemyPrefab, position, rotation);
		currentEnemy.transform.parent = transform;
		EnemyController enemyController = currentEnemy.GetComponent<EnemyController>();
		enemyController.Activate(room);
		enemyController.enemy.EnterState(initialState);
	}

	void AttemptSpawn () {
		if (currentEnemy != null) {
			return;
		}

		SpawnEnemy();
	}
}
