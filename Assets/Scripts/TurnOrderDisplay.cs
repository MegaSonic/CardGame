using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

[RequireComponent (typeof (Text))]
public class TurnOrderDisplay : MonoBehaviour {

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
		DisplayTurnOrder ();
	}

	/// <summary>
	/// Clears the display.
	/// </summary>
	void ClearDisplay()
	{
		txt.text = "";
	}

	/// <summary>
	/// Displays the turn order.
	/// </summary>
	void DisplayTurnOrder()
	{
		txt.text = "Turn order: | ";
		foreach (int i in world.getTurnOrder()){
			txt.text += ps.playerList[i].actorName + " | ";
		}
	}

}
