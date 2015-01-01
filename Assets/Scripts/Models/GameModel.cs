using UnityEngine;
using System.Collections;

public class GameModel {

	public GameObject go;
	public Vector3 CurrentPosition {
		get {
			return go.transform.position;
		}
	}


}
