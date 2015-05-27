using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerState : Extender {

	public List<PlayerStateInfo> playerList;

	private Board theBoard;

	public Sprite p1Sprite;
	public Sprite p2Sprite;
	public Sprite p3Sprite;

	void Start() {
		playerList = new List<PlayerStateInfo> ();

		GameObject tmp = GameObject.Find ("Board");
		theBoard = ExtensionMethods.GetSafeComponent<Board>(tmp);

		SetUpTest ();

		p1Sprite = Resources.Load<Sprite> ("player1-test");
		p2Sprite = Resources.Load<Sprite> ("player2-test");
		p3Sprite = Resources.Load<Sprite> ("player3-test");
	}



	public void SetUpTest() {
		PlayerStateInfo player1 = new PlayerStateInfo (3, 1, 100, 100, "player 1", Player.PlayerType.Warrior, Player.PlayerJob.Warrior, 20, 5, 10);

		player1.gameOb.GetComponent<SpriteRenderer>().sprite = p1Sprite;
		float tmpX = theBoard.board [3, 1].screenLocationX;
		float tmpY = theBoard.board [3, 1].screenLocationY;
		player1.gameOb.transform.position = new Vector3 (tmpX, tmpY, 0);

		playerList.Add (player1);

		PlayerStateInfo player2 = new PlayerStateInfo (5, 0, 80, 80, "player 2", Player.PlayerType.Thief, Player.PlayerJob.Thief, 15, 5, 15);

		player2.gameOb.GetComponent<SpriteRenderer>().sprite = p2Sprite;
		tmpX = theBoard.board [5, 0].screenLocationX;
		tmpY = theBoard.board [5, 0].screenLocationY;
		player2.gameOb.transform.position = new Vector3 (tmpX, tmpY, 0);
		
		playerList.Add (player2);

		PlayerStateInfo player3 = new PlayerStateInfo (5, 2, 80, 80, "player 3", Player.PlayerType.Mage, Player.PlayerJob.Mage, 5, 17, 8);

		player3.gameOb.GetComponent<SpriteRenderer>().sprite = p3Sprite;
		tmpX = theBoard.board [5, 2].screenLocationX;
		tmpY = theBoard.board [5, 2].screenLocationY;
		player3.gameOb.transform.position = new Vector3 (tmpX, tmpY, 0);
		
		playerList.Add (player3);

	}
}

[System.Serializable]
public class PlayerStateInfo
{
	public Player.PlayerType playerType;
	public Player.PlayerJob playerJob;
	public BoardLocation location;
	public HealthInfo health;
	public StatsInfo stats;
	public string actorName;
	public GameObject gameOb;

	public PlayerStateInfo(int locX, int locY, int curHealth, int maxHealth, string pName, Player.PlayerType type, 
	                       Player.PlayerJob job, int strength, int magic, int speed)
	{
		location = new BoardLocation();
		location.x = locX;
		location.y = locY;

		health = new HealthInfo (curHealth, maxHealth);
		actorName = pName;
		playerType = type;
		playerJob = job;
		stats = new StatsInfo (strength, magic, speed);

		gameOb = new GameObject ();
		gameOb.AddComponent<SpriteRenderer> ();
		gameOb.transform.localScale = new Vector3 (0.3f, 0.3f, 0.3f);

	}

}
