using UnityEngine;
using System.Collections;

public class Enemy : Actor {

	public SpriteRenderer enemySprite;
	public static GameObject enemyObj;

	public static Enemy CreateEnemy(int locX, int locY, int curHealth, int maxHealth, string eName, 
	                                int strength, int magic, int speed, int maxMove)
	{
		enemyObj = Instantiate(Resources.Load("Enemy Object")) as GameObject;
		Enemy enemy = enemyObj.GetSafeComponent<Enemy>();
		
		enemy.location = new BoardLocation(locX, locY);
		enemy.health = new HealthInfo (curHealth, maxHealth);
		enemy.actorName = eName;
		enemy.stats = new StatsInfo (strength, magic, speed, maxMove);
		
		enemy.enemySprite = enemyObj.GetComponent<SpriteRenderer>();

		enemyObj.transform.parent = GameObject.Find("Enemies").transform;
		enemyObj.name = enemy.actorName;
		
		return enemy;
	}
}
