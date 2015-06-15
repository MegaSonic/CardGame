using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Maintains Enemy state information across Battles.
/// </summary>
public class EnemyState : Extender {
	
	public List<Enemy> enemyList;
	
	private Board theBoard;
	
	public Sprite e1Sprite;
	public Sprite e2Sprite;
	public Sprite e3Sprite;
	
	void Start() {
		enemyList = new List<Enemy> ();
		
		GameObject tmp = GameObject.Find ("Board");
		theBoard = ExtensionMethods.GetSafeComponent<Board>(tmp);
		
		SetUpTest ();
		
		e1Sprite = Resources.Load<Sprite> ("enemy1-test");
		e2Sprite = Resources.Load<Sprite> ("enemy2-test");
		e3Sprite = Resources.Load<Sprite> ("enemy3-test");
	}
	
	
	
	public void SetUpTest() {
		Enemy enemy1 = Enemy.CreateEnemy (0, 0, 100, 100, "enemy 1", 20, 5, 10, 3);
		SetUpEnemy(enemy1, e1Sprite, 0, 0);
		enemyList.Add (enemy1);
		
		Enemy enemy2 = Enemy.CreateEnemy (2, 2, 80, 80, "enemy 2", 15, 5, 15, 4);
		SetUpEnemy(enemy2, e2Sprite, 2, 2);
		enemyList.Add (enemy2);
		
		Enemy enemy3 = Enemy.CreateEnemy (0, 2, 80, 80, "enemy 3", 5, 17, 8, 3);
		SetUpEnemy(enemy3, e3Sprite, 0, 2);
		enemyList.Add (enemy3);
	}
	
	/// <summary>
	/// Sets up an enemy sprite on the Panel x, y
	/// </summary>
	/// <param name="enemy"></param>
	/// <param name="x"></param>
	/// <param name="y"></param>
	public void SetUpEnemy(Enemy enemy, Sprite sprite, int x, int y)
	{
		enemy.actorSprite.sprite = sprite;
		float tmpX = theBoard.board[x, y].screenLocationX;
		float tmpY = theBoard.board[x, y].screenLocationY;
		enemy.transform.position = new Vector3(tmpX, tmpY);
		theBoard.board[x, y].Unit = enemy;
	}
}
