using UnityEngine;
using System.Collections;

public class MusicController : GameController{

	public AudioSource music;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Paused && music.isPlaying) {
			music.Stop();
		}

		if (!Paused && !music.isPlaying) {
			music.Play();
		}
	}
}
