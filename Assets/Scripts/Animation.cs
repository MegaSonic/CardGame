using UnityEngine;
using UnityEditor;
using System.Collections;

public class AnimationClip : ScriptableObject
{
    public string name;
    public int fps;
    public Sprite[] frames;

    public string sequenceCode;
    public string cue;

    public SpriteAnimator.AnimationTrigger[] triggers;
}

#if (UNITY_EDITOR)

public class MakeScriptableObject
{
    [MenuItem("Assets/Create/AnimationClip")]
    public static void CreateSpriteClip()
    {
        AnimationClip clip = ScriptableObject.CreateInstance<AnimationClip>();
 
        AssetDatabase.CreateAsset(clip, "Assets/NewSpriteAnimationClip.asset");
        AssetDatabase.SaveAssets();
 
        EditorUtility.FocusProjectWindow();
 
        Selection.activeObject = clip;
    }
}

#endif
