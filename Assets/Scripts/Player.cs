using UnityEngine;
using System.Collections;

/// <summary>
/// Represents possible player types.
/// </summary>
public enum PlayerType
{
    Warrior = 1,
    Mage = 2,
    Thief = 3
}

/// <summary>
/// Represents possible player jobs.
/// 
/// Note: give each enum a value. Otherwise, if we decide to rearrange enums or remove them, 
/// the inspector value will break.
/// </summary>
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

/// <summary>
/// Player.
/// </summary>
public class Player : Actor {

	/// <summary>
	/// The type of the _player.
	/// </summary>
	private PlayerType _playerType;

	/// <summary>
	/// Gets or sets the type of the player.
	/// </summary>
	/// <value>The type of the player.</value>
	public PlayerType playerType {
		get {
			return _playerType;
		}
		set {
			_playerType = value;
		}
	}

	/// <summary>
	/// The _player job.
	/// </summary>
	private PlayerJob _playerJob;

	/// <summary>
	/// Gets or sets the player job.
	/// </summary>
	/// <value>The player job.</value>
	public PlayerJob playerJob {
		get {
			return _playerJob;
		}
		set {
			_playerJob = value;
		}
	}

    private bool isDragging;

	/// <summary>
	/// Creates the player.
	/// </summary>
	/// <returns>The player.</returns>
	/// <param name="locX">Location x.</param>
	/// <param name="locY">Location y.</param>
	/// <param name="curHealth">Current health.</param>
	/// <param name="maxHealth">Max health.</param>
	/// <param name="pName">P name.</param>
	/// <param name="type">Type.</param>
	/// <param name="job">Job.</param>
	/// <param name="strength">Strength.</param>
	/// <param name="magic">Magic.</param>
	/// <param name="speed">Speed.</param>
	/// <param name="maxMove">Max move.</param>
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

		actorObj.transform.SetParent(GameObject.Find("Players").transform);
		actorObj.name = player.actorName;

		return player;
	}
}
