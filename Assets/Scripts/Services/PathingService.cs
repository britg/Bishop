using UnityEngine;
using System.Collections;

public class PathingService {

	Agent agent;
	float halfLaneWidth = 1f;
	float sphereRadius = 0.005f;

	public PathingService (Agent _agent) {
		agent = _agent;
	}

	public bool DestinationValid (Vector3 destination) {
		return PathValid(destination, agent.CurrentDirection);
	}

	public bool PathValid (Vector3 start, Agent.Direction direction) {
//		var hits = Physics.RaycastAll(start, agent.Vector(direction));
		var hits = Physics.SphereCastAll(start, sphereRadius, agent.Vector (direction));
		foreach (RaycastHit hit in hits) {
			if (hit.distance <= halfLaneWidth && GameLayer.isWall(hit)) {
				return false;
			}
		}

		return true;
	}

	public bool TurnValid (Agent.Direction next) {
		Vector3 left = Vector3.zero;
		Vector3 right = Vector3.zero;
		Vector3 offset = Vector3.zero;
		float testLimit = halfLaneWidth - 0.08f;

		if (next == Agent.Direction.Right) {
			offset = new Vector3(0f, 0f, testLimit);
		}

		if (next == Agent.Direction.Up) {
			offset = new Vector3(-testLimit, 0f, 0f);
		}

		if (next == Agent.Direction.Left) {
			offset = new Vector3(0f, 0f, -testLimit);
		}

		if (next == Agent.Direction.Down) {
			offset = new Vector3(testLimit, 0f, 0f);
		}

		left = agent.CurrentPosition + offset;
		right = agent.CurrentPosition - offset;

		return (PathValid(agent.CurrentPosition, next) && PathValid(left, next) && PathValid(right, next));
	}

	public bool CanTurn () {
		var turnPoint = agent.CurrentPosition + agent.NextVelocity;
		return DestinationValid(turnPoint);
	}

}
