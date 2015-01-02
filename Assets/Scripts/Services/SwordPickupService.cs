using UnityEngine;
using System.Collections;

public class SwordPickupService {

	Player player;
	Sword sword;

	public SwordPickupService (Player _p, Sword _s) {
		player = _p;
		sword = _s;
	}

	public bool AttemptPurchase () {
		if (player.Gold >= sword.cost) {
			player.Swords += 1;
			player.Gold -= sword.cost;
			return true;
		}
		return false;
	}

}
