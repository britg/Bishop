using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerPropertyView : GameBehaviour {

	public enum Property {
		Courage,
		Level,
		Gold,
		Points,
		Swords,
		Hearts,
		HighScore,
		Keys
	}

	Player player;
	public Property propertyToWatch;
	Text textView;

	// Use this for initialization
	void Start () {
		player = GetPlayer();
		textView = gameObject.GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		UpdateProperty();
	}

	void UpdateProperty () {
		if (textView == null) {
			return;
		}

		if (propertyToWatch == Property.Courage) {
			textView.text = string.Format("{0}", player.Courage);
		} else if (propertyToWatch == Property.Level) {
			textView.text = string.Format("{0}", player.Level);
		} else if (propertyToWatch == Property.Gold) {
			textView.text = string.Format("{0}", player.Gold);
		} else if (propertyToWatch == Property.Points) {
			textView.text = string.Format("{0}", player.Points);
		} else if (propertyToWatch == Property.Swords) {
			textView.text = string.Format("{0}", player.Swords);
		} else if (propertyToWatch == Property.Hearts) {
			textView.text = string.Format("{0}", player.Hearts);
		} else if (propertyToWatch == Property.HighScore) {
			textView.text = string.Format("{0}", player.HighScore);
		} else if (propertyToWatch == Property.Keys) {
			textView.text = string.Format("{0}", player.Keys);
		}

	}
}
