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
		player.Dead = true;
	}
}
