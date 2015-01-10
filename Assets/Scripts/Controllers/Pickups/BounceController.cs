using UnityEngine;
using System.Collections;

public class BounceController : MonoBehaviour {

	public Vector3 bounce;
	public float time;

	// Use this for initialization
	void Start () {
		BounceUp();
	}
	
	// Update is called once per frame
	void Update () {
	}

	void BounceUp () {
		iTween.MoveBy(gameObject, iTween.Hash ("amount", bounce, 
		                                       "time", time,
		                                       "oncomplete", "BounceUpDone"));
	}

	void BounceUpDone () {
		iTween.MoveBy(gameObject, iTween.Hash ("amount", -bounce, 
		                                       "time", time,
		                                       "oncomplete", "BounceUp"));
	}
}
