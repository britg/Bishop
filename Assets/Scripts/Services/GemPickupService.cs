using UnityEngine;
using System.Collections;

public class GemPickupService {

	Player player;
	Gem gem;

	public GemPickupService (Player _player, Gem _gem) {
		player = _player;
		gem = _gem;
	}

	public void Pickup () {
		player.Gold += gem.GoldValue;
		player.Courage += 1;
	}

}
