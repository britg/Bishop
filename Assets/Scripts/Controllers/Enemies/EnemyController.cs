using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

	public Enemy enemy;

	public Enemy.Properties enemyInitialization;

	// Use this for initialization
	void Start () {
		enemy = new Enemy(enemyInitialization);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
