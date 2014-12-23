using UnityEngine;
using System.Collections;

public class GemController : MonoBehaviour {

	public int goldValue = 1;
	public Gem gem = new Gem();

	// Use this for initialization
	void Start () {
		gem.GoldValue = goldValue;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter (Collider collider) {
//		Debug.Log ("Collided with " + collider.gameObject);

	}

	void OnCollisionEnter (Collision collision) {
//		Debug.Log ("Collision " + collision.gameObject);
	}
}
