using UnityEngine;
using System.Collections;

public class EnemyCollisionService {

	Player player;
	Enemy enemy;

	public EnemyCollisionService (Player _player, Enemy _enemy) {
		player = _player;
		enemy = _enemy;
	}

	public void Collide () {
		if (PlayerSurvives()) {
			player.Points += enemy.pointValue;
			GameObject.Destroy(enemy.go);
		} else {
			if (player.Hearts <= 1) {
				player.Hearts = 0;
				player.Dead = true;
			} else {
				player.Hearts -= 1;
			}
		} 
	}

	public bool PlayerSurvives () {
		return player.Swords > 0;
	}
}
