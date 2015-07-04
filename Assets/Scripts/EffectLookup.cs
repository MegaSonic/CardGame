using UnityEngine;
using System.Collections;

// Note: give each enum a value. Otherwise, if we decide to rearrange enums or remove them, the inspector value will break.
public enum EffectType
{
	DealDamage = 0,
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
			// (Actor damageDealer, Actor damageTaker, PotencyInfo potencyInfo)
		case EffectType.DealDamage:
			Actor damageDealer = payloads[0] as Actor;
			Actor damageTaker = payloads[1] as Actor;
			Card.PotencyInfo potencyInfo = payloads[2] as Card.PotencyInfo;

			int damage = 0;

			if (potencyInfo.potencyStat == Card.DamageSource.Physical) {
				damage = Mathf.FloorToInt( damageDealer.stats.strength * potencyInfo.potency / 100f);
			}
			else if (potencyInfo.potencyStat == Card.DamageSource.Magical) {
				damage = Mathf.FloorToInt( damageDealer.stats.magic * potencyInfo.potency / 100f);
			}

			damageTaker.TakeDamage(damage);
			damageDealer.CallActorEvent(EventName.DamagedOther);
			damageTaker.CallActorEvent(EventName.WasDamaged);

			if (damageTaker.health.CurrentHealth < 0) {
				damageDealer.CallActorEvent(EventName.KilledOther);
				damageTaker.CallActorEvent(EventName.WasKilled);
			}

			break;


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
