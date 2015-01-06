using UnityEngine;
using System.Collections;

public class FollowPlayerController : GameController {

	Player player;

	public float followFactor;

	// Use this for initialization
	void Start () {
		player = GetPlayer();
	}

	void StartAtPlayer () {
		transform.position = player.CurrentPosition;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void LateUpdate () {
		var newPos = transform.position;
		newPos.x = player.CurrentPosition.x * followFactor;
		transform.position = newPos;
	}
}
