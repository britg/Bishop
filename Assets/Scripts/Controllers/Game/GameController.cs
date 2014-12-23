using UnityEngine;
using System.Collections;

public class GameController : GameBehaviour {

	public ItemReferences ItemReferences {
		get { return GameObject.Find("Game").GetComponent<ItemReferences>(); }
	}

}
