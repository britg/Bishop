using UnityEngine;
using System.Collections;

public class FloorController : GameController {

	public GameObject floorTilePrefab;
	public Vector3 dimensions;
	public float tileWidth = 2.5f;

	// Use this for initialization
	void Start () {
		CreateTiles();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void CreateTiles () {
		for (float x = -dimensions.x/2f; x < dimensions.x; x++) {
			for (float z = 0; x < dimensions.z; z++) {
				var pos = new Vector3(x, 0f, z);
				GameObject tile = (GameObject)Instantiate(floorTilePrefab, pos, Quaternion.identity);
				tile.transform.SetParent(transform);
			}
		}
	}
}
