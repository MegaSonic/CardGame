using UnityEngine;
using System.Collections;
using System;


/// <summary>
/// A list of Actor events.
/// Each Actor has one of these.
/// Used when an event needs to fire on a specific action that is actor-specific.
/// </summary>
public class ActorEvents : Extender {

	public delegate void ActorEventHandler(Actor sender, EventArgs e);
	
	/// <summary>
	/// Occurs when this actor draws a card.
	/// </summary>
	public event ActorEventHandler DrawCard;
	
	/// <summary>
	/// Occurs when this actor gains mana.
	/// </summary>
	public event ActorEventHandler GainMana;
	
	/// <summary>
	/// Occurs when this actor is damaged.
	/// </summary>
	public event ActorEventHandler IsDamaged;
	
	/// <summary>
	/// Occurs when this actor is healed.
	/// </summary>
	public event ActorEventHandler IsHealed;
	
	/// <summary>
	/// Occurs when this actor is reduced to 0 HP.
	/// </summary>
	public event ActorEventHandler IsKilled;

	/// <summary>
	/// Occurs when this actor begins their turn.
	/// </summary>
	public event ActorEventHandler BeginTurn;

	/// <summary>
	/// Occurs when this actor ends their turn.
	/// </summary>
	public event ActorEventHandler EndTurn;

	/// <summary>
	/// Occurs when this actor heals another actor.
	/// </summary>
	public event ActorEventHandler HealsOther;

	/// <summary>
	/// Occurs when this actor damages another actor.
	/// </summary>
	public event ActorEventHandler DamagesOther;

	/// <summary>
	/// Occurs when this actor moves.
	/// </summary>
	public event ActorEventHandler Moves;

}
