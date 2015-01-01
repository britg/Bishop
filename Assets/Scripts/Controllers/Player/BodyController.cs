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

	public float gemPickupTime = 0.2f;
	public float itemPickupTime = 0.3f;

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
		DetectCastle(collider.gameObject);
		DetectWaypoint(collider.gameObject);
	}

	void DetectGem (GameObject go) {
		var gemController = go.GetComponent<GoldController>();
		if (gemController != null) {
			var service = new GoldPickupService(player, gemController.gem);
			service.Pickup();
			PlayPickup();
			ShowGemPickup();
			go.SetActive(false);
		}
	}

	void PlayPickup () {
		goldPickupSound.Play();
	}

	bool goldAnimating = false;
	void ShowGemPickup () {
		if (goldAnimating) {
			return;
		}
		goldAnimating = true;
		iTween.MoveBy (playerGold, iTween.Hash (
			"islocal", true,
			"y", 2f,
			"time", gemPickupTime,
			"oncomplete", "ResetGem",
			"oncompletetarget", gameObject));
		iTween.RotateBy (playerGold, iTween.Hash (
			"islocal", true,
			"y", 1f,
			"time", gemPickupTime));
	}

	void ResetGem () {
		goldAnimating = false;
		playerGold.transform.localPosition = Vector3.zero;
	}

	void DetectSword (GameObject go) {
		if (go.name == "Sword") {
			player.Swords += 1;
			Destroy(go);
			swordPickupSound.Play();
		}
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
			player.waypointsTraversed.Add(go.transform.position);
		}
	}

}
