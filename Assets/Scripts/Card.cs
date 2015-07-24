using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;
using UnityEngine.UI;

public enum Variable
{
}

public class Card : Extender {

    private TargetLookup targetLookup;
    private EffectLookup effectLookup;
    private Battle battle;

    void Start()
    {
        battle = GameObject.FindGameObjectWithTag("World").GetSafeComponent<Battle>();
        targetLookup = GameObject.FindGameObjectWithTag("World").GetSafeComponent<TargetLookup>();
        effectLookup = GameObject.FindGameObjectWithTag("World").GetSafeComponent<EffectLookup>();
    }

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

    public PlayerType characterRestriction;
    public PlayerJob jobRestriction;

	/// <summary>
	/// The name of the card.
	/// </summary>
	public string cardName;

	/// <summary>
	/// The regular mana cost of the card, unmodified by effects.
	/// </summary>
	public int manaCost;
    
    /// <summary>
    /// The current mana cost of the card.
    /// </summary>
    [HideInInspector]
    public int currentManaCost;

	/// <summary>
	/// The rules text on this card.
	/// </summary>
	public string cardText;

	/// <summary>
	/// A list of what keywords this card uses.
	/// Note: Will have to write custom inspectors for keywords that need extra rules.
	/// </summary>
	public List<Keyword> cardKeywords;
	
	/// <summary>
	/// List of Card Actions that the Card does, in order.
	/// </summary>
	public List<CardAction> cardActions;

	/// <summary>
	/// A hidden object that can be used to store a list of targets from one action to another.
	/// </summary>
	[HideInInspector]
	public List<Actor> storeTargets;

    public bool requireManualTarget;
    public bool additionalCost;

    [HideInInspector]
    public ManualTarget manualTarget;
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Play()
    {
        if (battle == null)
        {
            battle = GameObject.FindGameObjectWithTag("World").GetSafeComponent<Battle>();
        }

		// iterate through actions and keywords
		foreach (CardAction c in cardActions) {
            switch (c.effectID)
            {
                case EffectType.DealDamage:
                    {
                        List<BoardLocation> locations = new List<BoardLocation>();
                        foreach (BoardLocation location in TargetLookup.Lookup(c.targetID, battle.GetCurrentActor()))
                        {
                            locations.Add(location);
                            foreach (Actor a in TargetLookup.GetActorsFromLocations(locations))
                            {
                                EffectLookup.Lookup(c.effectID, battle.GetCurrentActor(), a, c.potencyInfo);
                            }
                        }
                        
                    }
                    break;
                case EffectType.ChangePanelOwner:
                    {
                        List<BoardLocation> locations = new List<BoardLocation>();
                        foreach (BoardLocation location in TargetLookup.Lookup(c.targetID, battle.GetCurrentActor()))
                        {
                            locations.Add(location);
                            foreach (Panel a in TargetLookup.GetPanelsFromLocations(locations))
                            {
                                EffectLookup.Lookup(c.effectID, battle.GetCurrentActor(), a, Panel.WhoCanUse.Player);
                            }
                        }
                    }
                    break;
            }
		}
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
		/// Should this action use the actors in storeTargets instead of recalculating who to target?
		/// Note: if this is true, targetID is unused.
		/// </summary>
		public bool useStoreTargets;

		/// <summary>
		/// Should this action store its targets for later?
		/// </summary>
		public bool storeTargets;

		/// <summary>
		/// The target ID type.
		/// </summary>
		public TargetType targetID;

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
		public EffectType effectID;


	}
}


[CustomEditor( typeof(Card) )]
public class CardInspector : Editor
{
	public override void OnInspectorGUI()
	{
        Card card = (Card) target;

        card.cardName = EditorGUILayout.TextField("Card Name", card.cardName);
        card.manaCost = EditorGUILayout.IntField("Mana Cost", card.manaCost);
        EditorGUILayout.Space();

        card.cardID = EditorGUILayout.IntField("Card ID", card.cardID);
        card.characterRestriction = (PlayerType) EditorGUILayout.EnumPopup("Char Restriction", card.characterRestriction);
        card.jobRestriction = (PlayerJob) EditorGUILayout.EnumPopup("Job Restriction", card.jobRestriction);
        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Card Text");
        card.cardText = EditorGUILayout.TextField(card.cardText, GUILayout.MaxHeight(75));

        EditorGUILayout.Space();

        card.requireManualTarget = EditorGUILayout.Toggle("Require Target?", card.requireManualTarget);
        if (card.requireManualTarget)
        {
            EditorGUILayout.BeginVertical();
            card.manualTarget = (ManualTarget) EditorGUILayout.EnumPopup(card.manualTarget);
            EditorGUILayout.EndVertical();
        }
        
        card.additionalCost = EditorGUILayout.Toggle("Additional Cost?", card.additionalCost);
        if (card.additionalCost)
        {
            EditorGUILayout.BeginVertical();
            EditorGUILayout.LabelField("PLACEHOLDER");
            EditorGUILayout.EndVertical();
        }

        EditorGUILayout.Space();

        SerializedProperty actions = serializedObject.FindProperty("cardActions");
        EditorGUI.BeginChangeCheck();
        EditorGUILayout.PropertyField(actions, true);
        if (EditorGUI.EndChangeCheck())
            serializedObject.ApplyModifiedProperties();
	}
}
