using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerState : Extender {

	public List<Player> playerList;

	private Board theBoard;

	public Sprite p1Sprite;
	public Sprite p2Sprite;
	public Sprite p3Sprite;

	void Start() {
		playerList = new List<Player> ();

		GameObject tmp = GameObject.Find ("Board");
		theBoard = ExtensionMethods.GetSafeComponent<Board>(tmp);

		SetUpTest ();

		p1Sprite = Resources.Load<Sprite> ("player1-test");
		p2Sprite = Resources.Load<Sprite> ("player2-test");
		p3Sprite = Resources.Load<Sprite> ("player3-test");
	}



	public void SetUpTest() {
		Player player1 = Player.CreatePlayer (3, 1, 100, 100, "player 1", Player.PlayerType.Warrior, Player.PlayerJob.Warrior, 20, 5, 10);

		// set up sprite and initialize location -- we should have a function for this
		player1.playerScreenObj.GetComponent<SpriteRenderer>().sprite = p1Sprite;
		float tmpX = theBoard.board [3, 1].screenLocationX;
		float tmpY = theBoard.board [3, 1].screenLocationY;
		player1.playerScreenObj.transform.position = new Vector3 (tmpX, tmpY, 0);
		theBoard.board [3, 1].Unit = player1;

		playerList.Add (player1);

		Player player2 = Player.CreatePlayer (5, 0, 80, 80, "player 2", Player.PlayerType.Thief, Player.PlayerJob.Thief, 15, 5, 15);

		player2.playerScreenObj.GetComponent<SpriteRenderer>().sprite = p2Sprite;
		tmpX = theBoard.board [5, 0].screenLocationX;
		tmpY = theBoard.board [5, 0].screenLocationY;
		player2.playerScreenObj.transform.position = new Vector3 (tmpX, tmpY, 0);
		theBoard.board [5, 0].Unit = player2;
		
		playerList.Add (player2);

		Player player3 = Player.CreatePlayer (5, 2, 80, 80, "player 3", Player.PlayerType.Mage, Player.PlayerJob.Mage, 5, 17, 8);

		player3.playerScreenObj.GetComponent<SpriteRenderer>().sprite = p3Sprite;
		tmpX = theBoard.board [5, 2].screenLocationX;
		tmpY = theBoard.board [5, 2].screenLocationY;
		player3.playerScreenObj.transform.position = new Vector3 (tmpX, tmpY, 0);
		theBoard.board [5, 2].Unit = player3;
		
		playerList.Add (player3);
	}
}
