using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyController : GameController {

	public Enemy enemy;
	public Enemy.Properties enemyInitialization;

	Player player;
	Room room;

	// Use this for initialization
	void Start () {
		player = GetPlayer();
	}

	// Update is called once per frame
	void Update () {

	}

	public void Activate (Room _room) {
		room = _room;
		enemy = new Enemy(enemyInitialization);
		enemy.go = gameObject;
		enemy.room = room;
		enemy.lastWaypoint = transform.position;

		GetComponent<WanderController>().agent = enemy;
		GetComponent<AggroController>().agent = enemy;
	}

}
