using UnityEngine;
using System.Collections;

public class ScrollController : GameController {

	public Vector3 scrollSpeed;
	public Vector3 scrollAccel;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += scrollSpeed * Time.deltaTime;
		scrollSpeed += scrollAccel * Time.deltaTime;
	}
}
