using UnityEngine;
using System.Collections;

public class ScrollController : GameController {

	Player player;
	public float scrollSpeedZ = 3f;
	public float doorIncreaseZ = 0.5f;
	public Vector3 scrollSpeed;
	public Vector3 scrollAccel;
	public Vector3 doorAccel;
	public Vector3 maxSpeed;

	void Start () {
		player = GetPlayer();
//		player.ResetSaveData();
	}
	
	// Update is called once per frame
	void Update () {
	}

	void LateUpdate () {
		if (Paused) {
			return;
		}

		MoveFrame();
	}

	void MoveFrame () {
		float frameMoveZ = scrollSpeedZ * Time.deltaTime;
		transform.position += new Vector3(0f, 0f, frameMoveZ);
		player.Distance += frameMoveZ;
		player.runStats.AddDistance(frameMoveZ);
	}

	public void ResetOnPlayer () {
		iTween.MoveTo (gameObject, iTween.Hash("position", player.CurrentPosition, "time", 3f));
	}

	public void OnDoor () {
		scrollSpeedZ += doorIncreaseZ;
	}


}
