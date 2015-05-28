using UnityEngine;
using System.Collections;
using UnityEditor;

[System.Serializable]
public class Actor : Extender  {
	public HealthInfo health;
	public StatsInfo stats;
	public BoardLocation location;
	public string actorName;

	private World world;

	public void EndTurn(Actor sender) {
		world.changeTurns();
	}

	public void onClickEndTurn() {
		if (EndsTurn != null) 
			EndsTurn(this);
	}

	void Start() {
		// events = this.gameObject.GetComponent<ActorEvents> ();

		EndsTurn += new ActorEventHandler (EndTurn);

		GameObject tmp = GameObject.Find ("World");
		world = ExtensionMethods.GetSafeComponent<World>(tmp);
	}

	
	public delegate void ActorEventHandler(Actor sender);
	
	/// <summary>
	/// Occurs when this actor draws a card.
	/// </summary>
	public event ActorEventHandler DrawCard;
	
	/// <summary>
	/// Occurs when this actor gains mana.
	/// </summary>
	public event ActorEventHandler GainMana;
	
	/// <summary>
	/// Occurs when this actor is damaged.
	/// </summary>
	public event ActorEventHandler IsDamaged;
	
	/// <summary>
	/// Occurs when this actor is healed.
	/// </summary>
	public event ActorEventHandler IsHealed;
	
	/// <summary>
	/// Occurs when this actor is reduced to 0 HP.
	/// </summary>
	public event ActorEventHandler IsKilled;
	
	/// <summary>
	/// Occurs when this actor begins their turn.
	/// </summary>
	public event ActorEventHandler BeginsTurn;
	
	/// <summary>
	/// Occurs when this actor ends their turn.
	/// </summary>
	public event ActorEventHandler EndsTurn;
	
	/// <summary>
	/// Occurs when this actor heals another actor.
	/// </summary>
	public event ActorEventHandler HealsOther;
	
	/// <summary>
	/// Occurs when this actor damages another actor.
	/// </summary>
	public event ActorEventHandler DamagesOther;
	
	/// <summary>
	/// Occurs when this actor moves.
	/// </summary>
	public event ActorEventHandler Moves;

}

[System.Serializable]
public class BoardLocation {
	public int x;
	public int y;

	public BoardLocation(int x, int y)
	{
		this.x = x;
		this.y = y;
	}
}

[System.Serializable]
public class HealthInfo {
	[SerializeField]
	private int currentHealth;

	[SerializeField]
	private int maxHealth;
	
	public int CurrentHealth {
		get 
		{ 
			return currentHealth;
		}
		set
		{
			if (value > maxHealth)
				currentHealth = maxHealth;
			currentHealth = value;
		}
	}

	public int MaxHealth {
		get 
		{ 
			return maxHealth;
		}
		set
		{
			if (value < currentHealth)
				currentHealth = value;
			maxHealth = value;
		}
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="HealthInfo"/> class.
	/// </summary>
	/// <param name="health">Health.</param>
	/// <param name="maxHealth">Max health.</param>
	public HealthInfo(int currentHealth, int maxHealth)
	{
		this.currentHealth = currentHealth;
		this.maxHealth = maxHealth;
	}
}

[System.Serializable]
public class StatsInfo {
	[Range(1, 99)]
	public int strength;

	[Range(1, 99)]
	public int magic;

	[Range(1, 99)]
	public int speed;

	public int maxMove;

	public int remainingMove;

	/// <summary>
	/// Initializes a new instance of the <see cref="StatsInfo"/> class.
	/// </summary>
	/// <param name="strength">Strength.</param>
	/// <param name="magic">Magic.</param>
	/// <param name="speed">Speed.</param>
	public StatsInfo(int strength, int magic, int speed)
	{
		this.strength = strength;
		this.magic = magic;
		this.speed = speed;
	}
}


