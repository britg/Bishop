using UnityEngine;
using System.Collections;

public class ForestEntranceController : GameController {

	public GameObject forestPrompt;

	void Start () {
	}

	void OnTriggerEnter () {
		Pause();
		forestPrompt.SetActive(true);
	}

	public void OnPromptYes () {
		forestPrompt.SetActive(false);
		Unpause();
	}

	public void OnPromptNo () {
		forestPrompt.SetActive(false);
		Unpause();
	}
}
