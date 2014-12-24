using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class ObjectPoolController : GameController {

	public GameObject poolContainer;
	public int goldPoolCount;

	List<GameObject> goldPool;
	GameObject goldPrefab;

	// Use this for initialization
	void Start () {
		PoolGold();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void PoolGold () {
		goldPool = new List<GameObject>();
		goldPrefab = ItemReferences.goldPrefab;
		for (int i = 0; i < goldPoolCount; i++) {
			var gold = (GameObject)Instantiate(goldPrefab);
			gold.transform.SetParent(poolContainer.transform);
			gold.SetActive(false);
			goldPool.Add(gold);
		}
	}

	public List<GameObject> GetGoldAmount (int amount) {
		var toRemove = goldPool.GetRange(0, amount);
		goldPool.RemoveRange(0, amount);
		return toRemove;
	}

	public GameObject GetGold () {
		var gold = goldPool[0];
		goldPool.RemoveAt(0);
		return gold;
	}

	public void ReturnGold (GameObject goldObj) {
		goldObj.SetActive(false);
		goldPool.Add(goldObj);
	}

	public void ReturnGold (List<GameObject> goldList) {
		goldList.Select(x => { x.SetActive(false); return x; });
		goldPool.AddRange(goldList);
	}

}
