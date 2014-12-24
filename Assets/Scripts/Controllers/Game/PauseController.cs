using UnityEngine;
using System.Collections;

public class PauseController : GameController {

	bool paused = false;

	public bool Paused {
		get {
			return paused;
		}
	}

	public void Pause () {
		paused = true;
	}

	public void Unpause () {
		paused = false;
	}

	public void TogglePause () {
		if (Paused) {
			Unpause();
		} else {
			Pause();
		}
	}
}

