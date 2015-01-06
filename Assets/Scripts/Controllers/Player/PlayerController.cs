using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : GameController {

	public Player player;
	public Player.Properties playerInitialization;
	public float playerAccel;
	public AudioSource footsteps;
	bool canMove = false;
	PathingService pathingService;
	public float turnForgiveness = 0.2f;

	void Awake () {
		player = new Player(playerInitialization, gameObject);
		player.NextDirection = player.CurrentDirection;
		player.waypointsTraversed = new List<Vector3>();
		pathingService = new PathingService(player, turnForgiveness);

		// Proxy for some kind of intro sequence
//		Invoke ("EnableMovement", 1f);
		EnableMovement();
	}

	void Update () {
		if (Paused) {
			if (footsteps.isPlaying) {
				footsteps.Stop();
			}
			return;
		}
		CheckDead();
		LockLane();

		if (ShouldTurn() && CanTurn()) {
			Turn();
		}

		if (player.CurrentDirection == Player.Direction.Stop) {
			footsteps.Stop();
			return;
		} else {
			if (!footsteps.isPlaying) {
				footsteps.Play();
			}
		}

		if (CanMove()) {
			MovePlayer();
			SpeedUp();
		} else {
			AttemptTurnOrStop();
		}

		FaceDirection();
	}

	void MovePlayer () {
		Vector3 frameMove = player.Velocity * Time.deltaTime;
		transform.position += frameMove;
	}

	void AttemptTurnOrStop () {
		if (ShouldTurn()) {
			LockBoth();
			if (CanTurn()) {
				Turn();
				return;
			}
		}

		StopPlayer();
	}

	void StopPlayer () {
		player.CurrentDirection = Player.Direction.Stop;
		player.NextDirection = Player.Direction.Stop;
		LockBoth();
	}

	void SpeedUp () {
		if (player.CurrentSpeed < player.MaxSpeed) {
			player.CurrentSpeed += playerAccel * Time.deltaTime;
		}
	}

	void LockLane () {
		if (player.CurrentDirection == Player.Direction.Stop) {
			LockBoth();
		}

		if (player.CurrentDirection == Player.Direction.Right ||
		    	player.CurrentDirection == Player.Direction.Left) {
			LockZ();
		}

		if (player.CurrentDirection == Player.Direction.Up ||
		    	player.CurrentDirection == Player.Direction.Down) {
			LockX();
		}
	}

	void LockBoth () {
		LockX();
		LockZ();
	}

	void LockX () {
		var pos = transform.position;
		pos.x = Mathf.Round(pos.x * 2f) / 2f;
		transform.position = pos;
	}

	void LockZ () {
		var pos = transform.position;
		pos.z = Mathf.Round(pos.z * 2f) / 2f;
		transform.position = pos;
	}

	bool CanMove() {
		if (!canMove) {
			return false;
		}
		var frameDestination = transform.position + (player.Velocity*Time.deltaTime);
		return pathingService.DestinationValid(frameDestination);
	}

	bool ShouldTurn () {
		return player.NextDirection != player.CurrentDirection;
	}

	bool CanTurn () {
		return pathingService.TurnValid(player.NextDirection);
	}

	void Turn () {
		player.CurrentDirection = player.NextDirection;
	}

	void FaceDirection () {
		if (player.CurrentDirection == Player.Direction.Up) {
			transform.eulerAngles = Vector3.zero;
		}
		if (player.CurrentDirection == Player.Direction.Right) {
			transform.eulerAngles = new Vector3(0f, 90f, 0f);
		}
		if (player.CurrentDirection == Player.Direction.Down) {
			transform.eulerAngles = new Vector3(0f, 180f, 0f);
		}
		if (player.CurrentDirection == Player.Direction.Left) {
			transform.eulerAngles = new Vector3(0f, 270f, 0f);
		}
	}

	void EnableMovement () {
		canMove = true;
	}

	void DisableMovement () {
		canMove = false;
	}

	void CheckDead () {
		if (player.Dead) {
			player.Save();
			DisableMovement();
		}
	}

}
