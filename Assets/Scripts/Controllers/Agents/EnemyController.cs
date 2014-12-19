using UnityEngine;
using System.Collections;

[RequireComponent(typeof(WanderController))]
public class EnemyController : GameController {

	public Enemy enemy;
	public Enemy.Properties enemyInitialization;

	// Use this for initialization
	void Start () {
		enemy = new Enemy(enemyInitialization);

		SetAgents();
	}

	void SetAgents () {
		var wanderController = gameObject.GetComponent<WanderController>();
		if (wanderController != null) {
			wanderController.Agent = enemy;
		}
	}
	
	// Update is called once per frame
	void Update () {
	}


}
