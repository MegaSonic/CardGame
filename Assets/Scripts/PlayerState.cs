using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerState : Extender {

	public List<PlayerStateInfo> playerList;

	void Start() {
		playerList = new List<PlayerStateInfo> ();
		SetUpTest ();
	}



	public void SetUpTest() {
		PlayerStateInfo player1 = new PlayerStateInfo ();
		player1.x = 3;
	    player1.y = 1;

		player1.CurrentHealth = 100;
		player1.MaxHealth = 100;

		player1.playerType = Player.PlayerType.Warrior;
		player1.playerJob = Player.PlayerJob.Warrior;

		player1.strength = 20;
		player1.magic = 5;
		player1.speed = 10;

		playerList.Add (player1);

		PlayerStateInfo player2 = new PlayerStateInfo ();
		player2.x = 5;
		player2.y = 0;
		
		player2.CurrentHealth = 80;
		player2.MaxHealth = 80;
		
		player2.playerType = Player.PlayerType.Thief;
		player2.playerJob = Player.PlayerJob.Thief;
		
		player2.strength = 15;
		player2.magic = 5;
		player2.speed = 15;
		
		playerList.Add (player2);

		PlayerStateInfo player3 = new PlayerStateInfo ();
		player3.x = 5;
		player3.y = 2;
		
		player3.CurrentHealth = 80;
		player3.MaxHealth = 80;
		
		player3.playerType = Player.PlayerType.Mage;
		player3.playerJob = Player.PlayerJob.Mage;
		
		player3.strength = 5;
		player3.magic = 17;
		player3.speed = 8;
		
		playerList.Add (player3);

	}
}

[System.Serializable]
public class PlayerStateInfo
{
	public int x;
	public int y;
	public int CurrentHealth;
	public int MaxHealth;
	public Player.PlayerType playerType;
	public Player.PlayerJob playerJob;
	public int strength;
	public int magic;
	public int speed;
}
