using UnityEngine;
using System.Collections;

public class RotateController : GameController {

	public Vector3 rotateSpeed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(rotateSpeed*Time.deltaTime);
	}
}
