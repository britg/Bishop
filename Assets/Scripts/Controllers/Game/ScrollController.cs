using UnityEngine;
using System.Collections;

public class ScrollController : GameController {

	public Vector3 scrollSpeed;
	public Vector3 scrollAccel;
	public Vector3 maxSpeed;

	void Start () {
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

		if (scrollSpeed.z < maxSpeed.z) {
			scrollSpeed += scrollAccel * Time.deltaTime;
		}
	}



}
