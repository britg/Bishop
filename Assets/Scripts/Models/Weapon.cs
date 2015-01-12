using UnityEngine;
using System.Collections;

public class Weapon : GameModel {

	public int id;
	public string name;
	public int cost;
	public bool unlocked;

	public Weapon (int _id, string _name, int _cost) {
		id = _id;
		name = _name;
		cost = _cost;
	}

}
