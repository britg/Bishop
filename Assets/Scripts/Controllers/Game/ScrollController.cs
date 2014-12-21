using UnityEngine;
using System.Collections;

public class ScrollController : GameController {

	Player player;

	public float resetDistance = 30f;

	public Vector3 scrollSpeed;
	public Vector3 scrollAccel;
	public Vector3 maxSpeed;

	// Use this for initialization
	void Start () {
		player = GetPlayer();
	}
	
	// Update is called once per frame
	void Update () {
		MoveFrame();
		EnsureNextRoom();
	}

	void MoveFrame () {
		Vector3 frameMove = scrollSpeed * Time.deltaTime;
		transform.position += frameMove;
		scrollSpeed += scrollAccel * Time.deltaTime;
	}

	void EnsureNextRoom () {
		if (transform.position.x >= resetDistance) {
			Reset();
		}
	}

	void Reset () {
		Debug.Log("Resetting");
		var pos = transform.position;
		pos.x = 0f;
		transform.position = pos;
	}

}
