using UnityEngine;
using System.Collections;

public class HeartsView : GameView {

	Player player;

	int prevHeartCount = -1;

	public int heartCount = 5;
	public GameObject[] hearts;

	// Use this for initialization
	void Start () {
		player = GetPlayer();
		hearts = new GameObject[heartCount];
		int i = 0;
		foreach (Transform child in transform) {
			hearts[i] = child.gameObject;
			i++;
		}
	}
	
	// Update is called once per frame
	void Update () {
		UpdateHearts();
	}

	void UpdateHearts () {
		if (player.Hearts == prevHeartCount) {
			return;
		}

		int i = 0;
		foreach (GameObject heart in hearts) {
			if (i < player.Hearts) {
				heart.SetActive(true);
			} else {
				heart.SetActive(false);
			}
			i++;
		}
	}
}
