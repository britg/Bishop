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

	GameObject railRef;
	public GameObject RailRef {
		get {
			if (railRef == null) {
				railRef = GameObject.Find("Rail");
			}
			return railRef;
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

	PauseController pauseController;
	public PauseController PauseController {
		get {
			if (GameRef == null) {
				return null;
			}
			if (pauseController == null) {
				pauseController = GameRef.GetComponent<PauseController>();
			}
			return pauseController;

		}
	}
	public virtual bool Paused {
		get {
			if (GameRef == null) {
				return true;
			}
			return PauseController.Paused;
		}
	}

	public virtual void Pause () {
		PauseController.Pause();
	}

	public virtual void Unpause () {
		PauseController.Unpause();
	}

	public virtual void StartGame () {
		PauseController.StartGame();
	}

	public virtual void ShowDaily () {
		PauseController.ShowDaily();
	}

	public virtual void ShowNormal () {
		PauseController.ShowNormal();
	}

	ScrollController scrollingController;
	public ScrollController ScrollingController {
		get {
			if (scrollingController == null) {
				scrollingController = RailRef.GetComponent<ScrollController>();
			}
			return scrollingController;
		}
	}

}
