using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoomFlowController : GameController {

	Player player;

	public GameObject roomsContainer;
	public float roomScrollOffset = 45f;
	public int minRoomCount = 2;
	public float roomLength = 31f;

	public float currentZ;
	List<GameObject> roomObjs;
	GameObject[] roomPrefabs;

	float CurrentScroll {
		get {
			return transform.position.z;
		}
	}

	Room _currentRoom;
	Room currentRoom {
		get {
			if (_currentRoom == null) {
				_currentRoom = roomObjs[0].GetComponent<RoomController>().room;
			}
			return _currentRoom;
		}
	}

	// Use this for initialization
	void Start () {
		roomPrefabs = Resources.LoadAll<GameObject>("Rooms");
		GetRoomObjects();
		EnsureRoomObjects();
		player = GetPlayer();
		PlacePlayer();
	}
	
	// Update is called once per frame
	void Update () {
		RemoveScrolledRoomObjects();
		EnsureRoomObjects();
	}

	void GetRoomObjects () {
		roomObjs = new List<GameObject>();
		foreach(Transform child in roomsContainer.transform) {
			roomObjs.Add(child.gameObject);
		}
	}

	void RemoveScrolledRoomObjects () {
		var roomsToDestroy = new List<GameObject>();
		foreach (GameObject roomObj in roomObjs) {
			if (roomObj.transform.position.z < CurrentScroll - roomScrollOffset) {
				roomsToDestroy.Add(roomObj);
			}
		}

		foreach(GameObject roomObj in roomsToDestroy) {
			roomObjs.Remove(roomObj);
			Destroy(roomObj);
		}
	}

	void EnsureRoomObjects () {
		if (roomObjs.Count < minRoomCount) {
			AddRoomObject();
		}
	}

	void AddRoomObject () {
		var roomPrefab = (GameObject)roomPrefabs[Random.Range (0, roomPrefabs.Length)];
		var pos = new Vector3(0f, 0f, currentZ);
		var roomObj = (GameObject)Instantiate(roomPrefab, pos, roomPrefab.transform.rotation);
		roomObj.transform.SetParent(roomsContainer.transform);
		currentZ += roomLength;
		roomObjs.Add(roomObj);
	}

	void PlacePlayer () {
		player.room = currentRoom;
	}
}
