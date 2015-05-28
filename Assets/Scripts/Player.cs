using UnityEngine;
using System.Collections;

public class Player : Actor {

	public PlayerType playerType;
	public PlayerJob playerJob;
	//public BoardLocation location;
	//public HealthInfo health;
	//public StatsInfo stats;
	//public string actorName;
	public GameObject playerScreenObj;
	public static GameObject playerObj;

    private bool isDragging;

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
	
	public static Player CreatePlayer(int locX, int locY, int curHealth, int maxHealth, string pName, PlayerType type, 
	                       PlayerJob job, int strength, int magic, int speed)
	{
		playerObj = new GameObject ("Player Object");
		Player thisObj = playerObj.AddComponent<Player> ();

		thisObj.location = new BoardLocation(locX, locY);
		thisObj.health = new HealthInfo (curHealth, maxHealth);
		thisObj.actorName = pName;
		thisObj.playerType = type;
		thisObj.playerJob = job;
		thisObj.stats = new StatsInfo (strength, magic, speed);
		
		thisObj.playerScreenObj = new GameObject ();
		thisObj.playerScreenObj.AddComponent<SpriteRenderer> ();
		thisObj.playerScreenObj.transform.localScale = new Vector3 (0.3f, 0.3f, 0.3f);		

		return thisObj;
	}




}
