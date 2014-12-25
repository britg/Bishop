using UnityEngine;
using System.Collections;

public class GoldPickupService {

	Player player;
	Gem gem;

	public GoldPickupService (Player _player, Gem _gem) {
		player = _player;
		gem = _gem;
	}

	public void Pickup () {
		player.Points += 1;
		player.Gold += 1;
	}

}
