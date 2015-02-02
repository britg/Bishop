using UnityEngine;
using System.Collections;

public class DoorController : GameController {

	Player player;
    public Door door;

	// Use this for initialization
	void Start () {
		player = GetPlayer();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnOpen () {
		Pause();
		MovePlayerToDoorCenter();
		ScrollingController.ResetOnPlayer();
		RaiseDoor();
    }

    public void OnNotOpen () {

    }

	void MovePlayerToDoorCenter () {
		Vector3 pos = transform.position + Vector3.right;
		iTween.MoveTo(player.go, iTween.Hash ("position", pos, 
		                                      "time", 3f,
		                                      "oncomplete", "Continue",
		                                      "oncompletetarget", gameObject));
	}

	void RaiseDoor () {
		iTween.MoveBy(gameObject, iTween.Hash ("y", 5f, "time", 1f));
	}

	void Continue () {
		Unpause();
		Destroy(gameObject);
	}
}
