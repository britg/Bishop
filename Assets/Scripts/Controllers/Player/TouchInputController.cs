using UnityEngine;
using System.Collections;

public class TouchInputController : GameController {

	enum SwipeDirection {
		None,
		Up,
		Down,
		Right,
		Left
	}
	SwipeDirection swipeDirection = SwipeDirection.None;

	Player player;

	Vector3 frameSwipeDistance = Vector3.zero;
	Vector3 currentSwipeDistance = Vector3.zero;
	public float requiredSwipeDistance = 100f;

	Vector3 lastMousePosition;

	int touchCount {
		get {
			if (Input.GetMouseButton(0)) {
				return 1;
			}
			return Input.touchCount;
		}
	}

	// Use this for initialization
	void Start () {
		player = GetPlayer();
		requiredSwipeDistance = Screen.height / 2000f * requiredSwipeDistance;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0)) {
			lastMousePosition = Input.mousePosition;
		}
		AccumulateSwipeDuration();
		HandleDirectionChange();
	}

	void HandleDirectionChange () {
		TouchDirection();
		if (InputUp()) {
			player.QueueNextDirection(Player.Direction.Up);
			return;
		}
		if (InputDown()) {
			player.QueueNextDirection(Player.Direction.Down);
			return;
		}
		if (InputLeft()) {
			player.QueueNextDirection(Player.Direction.Left);
			return;
		}
		if (InputRight()) {
			player.QueueNextDirection(Player.Direction.Right);
			return;
		}
	}
	
	Vector3 TouchDirection () {
		Vector3 direction = Vector3.zero;
		if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Moved) {
			direction = Input.touches[0].deltaPosition;
		} else if (Input.GetMouseButton(0)) {
			direction = Input.mousePosition - lastMousePosition;
			lastMousePosition = Input.mousePosition;
		}
		return direction;
	}
	
	void AccumulateSwipeDuration () {

		frameSwipeDistance = TouchDirection();
		
		if (frameSwipeDistance.Equals(Vector3.zero)) {
			currentSwipeDistance = Vector3.zero;
			swipeDirection = SwipeDirection.None;
			return;
		}
		
		currentSwipeDistance += frameSwipeDistance;

		if (Mathf.Abs(currentSwipeDistance.y) > Mathf.Abs(currentSwipeDistance.x)) {

			if (currentSwipeDistance.y >= requiredSwipeDistance) {
				swipeDirection = SwipeDirection.Up;
				return;
			}

			if (currentSwipeDistance.y <= -requiredSwipeDistance) {
				swipeDirection = SwipeDirection.Down;
				return;
			}
		} else {

			if (currentSwipeDistance.x >= requiredSwipeDistance) {
				swipeDirection = SwipeDirection.Right;
				return;
			}

			if (currentSwipeDistance.x <= -requiredSwipeDistance) {
				swipeDirection = SwipeDirection.Left;
				return;
			}
		}
	}
	
	bool InputUp () {
		return swipeDirection == SwipeDirection.Up;
	}
	
	bool InputDown () {
		return swipeDirection == SwipeDirection.Down;
	}
	
	bool InputRight () {
		return swipeDirection == SwipeDirection.Right;
	}
	
	bool InputLeft () {
		return swipeDirection == SwipeDirection.Left;
	}


}
