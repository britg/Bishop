using UnityEngine;
using System.Collections;

public class ScrollController : GameController {

	bool scrolling = false;

	public Vector3 scrollSpeed;
	public Vector3 scrollAccel;
	public Vector3 maxSpeed;
	public Vector3 scrollStart;
	public float scrollToStartTime;

	public GameObject castleDoor;
	public Vector3 castleDoorOpenPosition;
	public float castleDoorOpenTime;
	public AudioSource castleDoorSound;

	public MusicController musicController;

	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (Paused || !scrolling) {
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

	public void TransitionToCastle () {
		Pause();
		iTween.MoveTo(gameObject, iTween.Hash ("position", scrollStart,
		                                       "time", scrollToStartTime));

		iTween.MoveTo(castleDoor, iTween.Hash ("position", castleDoorOpenPosition,
		                                       "time", castleDoorOpenTime,
		                                       "oncomplete", "TransitionDone",
		                                       "oncompletetarget", gameObject));
		castleDoorSound.Play();
	}

	void TransitionDone () {
		Unpause();
		musicController.shouldPlayMusic = true;
		scrolling = true;
	}

}
