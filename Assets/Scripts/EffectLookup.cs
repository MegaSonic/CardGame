using UnityEngine;
using System.Collections;

// Note: give each enum a value. Otherwise, if we decide to rearrange enums or remove them, the inspector value will break.
public enum EffectType
{
    StrengthBuff = 1,
    MagicBuff = 2,
    SpeedBuff = 3
}

public static class EffectLookup {

	
    /// <summary>
    /// Looks up an effect. Payloads lets you send an optional number of objects which Lookup will handle (or not)
    /// </summary>
    /// <param name="effectType"></param>
    /// <param name="payloads"></param>
    public static void Lookup(EffectType effectType, params object[] payloads)
    {

    }
}
