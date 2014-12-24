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
		if (player.Swords < 1) {
			player.Dead = true;
		} else {
			player.Swords -= 1;
			GameObject.Destroy(enemy.go);
		}
	}
}
