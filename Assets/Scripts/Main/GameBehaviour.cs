using UnityEngine;
using System.Collections;

public class GameBehaviour : MonoBehaviour {

	protected Player GetPlayer () {
		var playerObj = GameObject.Find("Player");
		var playerController = playerObj.GetComponent<PlayerController>();
		return playerController.player;
	}
}
