using UnityEngine;
using System.Collections;

public class PauseController : GameController {

	public bool paused = false;
	public GameObject pauseButton;
	public GameObject startScreen;

	void Start () {
		pauseButton.SetActive(false);
		startScreen.SetActive(true);
	}

	public override bool Paused {
		get {
			return paused;
		}
	}

	public override void Pause () {
		paused = true;
	}

	public override void Unpause () {
		paused = false;
	}

	public void TogglePause () {
		if (Paused) {
			Unpause();
		} else {
			Pause();
		}
	}

	public void StartGame () {
		Unpause();
		pauseButton.SetActive(true);
		startScreen.SetActive(false);
	}
}

