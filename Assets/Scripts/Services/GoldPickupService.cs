using UnityEngine;
using System.Collections;

public class GoldPickupService {

	Player player;
	Gem gem;

	float currentComboHoldTime;

	public GoldPickupService (Player _player, Gem _gem) {
		player = _player;
		gem = _gem;
	}

	public void Pickup () {
		player.Points += 1;
		player.Gold += 1;
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
