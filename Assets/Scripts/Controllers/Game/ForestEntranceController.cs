using UnityEngine;
using System.Collections;

public class ForestEntranceController : GameController {

	Player player;
	public GameObject forestPrompt;

	void Start () {
		player = GetPlayer();
	}

	void OnTriggerEnter () {
		Pause();
		forestPrompt.SetActive(true);
	}

	public void OnPromptYes () {

	}

	public void OnPromptNo () {
		forestPrompt.SetActive(false);
		player.CurrentDirection = Agent.Direction.Left;
		Unpause();
	}
}
