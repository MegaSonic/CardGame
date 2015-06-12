using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;
using System;

public enum Direction {
    Up = 0,
    Left = 1,
    Right = 2,
    Down = 3
}

[System.Serializable]
public class Actor : Extender  {
	public HealthInfo health;
	public StatsInfo stats;
	public BoardLocation location;
	public string actorName;
	public SpriteRenderer actorSprite;
	public static GameObject actorObj;


	private World world;
	private TextMesh healthDisplay;

    private Dictionary<EventName, ActorEvent> actorEventList = new Dictionary<EventName,ActorEvent>();


	public void CallEndTurnEvent() {
        CallActorEvent(EventName.EndedTurn);
	}

	void Start() {
		// events = this.gameObject.GetComponent<ActorEvents> ();

		GameObject tmp = GameObject.Find ("World");
		world = ExtensionMethods.GetSafeComponent<World>(tmp);
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
    
	private bool InitializeHealthDisplay()
	{
		healthDisplay = GetComponentInChildren<TextMesh>();

		if (healthDisplay == null)
			return false;
		else
			return true;
	}
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
	public StatsInfo(int strength, int magic, int speed, int maxMove)
	{
		this.strength = strength;
		this.magic = magic;
		this.speed = speed;
		this.maxMove = maxMove;
		remainingMove = maxMove;
	}
}


