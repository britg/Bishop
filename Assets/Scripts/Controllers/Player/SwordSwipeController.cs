using UnityEngine;
using System.Collections;

public class SwordSwipeController : GameController {

	Player player;

	public float swipeDuration = 0.4f;
	public float swipeAnimation = 0.1f;
	public GameObject sword;
	int swordPos = 0;
	bool isAnimating = false;
	float currentSwipeTime = 0f;

	Vector3 pos0;
	Vector3 pos1 = new Vector3 (90f, -60f, 27f);


	bool swipe {
		get {
			if (Paused) {
				return false;
			}
			if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonUp(0)) {
				return true;
			}

			if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Space)) {
				return true;
			}

			if (Input.touchCount > 0 && (Input.touches[0].phase == TouchPhase.Began || Input.touches[0].phase == TouchPhase.Began)) {
				return true;
			}
			return false;
		}
	}

	// Use this for initialization
	void Start () {
		player = GetPlayer();
		pos0 = sword.transform.eulerAngles;
	}
	
	// Update is called once per frame
	void Update () {
		if (swipe && !isAnimating) {
			Swipe();
		}

		if (player.isAttacking) {
			UpdateSwipeTime();
		}
	}

	void Swipe () {
		currentSwipeTime = 0f;
		player.isAttacking = true;
		isAnimating = true;
		Vector3 pos;
		if (swordPos == 0) {
			swordPos = 1;
			pos = pos1;
		} else {
			swordPos = 0;
			pos = pos0;
		}

		iTween.RotateTo(sword, iTween.Hash (
				"time", swipeAnimation,
				"islocal", true,
				"rotation", pos,
				"oncomplete", "OnSwipeComplete",
				"oncompletetarget", gameObject
			));
	}

	void OnSwipeComplete () {
		isAnimating = false;
//		player.isSwiping = false;
	}

	void UpdateSwipeTime () {
		currentSwipeTime += Time.deltaTime;
		if (currentSwipeTime >= swipeDuration) {
			player.isAttacking = false;
			currentSwipeTime = 0f;
		}
	}
}
