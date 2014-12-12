using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerPropertyView : GameBehaviour {

	public enum Property {
		Courage,
		Level,
		Gold
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
		}

	}
}
