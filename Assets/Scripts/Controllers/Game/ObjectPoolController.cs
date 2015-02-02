using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class ObjectPoolController : GameController {

	public GameObject poolContainer;
	public int goldPoolCount;
	public int wallPoolCount;

	List<GameObject> goldPool;
	GameObject goldPrefab;

	List<GameObject> wallPool;
	GameObject wallPrefab;

	// Use this for initialization
	void Start () {
//		PoolGold();
		PoolWalls();
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

	void PoolWalls () {
		wallPool = new List<GameObject>();
		wallPrefab = ItemReferences.wallPrefab;
		for (int i = 0; i < wallPoolCount; i++) {
			var wall = (GameObject)Instantiate(wallPrefab);
			wall.transform.SetParent(poolContainer.transform);
			wall.SetActive(false);
			wallPool.Add(wall);
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


	public List<GameObject> GetWallAmount (int amount) {
		var toRemove = wallPool.GetRange(0, amount);
		wallPool.RemoveRange(0, amount);
		return toRemove;
	}

	public GameObject GetWall () {
		var wall = wallPool[0];
		wallPool.RemoveAt(0);
		return wall;
	}

	public void ReturnWall (GameObject wallObj) {
		wallObj.transform.SetParent(poolContainer.transform);
		wallObj.SetActive(false);
		wallPool.Add(wallObj);
	}

	public void ReturnWalls (List<GameObject> wallList) {
		wallList.Select(x => { x.SetActive(false); return x; });
		wallPool.AddRange(wallList);
	}

}
