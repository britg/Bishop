using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoomFlowController : GameController {

	public GameObject roomsContainer;
	public float roomScrollOffset = 45f;
	public int minRoomCount = 2;
	public float roomLength = 31f;

	float currentZ;
	List<GameObject> rooms;
	GameObject[] roomPrefabs;

	float CurrentScroll {
		get {
			return transform.position.z;
		}
	}

	// Use this for initialization
	void Start () {
		roomPrefabs = Resources.LoadAll<GameObject>("Rooms");
		GetRooms();
		currentZ = 15.5f;
	}
	
	// Update is called once per frame
	void Update () {
		RemoveScrolledRooms();
		EnsureRooms();
	}

	void GetRooms () {
		rooms = new List<GameObject>();
		foreach(Transform child in roomsContainer.transform) {
			rooms.Add(child.gameObject);
		}
	}

	void RemoveScrolledRooms () {
		var roomsToDestroy = new List<GameObject>();
		foreach (GameObject room in rooms) {
			if (room.transform.position.z < CurrentScroll - roomScrollOffset) {
				roomsToDestroy.Add(room);
			}
		}

		foreach(GameObject room in roomsToDestroy) {
			rooms.Remove(room);
			Destroy(room);
		}
	}

	void EnsureRooms () {
		if (rooms.Count < minRoomCount) {
			AddRoom();
		}
	}

	void AddRoom () {
		var roomPrefab = (GameObject)roomPrefabs[Random.Range (0, roomPrefabs.Length)];
		var pos = new Vector3(0f, 0f, currentZ);
		var room = (GameObject)Instantiate(roomPrefab, pos, roomPrefab.transform.rotation);
		room.transform.SetParent(roomsContainer.transform);
		currentZ += roomLength;
		rooms.Add(room);
	}
}
