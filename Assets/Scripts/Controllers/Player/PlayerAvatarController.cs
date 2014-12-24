using UnityEngine;
using System.Collections;

public class PlayerAvatarController : GameController {

	Player player;
	public GameObject sword;

	// Use this for initialization
	void Start () {
		player = GetPlayer();
	}
	
	// Update is called once per frame
	void Update () {
		ShowHideSword();
	}

	void ShowHideSword () {
		if (player.Swords > 0) {
			sword.SetActive(true);
		} else {
			sword.SetActive(false);
		}
	}
}
