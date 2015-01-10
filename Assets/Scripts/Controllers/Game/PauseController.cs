using UnityEngine;
using System.Collections;

public class PauseController : GameController {

	Player player;
	public bool paused = false;
	public GameObject pauseButton;
	public GameObject startScreen;

	void Start () {
		player = GetPlayer();
		pauseButton.SetActive(false);
		startScreen.SetActive(true);
	}

	void Update () {
		if (player.Dead) {
			pauseButton.SetActive(false);
		}
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

	public override void StartGame () {
		Unpause();
		pauseButton.SetActive(true);
		startScreen.SetActive(false);
	}
}

