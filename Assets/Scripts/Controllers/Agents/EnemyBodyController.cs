using UnityEngine;
using System.Collections;

public class EnemyBodyController : GameController {

	Enemy enemy;
	Player player;

	// Use this for initialization
	void Start () {
		var enemyController = transform.parent.gameObject.GetComponent<EnemyController>();
		enemy = enemyController.enemy;
		player = GetPlayer();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter (Collider collider) {
		DetectWaypoint(collider.gameObject);
	}


	Vector3 incrementWaypoint;
	void DetectWaypoint (GameObject go) {
		if (go.tag == "Waypoint" && enemy != null) {
			var thisWaypoint = go.transform.position;
			if (player != null && enemy.CurrentStateAggro) {
				Vector3 targetWaypoint;
				if (player.waypointsTraversed.Count < enemy.currentChaseIndex + 1) {
					targetWaypoint = player.waypointsTraversed[player.waypointsTraversed.Count-1];
				} else {
				 	targetWaypoint = player.waypointsTraversed[enemy.currentChaseIndex];
				}

				if (thisWaypoint == targetWaypoint) {
					incrementWaypoint = thisWaypoint;
					Invoke ("IncrementCurrentChaseIndex", 0.5f / enemy.AggroSpeed);
//					IncrementCurrentChaseIndex();
				}
			}
		}
	}

	void IncrementCurrentChaseIndex () {
		enemy.lastWaypoint = incrementWaypoint;
		enemy.currentChaseIndex++;
	}


}
