using UnityEngine;
using System.Collections;

public class MazeController : GameController {

	GameObject wallPrefab;
	Room room;

	// Use this for initialization
	void Start () {
		wallPrefab = ItemReferences.wallPrefab;
		room = gameObject.GetComponent<RoomController>().room;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void GenerateMaze () {

	}
}
