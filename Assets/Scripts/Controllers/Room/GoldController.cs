using UnityEngine;
using System.Collections;

public class GoldController : MonoBehaviour {

	public int goldValue = 1;
	public Gem gem = new Gem();

	// Use this for initialization
	void Start () {
		gem.GoldValue = goldValue;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

}
