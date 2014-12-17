using UnityEngine;
using System.Collections;

public class WanderController : GameController {

	public Agent Agent { get; set; }

	Agent.Direction nextDirection = Agent.Direction.Stop;
	bool nextWaypointSet = false;
	Vector3 nextWaypoint = Vector3.zero;
	float currentMoveDistance = 0f;
	float waypointDistance = 1f;
	float consideredEqual = 0.001f;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (Agent.CurrentStateWandering) {
			Wander();
		}
	}

	void Wander () {
		transform.position += Agent.Velocity * Time.deltaTime;

		Vector3 frameMove = Agent.Velocity * Time.deltaTime;
		float frameMagnitude = Agent.CurrentSpeed * Time.deltaTime;
		float nextMoveDistance = currentMoveDistance + frameMagnitude;
		if (nextMoveDistance > waypointDistance) {
			currentMoveDistance = waypointDistance;
			transform.position = nextWaypoint;
		} else {
			currentMoveDistance += frameMagnitude;
			transform.position += frameMove;
		}

		ChooseNextWaypoint();
	}

	void ChooseNextWaypoint () {
		if (!nextWaypointSet) {
			nextWaypoint = transform.position + Agent.DirectionVector;
			nextWaypointSet = true;
		}
		
		if (AtWaypoint()) {
			currentMoveDistance = 0f;
//			RandomizeNextDirection();
			Agent.CurrentDirection = nextDirection;
			nextWaypoint = transform.position + Agent.DirectionVector;
			
			if (!WaypointValid(nextWaypoint)) {
				Agent.CurrentDirection = Agent.Direction.Stop;
			}
		}
	}

	void RandomizeNextDirection () {
		var values = System.Enum.GetValues(typeof(Agent.Direction));
		nextDirection = (Agent.Direction)values.GetValue(Random.Range(0, values.Length-1));
	}
	
	bool AtWaypoint () {
		return Agent.CurrentDirection == Agent.Direction.Stop ||
			(Vector3.SqrMagnitude(transform.position - nextWaypoint) < consideredEqual) ||
				currentMoveDistance >= waypointDistance;
	}
	
	bool WaypointValid (Vector3 waypoint) {
		float testDistance = 1.1f;
		float radius = 0.4f;
		var hits = Physics.SphereCastAll(transform.position, radius, waypoint - transform.position, testDistance);
		foreach (RaycastHit hit in hits) {
			if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Walls")) {
				return false;
			}
		}
		return true;
	}
}
