using UnityEngine;
using System.Collections;

public class GoldPickupService {

	Player player;
	Gold gold;

	float currentComboHoldTime;

	public GoldPickupService (Player _player, Gold _gold) {
		player = _player;
		gold = _gold;
	}

	public void Pickup () {
		player.Points += gold.pointValue;
	}

	void ContinueCombo () {
		player.CurrentCombo += 1;
		currentComboHoldTime = 0;
	}

	void CheckComboHold () {
		if (currentComboHoldTime > player.ComboHoldTime) {
			StopCombo();
		}
	}

	void StopCombo () {
		player.CurrentCombo = 0;
	}

}
