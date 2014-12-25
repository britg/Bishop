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
		DetectDoor(collider.gameObject);
	}

	void DetectGem (GameObject go) {
		var gemController = go.GetComponent<GoldController>();
		if (gemController != null) {
			var service = new GoldPickupService(player, gemController.gem);
			service.Pickup();
			ObjectPool.ReturnGold(go);
		}
	}

	void DetectSword (GameObject go) {
		if (go.name == "Sword") {
			player.Swords += 1;
			Destroy(go);
		}
	}

	void DetectHeart (GameObject go) {

	}

	void DetectKey (GameObject go) {
		if (go.name == "Key") {
			player.Keys += 1;
			Destroy(go);
		}
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

	void DetectDoor (GameObject go) {
		if (go.name == "Door Trigger") {
			if (player.Keys > 0) {
				player.Keys -= 1;
				Destroy(go.transform.parent.gameObject);
			}
		}
	}

}
