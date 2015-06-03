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
		Player player1 = Player.CreatePlayer (3, 1, 100, 100, "player 1", Player.PlayerType.Warrior, Player.PlayerJob.Warrior, 20, 5, 10, 3);
        SetUpPlayer(player1, p1Sprite, 3, 1);
		playerList.Add (player1);

		Player player2 = Player.CreatePlayer (5, 0, 80, 80, "player 2", Player.PlayerType.Thief, Player.PlayerJob.Thief, 15, 5, 15, 4);
        SetUpPlayer(player2, p2Sprite, 5, 0);
		playerList.Add (player2);

		Player player3 = Player.CreatePlayer (5, 2, 80, 80, "player 3", Player.PlayerType.Mage, Player.PlayerJob.Mage, 5, 17, 8, 3);
        SetUpPlayer(player3, p3Sprite, 5, 2);
		playerList.Add (player3);
	}

    /// <summary>
    /// Sets up a player sprite on the Panel x, y
    /// </summary>
    /// <param name="player"></param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    public void SetUpPlayer(Player player, Sprite sprite, int x, int y)
    {
        player.playerSprite.sprite = sprite;
        float tmpX = theBoard.board[x, y].screenLocationX;
        float tmpY = theBoard.board[x, y].screenLocationY;
        player.transform.position = new Vector3(tmpX, tmpY);
        theBoard.board[x, y].Unit = player;
    }
}
