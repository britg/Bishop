using UnityEngine;
using System.Collections;

public class SwordController : GameController {

	public int cost = 100;
	public int pointValue = 100;
	public Sword sword = new Sword();

	// Use this for initialization
	void Start () {
		sword.go = gameObject;
		sword.cost = cost;
		sword.pointValue = pointValue;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnPickup () {
		Invoke("Reactivate", 10f);
		gameObject.SetActive(false);
	}

	public void Reactivate () {
		gameObject.SetActive(true);
	}
}
