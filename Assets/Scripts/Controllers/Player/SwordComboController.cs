using UnityEngine;
using System.Collections;

public class SwordComboController : GameController {

	Player player;

	int COMBO_COUNT = 10;

	// Use this for initialization
	void Start () {
		player = GetPlayer();
	}
	
	// Update is called once per frame
	void Update () {
		if (player.CurrentCombo >= COMBO_COUNT) { // temp
			player.Swords++;
			player.CurrentCombo = 0;
		}
	}
}
