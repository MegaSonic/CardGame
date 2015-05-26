﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent (typeof (Text))]
public class TurnDisplay : MonoBehaviour {

	private Text txt;
	
	private World world;
	private PlayerState ps;


	// Use this for initialization
	void Start () {
		GameObject tmp = GameObject.Find ("World");
		world = ExtensionMethods.GetSafeComponent<World>(tmp);
		
		GameObject tmp2 = GameObject.Find ("PlayerState");
		ps = ExtensionMethods.GetSafeComponent<PlayerState>(tmp2);
		
		txt = GetComponent<Text>();
		
		ClearDisplay ();
	}
	
	// Update is called once per frame
	void Update () {
		DisplayTurn ();
	}

	/// <summary>
	/// Clears the display.
	/// </summary>
	void ClearDisplay()
	{
		txt.text = "";
	}

	/// <summary>
	/// Displays the name of whose turn it is currently.
	/// </summary>
	void DisplayTurn()
	{
		int turnIndex = world.getCurrentTurn ();
		string name = ps.playerList [turnIndex].actorName;

		txt.text = "Current turn: " + name;
	}

}