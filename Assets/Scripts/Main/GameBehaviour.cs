using UnityEngine;
using System.Collections;

public class GameBehaviour : MonoBehaviour {

	protected Player GetPlayer () {
		var playerObj = GameObject.Find("Player");
		var playerController = playerObj.GetComponent<PlayerController>();
		return playerController.player;
	}

	public static bool Roll (float against) {
		return Random.value * 100f < against;
	}
}
