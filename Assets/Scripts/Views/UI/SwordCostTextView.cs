using UnityEngine;
using System.Collections;

public class SwordCostTextView : GameView {

	Sword sword;
	TextMesh text;

	// Use this for initialization
	void Start () {
		sword = transform.parent.GetComponent<SwordController>().sword;
		text = GetComponent<TextMesh>();
		Invoke ("SetSwordCostText", 0.1f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void SetSwordCostText () {
		text.text = string.Format("{0}", sword.cost);
	}
}
