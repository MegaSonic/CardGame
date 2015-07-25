using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

public class StatusEffect : Extender {

    public enum StatusType
    {
        Status = 0,
        Buff = 1,
        Debuff = 2
    }

    public string statusName;
    public StatusType statusType;

    public bool isTemporary;
    public int maxTurns;
    public int currentTurn = 0;

    public bool hasStacks;
    public int stacks;
    public int maxStacks;

    public bool visible;

    public List<StatusAction> statusActions;

    public EffectType onApply;
    public EffectType onEnd;

    public Sprite graphic;


    

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    [System.Serializable]
    public class StatusAction {
        public EventName eventType;
        public EffectType effect;
        public object[] extraParams;
    }



}

[CustomEditor( typeof(StatusEffect) )]
public class StatusEditor : Editor
{
    public override void OnInspectorGUI()
	{
        StatusEffect effect = (StatusEffect)target;

        effect.statusName = EditorGUILayout.TextField("Status Name", effect.statusName);

        EditorGUILayout.BeginVertical();
        effect.statusType = (StatusEffect.StatusType) EditorGUILayout.EnumPopup("Status Type", effect.statusType);

        effect.isTemporary = EditorGUILayout.Toggle("Temporary buff?", effect.isTemporary);
        if (effect.isTemporary)
        {
            effect.maxTurns = EditorGUILayout.IntField("Number of turns", effect.maxTurns);
        }
        else
        {
            EditorGUILayout.LabelField("This status will not fall off.");
        }

        effect.hasStacks = EditorGUILayout.Toggle("Has Stacks?", effect.hasStacks);
        if (effect.hasStacks)
        {
            effect.maxStacks = EditorGUILayout.IntField("Max Stacks", effect.maxStacks);
        }

        effect.visible = EditorGUILayout.Toggle("Visible in game?", effect.visible);
        if (effect.visible)
        {
            effect.graphic = (Sprite) EditorGUILayout.ObjectField("Sprite", effect.graphic, typeof(Sprite));
        }

        effect.onApply = (EffectType) EditorGUILayout.EnumPopup("Apply Effect", effect.onApply);
        effect.onEnd = (EffectType) EditorGUILayout.EnumPopup("End Effect", effect.onEnd);

        SerializedProperty actions = serializedObject.FindProperty("statusActions");
        EditorGUI.BeginChangeCheck();
        EditorGUILayout.PropertyField(actions, true);
        if (EditorGUI.EndChangeCheck())
            serializedObject.ApplyModifiedProperties();

        EditorGUILayout.EndVertical();
    }
}
