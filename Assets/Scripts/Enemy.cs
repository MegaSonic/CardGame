using UnityEngine;
using System.Collections;

public class Enemy : Actor {

	/// <summary>
	/// Creates the enemy.
	/// </summary>
	/// <returns>The enemy.</returns>
	/// <param name="locX">Location x.</param>
	/// <param name="locY">Location y.</param>
	/// <param name="curHealth">Current health.</param>
	/// <param name="maxHealth">Max health.</param>
	/// <param name="eName">E name.</param>
	/// <param name="strength">Strength.</param>
	/// <param name="magic">Magic.</param>
	/// <param name="speed">Speed.</param>
	/// <param name="maxMove">Max move.</param>
	public static Enemy CreateEnemy(int locX, int locY, int curHealth, int maxHealth, string eName, 
	                                int strength, int magic, int speed, int maxMove)
	{
		actorObj = Instantiate(Resources.Load("Enemy Object")) as GameObject;
		Enemy enemy = actorObj.GetSafeComponent<Enemy>();
		
		enemy.location = new BoardLocation(locX, locY);
		enemy.health = new HealthInfo (curHealth, maxHealth);
		enemy.actorName = eName;
		enemy.stats = new StatsInfo (strength, magic, speed, maxMove);
		
		enemy.actorSprite = actorObj.GetComponent<SpriteRenderer>();

		actorObj.transform.SetParent(GameObject.Find("Enemies").transform);
		actorObj.name = enemy.actorName;
		
		return enemy;
	}
}
