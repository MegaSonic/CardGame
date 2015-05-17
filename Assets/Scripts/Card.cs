using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;

public class Card : Extender {

	/// <summary>
	/// What this card bases its potency off of, i.e. strength or magic.
	/// Enum so this can be expanded later.
	/// </summary>
	public enum DamageSource {
		Physical,
		Magical
	}

	public enum Keyword
	{
		Taunt = 1,
		Shroud = 2,
		Overload = 3

	}

	/// <summary>
	/// The card ID. Might be useful when building decks.
	/// </summary>
	public int cardID;

	/// <summary>
	/// The name of the card.
	/// </summary>
	public string cardName;

	/// <summary>
	/// The mana cost.
	/// </summary>
	public int manaCost;

	public List<Keyword> cardKeywords;
	
	/// <summary>
	/// List of Card Actions that the Card does, in order.
	/// </summary>
	public List<CardAction> cardActions;



	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	[System.Serializable]
	public class PotencyInfo {
		
		/// <summary>
		/// The potency of this card. Should probably be a multiple of 10.
		/// </summary>
		public int potency;
		
		/// <summary>
		/// What stat is used to calculate damage?
		/// </summary>
		public DamageSource potencyStat;


	}

	[System.Serializable]
	public class CardAction {

		/// <summary>
		/// The target ID type.
		/// </summary>
		public TargetLookup.TargetType targetID;

		/// <summary>
		/// Whether or not this card should use a damage calculation in its attack.
		/// </summary>
		public bool usePotency;

		/// <summary>
		/// Info about potency. Serialized so it can be expanded/collapsed in the inspector.
		/// </summary>
		public PotencyInfo potencyInfo;

		/// <summary>
		/// Should this action also apply an effect to the targets?
		/// </summary>
		public EffectLookup.EffectType effectID;


	}
}

/*
[CustomEditor( typeof(Card) )]
public class CardInspector : Editor
{
	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI ();

		Card card = (Card)target;

		foreach (Card.CardAction c in card.cardActions) {
			c.displayPotency = EditorGUILayout.Toggle ("Potency?", c.displayPotency);
			if (c.displayPotency) {
				EditorGUILayout.BeginVertical ();
				EditorGUILayout.IntField ("Potency", c.potencyInfo.potency);
				EditorGUILayout.EnumPopup ("Potency Stat", c.potencyInfo.potencyStat);
				EditorGUILayout.EndVertical ();
			}
		}
	}
}
*/