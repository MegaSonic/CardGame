using UnityEngine;
using System.Collections;
using UnityEditor;

public class Card : Extender {

	/// <summary>
	/// What this card bases its potency off of, i.e. strength or magic.
	/// Enum so this can be expanded later.
	/// </summary>
	public enum DamageSource {
		Physical,
		Magical
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



	/// <summary>
	/// The target ID type.
	/// </summary>
	public TargetLookup.TargetType targetID;

	/// <summary>
	/// Whether or not this card should display its potency.
	/// </summary>
	[HideInInspector]
	public bool displayPotency;
	
	/// <summary>
	/// Info about potency. Serialized so it can be expanded/collapsed in the inspector.
	/// </summary>
	[HideInInspector]
	public PotencyInfo potencyInfo;


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
}

[CustomEditor( typeof(Card) )]
public class CardInspector : Editor
{
	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI ();

		Card card = (Card)target;
		
		card.displayPotency = EditorGUILayout.Toggle("Potency?", card.displayPotency);
		if (card.displayPotency)
		{
			EditorGUILayout.BeginVertical();
			EditorGUILayout.IntField("Potency", card.potencyInfo.potency);
			EditorGUILayout.EnumPopup("Potency Stat", card.potencyInfo.potencyStat);
			EditorGUILayout.EndVertical();
		}
	}
}
