using UnityEngine;
using System.Collections;

public class BodyController : GameController {

	Player player;
	public AudioSource goldPickupSound;
	public AudioSource swordPickupSound;
	public AudioSource keyPickupSound;

	public AudioSource attackSound;
	public AudioSource unlockSound;

	public GameObject playerGold;

	public float goldPickupTime = 0.2f;
	public float itemPickupTime = 0.3f;

	// Use this for initialization
	void Start () {
		player = GetPlayer();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter (Collider collider) {
		DetectGold(collider.gameObject);
		DetectSword(collider.gameObject);
		DetectHeart(collider.gameObject);
		DetectKey(collider.gameObject);
		DetectEnemy(collider.gameObject);
		DetectInstantDeath(collider.gameObject);
		DetectDoor(collider.gameObject);
		DetectCastle(collider.gameObject);
		DetectWaypoint(collider.gameObject);
	}

	void DetectGold (GameObject go) {
		var goldController = go.GetComponent<GoldController>();
		if (goldController != null) {
			var service = new GoldPickupService(player, goldController.gold);
			service.Pickup();
			PlayPickup();
			ShowGoldPickup();
			goldController.OnPickup();
		}
	}

	void PlayPickup () {
		goldPickupSound.Play();
	}

	bool goldAnimating = false;
	void ShowGoldPickup () {
		if (goldAnimating) {
			return;
		}
		goldAnimating = true;
		iTween.MoveBy (playerGold, iTween.Hash (
			"islocal", true,
			"y", 2f,
			"time", goldPickupTime,
			"oncomplete", "ResetGold",
			"oncompletetarget", gameObject));
		iTween.RotateBy (playerGold, iTween.Hash (
			"islocal", true,
			"y", 1f,
			"time", goldPickupTime));
	}

	void ResetGold () {
		goldAnimating = false;
		playerGold.transform.localPosition = Vector3.zero;
	}

	void DetectSword (GameObject go) {
		if (go.name == "Sword") {
			var swordController = go.transform.parent.gameObject.GetComponent<SwordController>();
			Sword sword = swordController.sword;
			var service = new SwordPickupService(player, sword);
			bool purchased = service.AttemptPurchase();
			if (purchased) {
				swordController.OnPickup();
				swordPickupSound.Play();
				Invoke ("RemoveSword", 10f); // TEMP for test
			}
		}
	}

	// TEMP
	void RemoveSword () {
		player.Swords = 0;
	}

	void DetectHeart (GameObject go) {

	}

	void DetectKey (GameObject go) {
		if (go.name == "Key") {
			player.Keys += 1;
			Destroy(go);
			keyPickupSound.Play();
		}
	}

	void DetectEnemy (GameObject go) {
		if (go.tag == "Enemy") {
			var enemy = go.transform.parent.GetComponent<EnemyController>().enemy;
			var service = new EnemyCollisionService(player, enemy);
			if (service.PlayerSurvives()) {
				DoAttack();
			}
			service.Collide();

		}
	}

	void DoAttack () {
		attackSound.Play();
		ShakeCamera();
	}

	void ShakeCamera () {
		iTween.ShakePosition(Camera.main.gameObject, iTween.Hash ("time", 0.2f, "amount", new Vector3(1f, 1f, 1f)));
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

	void DetectCastle (GameObject go) {
		if (go.name == "Castle Door Trigger") {
			ScrollingController.TransitionToCastle();
		}
	}

	void DetectWaypoint (GameObject go) {
		if (go.tag == "Waypoint") {
			var waypoint = go.transform.position;
			player.waypointService.OnEnterWaypoint(waypoint);
		}
	}

}
