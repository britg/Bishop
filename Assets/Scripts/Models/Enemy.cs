using UnityEngine;
using System.Collections;

public class Enemy : GameModel {

	[System.Serializable]
	public class Properties {
		public int level;
		public int courage;
	}

	public int Level { get; set; }
	public int Courage { get; set; }

	public Enemy (Properties props) {
		Level = props.level;
		Courage = props.courage;
	}

}
