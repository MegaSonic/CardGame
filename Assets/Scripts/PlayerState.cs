using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerState : Extender {

	public List<PlayerStateInfo> playerList;

	void Start() {
		playerList = new List<PlayerStateInfo> ();
		SetUpTest ();
		//playerList.Add (new PlayerStateInfo ());
		//playerList.Add (new PlayerStateInfo ());
		//playerList.Add (new PlayerStateInfo ());
	}



	public void SetUpTest() {
		PlayerStateInfo player1 = new PlayerStateInfo ();

		// For some reason this gives a null reference exception??
		player1.location.x = 3;
	    player1.location.y = 1;

		player1.health.CurrentHealth = 100;
		player1.health.MaxHealth = 100;

		player1.actorName = "player 1";
		player1.playerType = Player.PlayerType.Warrior;
		player1.playerJob = Player.PlayerJob.Warrior;

		player1.stats.strength = 20;
		player1.stats.magic = 5;
		player1.stats.speed = 10;
		
		playerList.Add (player1);

		PlayerStateInfo player2 = new PlayerStateInfo ();
		player2.location.x = 5;
		player2.location.y = 0;
		
		player2.health.CurrentHealth = 80;
		player2.health.MaxHealth = 80;

		player2.actorName = "player 2";
		player2.playerType = Player.PlayerType.Thief;
		player2.playerJob = Player.PlayerJob.Thief;
		
		player2.stats.strength = 15;
		player2.stats.magic = 5;
		player2.stats.speed = 15;
		
		playerList.Add (player2);

		PlayerStateInfo player3 = new PlayerStateInfo ();
		player3.location.x = 5;
		player3.location.y = 2;
		
		player3.health.CurrentHealth = 80;
		player3.health.MaxHealth = 80;

		player3.actorName = "player 3";
		player3.playerType = Player.PlayerType.Mage;
		player3.playerJob = Player.PlayerJob.Mage;
		
		player3.stats.strength = 5;
		player3.stats.magic = 17;
		player3.stats.speed = 8;
		
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

	public PlayerStateInfo()
	{
		location = new BoardLocation();
		health = new HealthInfo ();
		stats = new StatsInfo ();
	}

}
