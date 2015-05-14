using UnityEngine;
using System.Collections;

public class Player : Actor {

	public PlayerType playerType;
	public PlayerJob playerJob;


	public enum PlayerType {
		Warrior = 1,
		Mage = 2,
		Thief = 3
	}


	// Note: give each enum a value. Otherwise, if we decide to rearrange enums or remove them, 
	// the inspector value will break.
	public enum PlayerJob {
		Warrior = 1,
		Mage = 2,
		Thief = 3,
		Paladin = 4,
		Cleric = 5,
		Sniper = 6,
		Ninja = 7

	}


}
