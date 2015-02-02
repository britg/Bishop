using UnityEngine;
using System.Collections;

public class ScrollController : GameController {

	Player player;
	public Vector3 scrollSpeed;
	public Vector3 scrollAccel;
	public Vector3 maxSpeed;

	void Start () {
		player = GetPlayer();
//		player.ResetSaveData();
	}
	
	// Update is called once per frame
	void Update () {
		if (Paused) {
			return;
		}

		MoveFrame();
	}

	void MoveFrame () {
		Vector3 frameMove = scrollSpeed * Time.deltaTime;
		transform.position += frameMove;
		player.Distance += frameMove.z;
		player.runStats.AddDistance(frameMove.z);

		if (scrollSpeed.z < maxSpeed.z) {
			scrollSpeed += scrollAccel * Time.deltaTime;
		}
	}

	public void ResetOnPlayer () {
		iTween.MoveTo (gameObject, iTween.Hash("position", player.CurrentPosition, "time", 3f));
	}


}
