using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Armory : GameModel {

	public static Dictionary<int, Weapon> Weapons = new Dictionary<int, Weapon>() {
		{ 1, new Weapon(1, "Wooden Sword", 0) },
		{ 2, new Weapon(2, "The Master Sword", 1000) },
		{ 3, new Weapon(3, "Glamdring", 2000) },
		{ 4, new Weapon(4, "Sikanda", 3000) },
		{ 5, new Weapon(5, "Blackfyre", 4000) },
		{ 6, new Weapon(6, "Ice", 5000) },
		{ 7, new Weapon(7, "Needle", 6000) },
		{ 8, new Weapon(8, "Gryffindor", 7000) },
		{ 9, new Weapon(9, "Sting", 8000) },
		{ 10, new Weapon(10, "Green Destiny", 9000) },
		{ 11, new Weapon(11, "Excalibur", 10000) }
	};

	public static Armory PlayerArmory () {
		Armory armory = new Armory();
		armory.Load();
		return armory;
	}

	void Load () {

	}

}
