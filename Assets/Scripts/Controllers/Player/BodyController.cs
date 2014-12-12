using UnityEngine;
using System.Collections;

public class BodyController : GameController {

	Player player;

	// Use this for initialization
	void Start () {
		player = GetPlayer();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter (Collider collider) {
		Debug.Log ("Collided with " + collider.gameObject);
		DetectGem(collider.gameObject);

		Destroy(collider.gameObject);
	}

	void DetectGem (GameObject go) {
		var gemController = go.GetComponent<GemController>();
		if (gemController != null) {
			var service = new GemPickupService(player, gemController.gem);
			service.Pickup();
		}
	}

}
