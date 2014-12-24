using UnityEngine;
using System.Collections;

public class GameController : GameBehaviour {

	GameObject gameRef;
	public GameObject GameRef {
		get {
			if (gameRef == null) {
				gameRef = GameObject.Find("Game");
			}
			return gameRef;
		}
	}

	ItemReferences itemRef;
	public ItemReferences ItemReferences {
		get { 
			if (GameRef == null) {
				return null;
			}
			if (itemRef == null) {
				itemRef = GameRef.GetComponent<ItemReferences>();
			}
			return itemRef;
		}
	}

	ObjectPoolController _objectPool;
	public ObjectPoolController ObjectPool {
		get { 
			if (GameRef == null) {
				return null;
			}
			if (_objectPool == null) {
				_objectPool = GameRef.GetComponent<ObjectPoolController>();
			}
			return _objectPool;
		}
	}

}
