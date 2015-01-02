using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerDetectController : GameController {

	public Agent agent { get; set; }
	Player player;

	// Use this for initialization
	void Start () {
		player = GetPlayer();
	}
	
	// Update is called once per frame
	void Update () {
		if (agent.CurrentState != Agent.State.Aggro) {
			DetectPlayer();
		}
	}

	float currentDetectTime = 0f;
	void DetectPlayer () {
		if (currentDetectTime < agent.DetectTime) {
			ContinueDetect();
		} else {
			agent.OnDetectPlayer(player);
		}
	}
	
	void ContinueDetect () {
		var origin = transform.position;
		RaycastHit[] forwardHits = Physics.RaycastAll (transform.position, Vector3.forward, agent.DetectRadius);
		RaycastHit[] backHits = Physics.RaycastAll (transform.position, Vector3.back, agent.DetectRadius);
		RaycastHit[] rightHits = Physics.RaycastAll (transform.position, Vector3.right, agent.DetectRadius);
		RaycastHit[] leftHits = Physics.RaycastAll (transform.position, Vector3.left, agent.DetectRadius);

		bool forward = ScanHits(forwardHits);
		bool back = ScanHits(backHits);
		bool right = ScanHits(rightHits);
		bool left = ScanHits(leftHits);

		if (forward || back || right || left) {
			currentDetectTime += Time.deltaTime;
		} else {
			currentDetectTime = 0f;
		}
	}

	bool ScanHits (RaycastHit[] hits) {
		float playerDist = Mathf.Infinity;
		float wallDist = Mathf.Infinity;
		foreach(var hit in hits) {
			if (GameLayer.isWall(hit)) {
				if (hit.distance < wallDist) {
					wallDist = hit.distance;
				}
			}
			
			if (GameLayer.isPlayer(hit)) {
				playerDist = hit.distance;
			}
		}
		
		if (playerDist < wallDist) {
			return true;
		} 

		return false;
	}
}
