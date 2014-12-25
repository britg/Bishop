using UnityEngine;
using System.Collections;

public class EnemyController : GameController {

	public Enemy enemy;
	public Enemy.Properties enemyInitialization;

	// Use this for initialization
	void Start () {
		enemy = new Enemy(enemyInitialization);
		enemy.go = gameObject;
		Invoke ("SetAgents", 2f);
	}

	void SetAgents () {
		if (enemy.Wanders) {
			var wanderController = gameObject.AddComponent<WanderController>();
			wanderController.Agent = enemy;
		}
	}
	
	// Update is called once per frame
	void Update () {
	}


}
