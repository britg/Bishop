﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FPSView : GameView {

	public Text label;
	public float updateInterval = 0.1f;
	
	private float accum = 0.0f; // FPS accumulated over the interval
	private int frames = 0; // Frames drawn over the interval
	private float timeleft; // Left time for current interval
	
	// Use this for initialization
	void Start () {
		timeleft = updateInterval;
	}
	
	// Update is called once per frame
	void Update () {
		
		timeleft -= Time.deltaTime;
		accum += Time.timeScale/Time.deltaTime;
		++frames;
		
		// Interval ended - update GUI text and start new interval
		if (timeleft <= 0.0f) {   
			// display two fractional digits (f2 format)
			label.text = "FPS: " + (accum/frames).ToString("f2");
			timeleft = updateInterval;
			accum = 0.0f;
			frames = 0;
		}
	}

}
