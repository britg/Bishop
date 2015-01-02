using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DeathController : GameController {

	Player player;
	public GameObject gameOverPanel;
	public GameObject gameOverText;

	public AudioSource diedSound;

	void Start () {
		player = GetPlayer();
	}

	bool inTransition = false;
	void Update () {
		if (Paused) {
			return;
		}
		if (player.Dead && !inTransition) {
			DoDeadTransition();
		}
	}

	void DoDeadTransition () {
		Pause();
		inTransition = true;
		diedSound.Play();
		iTween.MoveTo (Camera.main.gameObject, iTween.Hash ("time", 1f,
		                                                    "position", transform.position + new Vector3(0f, 5f, 0f),
		               										"oncomplete", "ShowRestart",
		               										"oncompletetarget", gameObject));
	}

	void ShowRestart () {
		Application.LoadLevel(0);
//		gameOverPanel.SetActive(true);
//		gameOverText.SetActive(true);
//		inTransition = false;
	}
}
