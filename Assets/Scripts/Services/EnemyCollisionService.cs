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
			player.Gold += enemy.goldValue;
			GameObject.Destroy(enemy.go);
		} else {
			if (player.Hearts <= 1) {
				player.Hearts = 0;
				player.Dead = true;
				player.DeadBy = enemy.name;
			} else {
				player.Hearts -= 1;
			}
		} 
	}

	public bool PlayerSurvives () {
		return player.isSwiping;
	}
}
