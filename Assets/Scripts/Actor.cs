using UnityEngine;
using System.Collections;
using UnityEditor;

[System.Serializable]
public class Actor : Extender  {
	public HealthInfo health;
	public StatsInfo stats;
	public BoardLocation boardLocation;
	public string actorName;
	public ActorEvents events;

	public void EndTurn(Actor sender) {
		Debug.Log ("Clicked?");
	}

	void Start() {
		events = this.gameObject.GetComponent<ActorEvents> ();

		events.EndTurn += new ActorEvents.ActorEventHandler (EndTurn);

	}
}

[System.Serializable]
public class BoardLocation {
	public int x;
	public int y;
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
}

[System.Serializable]
public class StatsInfo {
	[Range(1, 99)]
	public int strength;

	[Range(1, 99)]
	public int magic;

	[Range(1, 99)]
	public int speed;

	public int move;
}


