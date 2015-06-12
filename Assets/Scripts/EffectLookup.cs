using UnityEngine;
using System.Collections;

// Note: give each enum a value. Otherwise, if we decide to rearrange enums or remove them, the inspector value will break.
public enum EffectType
{
    StrengthBuff = 1,
    MagicBuff = 2,
    SpeedBuff = 3,
    GainMana = 4
}

public static class EffectLookup {
    private static Mana mana = GameObject.FindGameObjectWithTag("World").GetSafeComponent<Mana>();
    private static Board board = GameObject.FindGameObjectWithTag("Board").GetSafeComponent<Board>();


    /// <summary>
    /// Looks up an effect. Payloads lets you send an optional number of objects which Lookup will handle (or not)
    /// </summary>
    /// <param name="effectType"></param>
    /// <param name="payloads"></param>
    public static void Lookup(EffectType effectType, params object[] payloads)
    {
        switch (effectType)
        {
            case EffectType.GainMana:
                if (payloads.Length > 0)
                {
                    mana.AddMana((int) payloads[0]);
                }
                else
                {
                    mana.AddMana(1);
                }

                break;
        }
    }
}
