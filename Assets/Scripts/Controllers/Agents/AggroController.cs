using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AggroController : GameController {

	public Agent agent { get; set; }

	Player player;

	Vector3 up;
	Vector3 right;
	Vector3 down;
	Vector3 left;
	List<Vector3> availableDirs;

	// Use this for initialization
	void Start () {
		player = GetPlayer();
		availableDirs = new List<Vector3>();
	}
	
	// Update is called once per frame
	void Update () {

		if (Paused) {
			return;
		}

		if (agent.CurrentStateAggro) {
			Chase();
		}
	}

	void Chase () {

		agent.TraversableSpots.Sort ((a, b) => {
			return Vector3.Distance(a, nextPosition).CompareTo(Vector3.Distance(b, nextPosition));
		});

		closestSpot = agent.TraversableSpots[0];
		if (closestSpot.Equals(transform.position)) {
			closestSpot = agent.TraversableSpots[1];
		}
		direction = closestSpot - transform.position;
		nextPosition = transform.position + Time.deltaTime * agent.CurrentSpeed * direction.normalized;
		transform.position = nextPosition;
	}

}
