using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;
using System;

/// <summary>
/// Direction.
/// </summary>
public enum Direction {
    Up = 0,
    Left = 1,
    Right = 2,
    Down = 3
}

/// <summary>
/// Actor.
/// </summary>
[System.Serializable]
public class Actor : Extender  {

	/// <summary>
	/// The actor's health information.
	/// </summary>
	private HealthInfo _health;

	/// <summary>
	/// Gets or sets the health.
	/// </summary>
	/// <value>The health.</value>
	public HealthInfo health {
		get {
			return _health;
		}
		set {
			_health = value;
		}
	}

	/// <summary>
	/// The actor's stats information.
	/// </summary>
	private StatsInfo _stats;

	/// <summary>
	/// Gets or sets the stats.
	/// </summary>
	/// <value>The stats.</value>
	public StatsInfo stats {
		get {
			return _stats;
		}
		set {
			_stats = value;
		}
	}

	/// <summary>
	/// The actor's location on the game board.
	/// </summary>
	private BoardLocation _location;

	/// <summary>
	/// Gets or sets the location.
	/// </summary>
	/// <value>The location.</value>
	public BoardLocation location {
		get {
			return _location;
		}
		set {
			_location = value;
		}
	}

	/// <summary>
	/// The name of the actor.
	/// </summary>
	protected string actorName;

	/// <summary>
	/// The actor's sprite.
	/// </summary>
	private SpriteRenderer _actorSprite;

	/// <summary>
	/// Gets or sets the actor sprite.
	/// </summary>
	/// <value>The actor sprite.</value>
	public SpriteRenderer actorSprite {
		get {
			return _actorSprite;
		}
		set {
			_actorSprite = value;
		}
	}

	/// <summary>
	/// The actor object.
	/// </summary>
	protected static GameObject actorObj;

	private Battle battle;
	private TextMesh healthDisplay;

    private Dictionary<EventName, ActorEvent> actorEventList = new Dictionary<EventName,ActorEvent>();

	/// <summary>
	/// Calls the end turn event.
	/// </summary>
	public void CallEndTurnEvent() {
        CallActorEvent(EventName.EndedTurn);
	}

	void Start() {
		// events = this.gameObject.GetComponent<ActorEvents> ();

		GameObject tmp = GameObject.Find ("Battle");
		battle = ExtensionMethods.GetSafeComponent<Battle>(tmp);
        InitializeEvents();
        actorEventList[EventName.Moved].AddEvent(EffectType.GainMana, 2);
	}

    
    /// <summary>
    /// Calls the event associated with the EventName e.
    /// </summary>
    /// <param name="e"></param>
	public void CallActorEvent(EventName e) {
        if (actorEventList.ContainsKey(e))
        {
            actorEventList[e].CallEvent();
        }
    }

    /// <summary>
    /// Adds a new effect to an event.
    /// </summary>
    /// <param name="eve">The event</param>
    /// <param name="eff">The effect lookup type</param>
    /// <param name="payload">Any extra parameters</param>
    public void AddEffectToEvent(EventName eve, EffectType eff, params object[] payload)
    {
        actorEventList[eve].AddEvent(eff, payload);
    }

    /// <summary>
    /// Initializes events. Will automatically create a new event for every type of events in the enum in ActorEvents.
    /// </summary>
    public void InitializeEvents()
    {
        foreach (EventName e in Enum.GetValues(typeof(EventName)))
        {
            actorEventList.Add(e, new ActorEvent());
        }
    }

	/// <summary>
	/// Updates the health display.
	/// </summary>
	public void UpdateHealthDisplay()
	{
		if (healthDisplay == null && !InitializeHealthDisplay())
			return;

		healthDisplay.text = health.CurrentHealth.ToString();
	}
    
	/// <summary>
	/// Initializes the health display.
	/// </summary>
	/// <returns><c>true</c>, if health display was initialized, <c>false</c> otherwise.</returns>
	private bool InitializeHealthDisplay()
	{
		healthDisplay = GetComponentInChildren<TextMesh>();

		if (healthDisplay == null)
			return false;
		else
			return true;
	}

	/// <summary>
	/// Returns a string that represents the current object.
	/// </summary>
	/// <returns>A string that represents the current object.</returns>
	/// <filterpriority>2</filterpriority>
	public override string ToString ()
	{
		return actorName;
	}
}

/// <summary>
/// Board location.
/// </summary>
[System.Serializable]
public class BoardLocation {

	private int _x;
	public int x {
		get {
			return _x;
		}
		set {
			_x = value;
		}
	}

	private int _y;
	public int y {
		get {
			return _y;
		}
		set {
			_y = value;
		}
	}

	public BoardLocation(int x, int y)
	{
		this.x = x;
		this.y = y;
	}
}

/// <summary>
/// Health info.
/// </summary>
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
	public StatsInfo(int strength, int magic, int speed, int maxMove)
	{
		this.strength = strength;
		this.magic = magic;
		this.speed = speed;
		this.maxMove = maxMove;
		remainingMove = maxMove;
	}
}


