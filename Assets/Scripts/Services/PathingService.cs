using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PathingService {

	Agent agent;
	float halfLaneWidth = 1f;
	float sphereRadius = 0.005f;
	float turnForgiveness = 0.2f;

	public PathingService (Agent _agent, float _turnForgiveness) {
		agent = _agent;
		turnForgiveness = _turnForgiveness;
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
		float testLimit = halfLaneWidth - turnForgiveness;

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

	public static bool atWaypoint (Agent agent) {
		return false;
	}

	public static List<Vector3> GetRoomWaypoints (Room room) {
		List<Vector3> waypoints = new List<Vector3>();
		for (var x = room.bounds.min.x; x <= room.bounds.max.x; x+= 1f) {
			for (var z = room.bounds.min.z; z <= room.bounds.max.z; z += 1f) {
				var spot = new Vector3(x, 0f, z) + room.CurrentPosition;
				if (isWaypoint(spot)) {
					waypoints.Add(spot);
				}
			}
		}
		return waypoints;
	}
	
	public static bool isWaypoint (Vector3 point) {
		var castFrom = point;
		castFrom.y = 2f;
		var hits = Physics.SphereCastAll(castFrom, 0.1f, Vector3.down, 2f);
		foreach (var hit in hits) {
			if (GameLayer.isWall(hit)) {
				return false;
			}
		}

		return true;
	}

}
