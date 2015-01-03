using UnityEngine;
using System.Collections;

public class FollowPlayerController : GameController {

	Player player;

	// Use this for initialization
	void Start () {
		player = GetPlayer();
		transform.position = player.CurrentPosition;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void LateUpdate () {
		transform.position = player.CurrentPosition;
	}
}
