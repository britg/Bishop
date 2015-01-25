using UnityEngine;
using System.Collections;

public class DoorController : GameController {

    public Door door;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnOpen () {
        Destroy(gameObject);
    }

    public void OnNotOpen () {

    }
}
