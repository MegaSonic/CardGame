using UnityEngine;
using System.Collections;

public enum PlayerType
{
    Warrior = 1,
    Mage = 2,
    Thief = 3
}

// Note: give each enum a value. Otherwise, if we decide to rearrange enums or remove them, 
// the inspector value will break.
public enum PlayerJob
{
    Warrior = 1,
    Mage = 2,
    Thief = 3,
    Paladin = 4,
    Cleric = 5,
    Sniper = 6,
    Ninja = 7

}

public class Player : Actor {

	public PlayerType playerType;
	public PlayerJob playerJob;
    //public SpriteRenderer playerSprite;
	//public static GameObject playerObj;

    private bool isDragging;
	
	public static Player CreatePlayer(int locX, int locY, int curHealth, int maxHealth, string pName, PlayerType type, 
	                       PlayerJob job, int strength, int magic, int speed, int maxMove)
	{
        actorObj = Instantiate(Resources.Load("Player Object")) as GameObject;
		Player player = actorObj.GetSafeComponent<Player>();

		player.location = new BoardLocation(locX, locY);
		player.health = new HealthInfo (curHealth, maxHealth);
		player.actorName = pName;
		player.playerType = type;
		player.playerJob = job;
		player.stats = new StatsInfo (strength, magic, speed, maxMove);

		player.actorSprite = actorObj.GetComponent<SpriteRenderer>();
		// thisObj.playerScreenObj.AddComponent<SpriteRenderer> ();
		// thisObj.playerScreenObj.transform.localScale = new Vector3 (0.3f, 0.3f, 0.3f);		

		//actorObj.transform.parent = GameObject.Find("Players").transform;
		actorObj.transform.SetParent(GameObject.Find("Players").transform);
		actorObj.name = player.actorName;

		return player;
	}
}
