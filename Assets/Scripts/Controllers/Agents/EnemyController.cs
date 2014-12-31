using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyController : GameController {

	public Enemy enemy;
	public Enemy.Properties enemyInitialization;

	public List<Vector3> traversableSpots;

	Player player;

	// Use this for initialization
	void Start () {
		player = GetPlayer();
		enemy = new Enemy(enemyInitialization);
		enemy.go = gameObject;
		enemy.TraversableSpots = traversableSpots;

		GetComponent<WanderController>().Agent = enemy;
		GetComponent<AggroController>().agent = enemy;
	}

	// Update is called once per frame
	void Update () {
		if (enemy.CurrentState != Agent.State.Aggro) {
			DetectPlayer();
		}
	}

	float currentDetectTime = 0f;
	void DetectPlayer () {
		if (currentDetectTime < enemy.DetectTime) {
			ContinueDetect();
		} else {
			enemy.EnterState(Agent.State.Aggro);
		}
	}

	void ContinueDetect () {
		var origin = transform.position;
		var playerPos = player.go.transform.position;
		var direction = playerPos - origin;
		RaycastHit[] hits = Physics.RaycastAll (transform.position, direction, enemy.DetectRadius);

		float playerDist = Mathf.Infinity;
		float wallDist = Mathf.Infinity;
		foreach(var hit in hits) {
			if (GameLayer.isWall(hit)) {
				if (hit.distance < wallDist) {
					wallDist = hit.distance;
				}
			}

			if (GameLayer.isPlayer(hit)) {
				playerDist = hit.distance;
			}
		}

		if (playerDist < wallDist) {
			currentDetectTime += Time.deltaTime;
		} else {
			currentDetectTime = 0f;
		}
	}

}
