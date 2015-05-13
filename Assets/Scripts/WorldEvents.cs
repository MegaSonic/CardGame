using UnityEngine;
using System.Collections;
using System;

public class WorldEvents : Extender {

	public delegate void EventHandler(object sender, EventArgs e);

	/// <summary>
	/// Occurs when any character draws a card.
	/// </summary>
	public event EventHandler DrawCard;

	/// <summary>
	/// Occurs when you gain mana.
	/// </summary>
	public event EventHandler GainMana;

	/// <summary>
	/// Occurs when character damaged.
	/// </summary>
	public event EventHandler CharacterDamaged;

	/// <summary>
	/// Occurs when character healed.
	/// </summary>
	public event EventHandler CharacterHealed;

	/// <summary>
	/// Occurs when any character is reduced to 0 HP.
	/// </summary>
	public event EventHandler CharacterKilled;


}
