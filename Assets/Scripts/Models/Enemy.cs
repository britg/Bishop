using UnityEngine;
using System.Collections;

public class Enemy : Agent {

	[System.Serializable]
	public class Properties {
		public int level;
		public int courage;
		public float wanderSpeed;
		public float aggroSpeed;
		public State state;

		public Direction direction;
	}


	public Enemy (Properties props) {
		Level = props.level;
		Courage = props.courage;
		WanderSpeed = props.wanderSpeed;
		AggroSpeed = props.aggroSpeed;
		CurrentDirection = props.direction;

		EnterState(props.state);
	}


}
