using UnityEngine;
using System.Collections;

[RequireComponent(typeof(WanderController))]
public class EnemyController : GameController {

	public Enemy enemy;
	public Enemy.Properties enemyInitialization;

	Agent.Direction nextDirection;
	bool nextWaypointSet = false;
	Vector3 nextWaypoint = Vector3.zero;
	float currentMoveDistance = 0f;
	float waypointDistance = 1f;

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
