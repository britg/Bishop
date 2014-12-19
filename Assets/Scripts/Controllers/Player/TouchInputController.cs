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

	// Use this for initialization
	void Start () {
		player = GetPlayer();
	}
	
	// Update is called once per frame
	void Update () {
		AccumulateSwipeDuration();
		HandleDirectionChange();
	}

	void HandleDirectionChange () {
		TouchDirection();
		if (InputUp()) {
			player.NextDirection = Player.Direction.Up;
			return;
		}
		if (InputDown()) {
			player.NextDirection = Player.Direction.Down;
			return;
		}
		if (InputLeft()) {
			player.NextDirection = Player.Direction.Left;
			return;
		}
		if (InputRight()) {
			player.NextDirection = Player.Direction.Right;
			return;
		}
	}
	
	Vector3 TouchDirection () {
		Vector3 direction = Vector3.zero;
		if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Moved) {
			direction = Input.touches[0].deltaPosition;
		}
		return direction;
	}
	
	void AccumulateSwipeDuration () {
		
		if (Input.touchCount < 1) {
			currentSwipeDistance = Vector3.zero;
			swipeDirection = SwipeDirection.None;
			return;
		}
		
		frameSwipeDistance = TouchDirection();
		currentSwipeDistance += frameSwipeDistance;
		
		if (currentSwipeDistance.y >= requiredSwipeDistance) {
			swipeDirection = SwipeDirection.Up;
			return;
		}
		
		if (currentSwipeDistance.x >= requiredSwipeDistance) {
			swipeDirection = SwipeDirection.Right;
			return;
		}
		
		if (currentSwipeDistance.y <= -requiredSwipeDistance) {
			swipeDirection = SwipeDirection.Down;
			return;
		}
		
		if (currentSwipeDistance.x <= -requiredSwipeDistance) {
			swipeDirection = SwipeDirection.Left;
			return;
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
