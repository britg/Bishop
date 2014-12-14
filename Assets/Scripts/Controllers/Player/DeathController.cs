using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DeathController : GameController {

	Player player;
	public GameObject gameOverPanel;

	void Start () {
		player = GetPlayer();
	}

	void Update () {
		if (player.Dead) {
			gameOverPanel.SetActive(true);
		}
	}
}
