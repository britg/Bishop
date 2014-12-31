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

		public Direction direction;
	}

	public float DetectRadius { get; set; }
	public float DetectTime { get; set; }

	public Enemy (Properties props) {
		Level = props.level;
		Courage = props.courage;
		WanderSpeed = props.wanderSpeed;
		AggroSpeed = props.aggroSpeed;
		CurrentDirection = props.direction;
		DetectRadius = props.detectRadius;
		DetectTime = props.detectTime;

		EnterState(props.state);
	}


}
