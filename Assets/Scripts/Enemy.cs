using UnityEngine;
using System.Collections;

public class Enemy : Actor {

	//public SpriteRenderer enemySprite;
	//public static GameObject enemyObj;

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

		//actorObj.transform.parent = GameObject.Find("Enemies").transform;
		actorObj.transform.SetParent(GameObject.Find("Enemies").transform);
		actorObj.name = enemy.actorName;
		
		return enemy;
	}
}
