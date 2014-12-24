using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DeathController : GameController {

	Player player;
	public GameObject gameOverPanel;
	public GameObject gameOverText;

	void Start () {
		player = GetPlayer();
	}

	void Update () {
		if (Paused) {
			return;
		}
		if (player.Dead) {
			gameOverPanel.SetActive(true);
			gameOverText.SetActive(true);
			PauseController.Pause();
		}
	}
}
