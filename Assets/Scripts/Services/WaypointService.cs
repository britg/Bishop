using UnityEngine;
using System.Collections;

public class WaypointService {

	Player player;

	public WaypointService (Player _player) {
		player = _player;
	}

	public void OnEnterWaypoint (Vector3 waypoint) {
		if (waypoint == player.lastWaypointTraversed) {
			return;
		}

		if (player.waypointsTraversed.Count < 2) {
			AddWaypoint(waypoint);
			return;
		}

		var secondToLast = player.waypointsTraversed[player.waypointsTraversed.Count - 2];
		if (waypoint == secondToLast) {
			player.waypointsTraversed.RemoveAt(player.waypointsTraversed.Count-1);
			return;
		}

		AddWaypoint(waypoint);
	}

	void AddWaypoint (Vector3 waypoint) {
		player.waypointsTraversed.Add(waypoint);
	}
}
