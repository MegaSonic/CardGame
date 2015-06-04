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

	

    public static void Lookup(EffectType effectType)
    {

    }
}
