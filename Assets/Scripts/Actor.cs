using UnityEngine;
using System.Collections;
using UnityEditor;

public class Actor : Extender {
	
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
}


