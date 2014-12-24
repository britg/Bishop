using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ControlsController : GameController {

	public GameObject menuPanel;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnMenuButton () {
		menuPanel.SetActive(!menuPanel.activeSelf);
	}

}
