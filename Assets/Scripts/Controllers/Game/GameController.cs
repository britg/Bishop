using UnityEngine;
using System.Collections;

public class GameController : GameBehaviour {

	public ItemReferences ItemReferences {
		get { return GameObject.Find("Game").GetComponent<ItemReferences>(); }
	}

	ObjectPoolController _objectPool;
	public ObjectPoolController ObjectPool {
		get { 
			if (_objectPool == null) {
				_objectPool = GameObject.Find("Game").GetComponent<ObjectPoolController>();
			}
			return _objectPool;
		}
	}

}
