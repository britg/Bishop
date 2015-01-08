using UnityEngine;
using System.Collections;

public class CameraController : GameController {

	public int framerate = 60;
	public bool followPlayer = true;

	public float updateInterval = 0.1f;
	
	private float accum = 0.0f; // FPS accumulated over the interval
	private int frames = 0; // Frames drawn over the interval
	private float timeleft; // Left time for current interval
	float fps = 60f;

	// Use this for initialization
	void Start () {
		timeleft = updateInterval;
		Application.targetFrameRate = framerate;
	}
	
	// Update is called once per frame
	void Update () {
		if (!downgraded) {
			Measure();
			if (currentDowngradeTime > cumulativeDowngradeTime) {
				Downgrade();
			}
		}
	}

	void Measure () {
		timeleft -= Time.deltaTime;
		accum += Time.timeScale/Time.deltaTime;
		++frames;
		
		// Interval ended - update GUI text and start new interval
		if (timeleft <= 0.0f) {   
			// display two fractional digits (f2 format)
			fps = accum/frames;
			timeleft = updateInterval;
			accum = 0.0f;
			frames = 0;
			if (fps < 30f) {
				currentDowngradeTime += updateInterval;
			}

		}
	}

	public float cumulativeDowngradeTime = 3f;
	float currentDowngradeTime = 0f;
	bool downgraded = false;
	void LateUpdate () {
	}

	void Downgrade () {
		downgraded = true;
		Camera.main.renderingPath = RenderingPath.Forward;
	}

}
