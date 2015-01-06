using UnityEngine;
using System.Collections;

public class GoldController : GameController {

//	public int goldValue = 1;
	public int pointValue = 1;
//	public float reactivateTime = 10f;

	public Gold gold = new Gold();

	// Use this for initialization
	void Start () {
		gold.go = gameObject;
//		gold.value = goldValue;
		gold.pointValue = pointValue;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnPickup () {
//		Invoke("Reactivate", reactivateTime);
//		gameObject.SetActive(false);
//		transform.parent.gameObject.SetActive(false);
		Destroy(transform.parent.gameObject);
	}

	void Reactivate () {
		gameObject.SetActive(true);
	}

}
