using UnityEngine;
using System.Collections;

public class Card : Extender {

	/// <summary>
	/// The name of the card.
	/// </summary>
	public string cardName;

	/// <summary>
	/// The mana cost.
	/// </summary>
	public int manaCost;

	/// <summary>
	/// The card ID. Probably can be used when building decks.
	/// </summary>
	public int cardID;

	/// <summary>
	/// The target ID type.
	/// </summary>
	public TargetLookup.TargetType targetID;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
