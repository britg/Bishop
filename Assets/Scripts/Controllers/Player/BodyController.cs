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
		DetectGem(collider.gameObject);
		DetectSword(collider.gameObject);
		DetectHeart(collider.gameObject);
		DetectKey(collider.gameObject);
		DetectEnemy(collider.gameObject);
		DetectInstantDeath(collider.gameObject);
	}

	void DetectGem (GameObject go) {
		var gemController = go.GetComponent<GemController>();
		if (gemController != null) {
			var service = new GemPickupService(player, gemController.gem);
			service.Pickup();
			Destroy(go);
		}
	}

	void DetectSword (GameObject go) {

	}

	void DetectHeart (GameObject go) {

	}

	void DetectKey (GameObject go) {

	}

	void DetectEnemy (GameObject go) {
		if (go.tag == "Enemy") {
			var enemy = go.transform.parent.GetComponent<EnemyController>().enemy;
			var service = new EnemyCollisionService(player, enemy);
			service.Collide();
		}
	}

	void DetectInstantDeath (GameObject go) {
		if (go.tag == "InstantDeath") {
			var service = new InstantDeathService(player, go);
			service.Collide();
		}
	}

}
