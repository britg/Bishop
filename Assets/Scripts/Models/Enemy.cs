using UnityEngine;
using System.Collections;

public class Enemy : Agent {

	[System.Serializable]
	public class Properties {
		public int level;
		public int courage;
		public float wanderSpeed;
		public float aggroSpeed;

		public bool wanders;
		public State state;
		public float detectRadius;
		public float detectTime;
		public float alertTime;

		public Direction direction;
	}

	public Enemy (Properties props) {
		Level = props.level;
		Courage = props.courage;
		WanderSpeed = props.wanderSpeed;
		AggroSpeed = props.aggroSpeed;
		CurrentDirection = props.direction;
		DetectRadius = props.detectRadius;
		DetectTime = props.detectTime;
		AlertTime = props.alertTime;

		EnterState(props.state);
	}


}
