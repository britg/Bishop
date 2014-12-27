using UnityEngine;
using System.Collections;

public class MusicController : GameController{

	public bool shouldPlayMusic = false;
	public AudioSource music;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (music == null) {
			return;
		}

		if (Paused && music.isPlaying) {
			music.Pause();
		}

		if (!Paused && shouldPlayMusic && !music.isPlaying) {
			music.Play();
		}
	}
}
