using UnityEngine;
using System.Collections;

public class ResetController : GameController {

	public void OnResetButton () {
		Application.LoadLevel(0);
	}

	public void OnQuitButton () {
		Application.Quit();
	}
}
