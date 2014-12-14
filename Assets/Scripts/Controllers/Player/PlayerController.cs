using UnityEngine;
using System.Collections;

public class PlayerController : GameController {

	float consideredEqual = 0.001f;

	public Player player;
	public Player.Properties playerInitialization;

	Player.Direction nextDirection;
	bool nextWaypointSet = false;
	Vector3 nextWaypoint = Vector3.zero;
	float currentMoveDistance = 0f;
	float waypointDistance = 1f;

	bool canMove = false;

	Vector3 frameSwipeDistance = Vector3.zero;
	Vector3 currentSwipeDistance = Vector3.zero;
	float requiredSwipeDistance = 100f;
	enum SwipeDirection {
		None,
		Up,
		Down,
		Right,
		Left
	}
	SwipeDirection swipeDirection = SwipeDirection.None;

	void Awake () {
		player = new Player(playerInitialization);
		nextDirection = player.CurrentDirection;
		Invoke ("EnableMovement", 1f);
	}

	void Update () {
		CheckDead();
		AccumulateSwipeDuration();
		HandleDirectionChange();
		if (canMove) {
			MovePlayer();
		}
	}

	void MovePlayer () {
		Vector3 frameMove = player.Velocity * Time.deltaTime;
		float frameMagnitude = player.Speed * Time.deltaTime;
		float nextMoveDistance = currentMoveDistance + frameMagnitude;
		if (nextMoveDistance > waypointDistance) {
			float remaining = waypointDistance - currentMoveDistance;
			float pct = remaining / waypointDistance;
			currentMoveDistance = waypointDistance;
			transform.position = nextWaypoint;
		} else {
			currentMoveDistance += frameMagnitude;
			transform.position += frameMove;
		}
//		currentMoveDistance += player.Speed * Time.deltaTime;

		ChooseNextWaypoint();
	}

	void EnableMovement () {
		canMove = true;
	}

	void DisableMovement () {
		canMove = false;
	}

	void HandleDirectionChange () {
		TouchDirection();
		if (HandleUp()) {
			nextDirection = Player.Direction.Up;
			Debug.Log ("Up");
			return;
		}
		if (HandleDown()) {
			nextDirection = Player.Direction.Down;
			Debug.Log ("Down");
			return;
		}
		if (HandleLeft()) {
			nextDirection = Player.Direction.Left;
			Debug.Log ("Left");
			return;
		}
		if (HandleRight()) {
			nextDirection = Player.Direction.Right;
			Debug.Log ("Right");
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

	bool HandleUp () {
		if (Input.GetKeyDown(KeyCode.W)) {
			return true;
		}

		if (swipeDirection == SwipeDirection.Up) {
			return true;
		}

		return false;
	}

	bool HandleDown () {
		if (Input.GetKeyDown(KeyCode.S)) {
			return true;
		}

		if (swipeDirection == SwipeDirection.Down) {
			return true;
		}
		
		return false;
	}

	bool HandleRight () {
		if (Input.GetKeyDown(KeyCode.D)) {
			return true;
		}

		if (swipeDirection == SwipeDirection.Right) {
			return true;
		}
		
		return false;
	}

	bool HandleLeft () {
		if (Input.GetKeyDown(KeyCode.A)) {
			return true;
		}

		if (swipeDirection == SwipeDirection.Left) {
			return true;
		}
		
		return false;
	}

	void ChooseNextWaypoint () {
		if (!nextWaypointSet) {
			nextWaypoint = transform.position + player.DirectionVector;
			nextWaypointSet = true;
		}

		if (AtWaypoint()) {
//			if (player.CurrentDirection != Player.Direction.Stop) {
//				Debug.Log ("Current move distance " + currentMoveDistance);
//				Debug.Log ("Transform " + transform.position);
//				nextDirection = Player.Direction.Stop;
//			}
			currentMoveDistance = 0f;
			player.CurrentDirection = nextDirection;
			nextWaypoint = transform.position + player.DirectionVector;

			if (!WaypointValid(nextWaypoint)) {
				player.CurrentDirection = Player.Direction.Stop;
			}
		}
	}

	bool AtWaypoint () {
		return player.CurrentDirection == Player.Direction.Stop ||
			(Vector3.SqrMagnitude(transform.position - nextWaypoint) < consideredEqual) ||
				currentMoveDistance >= waypointDistance;
	}

	bool WaypointValid (Vector3 waypoint) {
		float testDistance = 1.1f;
		float radius = 0.4f;
		var hits = Physics.SphereCastAll(transform.position, radius, waypoint - transform.position, testDistance);
		foreach (RaycastHit hit in hits) {
			if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Walls")) {
				return false;
			}
		}
		return true;
	}

	void CheckDead () {
		if (player.Dead) {
			DisableMovement();
		}
	}

}
