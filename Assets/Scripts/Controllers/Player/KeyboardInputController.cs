﻿using UnityEngine;
using System.Collections;

public class KeyboardInputController : GameController {

	Player player;

	void Start () {
		player = GetPlayer();
	}

	void Update () {
		HandleDirectionChange();
	}

	void HandleDirectionChange () {
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

	bool InputUp () {
		return Input.GetKeyDown(KeyCode.W);
	}
	
	bool InputDown () {
		return Input.GetKeyDown(KeyCode.S);
	}
	
	bool InputRight () {
		return Input.GetKeyDown(KeyCode.D);
	}
	
	bool InputLeft () {
		return Input.GetKeyDown(KeyCode.A);
	}

}
