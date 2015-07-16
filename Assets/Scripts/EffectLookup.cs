using UnityEngine;
using System.Collections;

// Note: give each enum a value. Otherwise, if we decide to rearrange enums or remove them, the inspector value will break.
public enum EffectType
{
	DealDamage = 0,
    StrengthBuff = 1,
    MagicBuff = 2,
    SpeedBuff = 3,
    GainMana = 4,
    Heal = 5,
    ChangePanelOwner = 6,
    DrawCards = 7
}

public class EffectLookup : Extender {
    private static Mana mana;
    private static Board board;

    void Start()
    {
        mana = GameObject.FindGameObjectWithTag("World").GetSafeComponent<Mana>();
        board = GameObject.FindGameObjectWithTag("Board").GetSafeComponent<Board>();
    }

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
                {
                    Actor damageDealer = payloads[0] as Actor;
                    Actor damageTaker = payloads[1] as Actor;
                    Card.PotencyInfo potencyInfo = payloads[2] as Card.PotencyInfo;

                    int damage = 0;

                    if (potencyInfo.potencyStat == Card.DamageSource.Physical)
                    {
                        damage = Mathf.FloorToInt(damageDealer.stats.strength * potencyInfo.potency / 100f);
                    }
                    else if (potencyInfo.potencyStat == Card.DamageSource.Magical)
                    {
                        damage = Mathf.FloorToInt(damageDealer.stats.magic * potencyInfo.potency / 100f);
                    }

                    damageTaker.TakeDamage(damage);
                    damageDealer.CallActorEvent(EventName.DamagedOther);
                    damageTaker.CallActorEvent(EventName.WasDamaged);

                    if (damageTaker.health.CurrentHealth < 0)
                    {
                        damageDealer.CallActorEvent(EventName.KilledOther);
                        damageTaker.CallActorEvent(EventName.WasKilled);
                    }
                }
                break;


            case EffectType.GainMana:
                if (payloads.Length > 0)
                {
                    mana.AddMana((int)payloads[0]);
                }
                else
                {
                    mana.AddMana(1);
                }

                break;
                // Actor damageDealer, Actor damageTaker, PotencyInfo potency
            case EffectType.Heal:
                {
                    Actor damageDealer = payloads[0] as Actor;
                    Actor damageTaker = payloads[1] as Actor;
                    Card.PotencyInfo potencyInfo = payloads[2] as Card.PotencyInfo;

                    int damage = 0;

                    if (potencyInfo.potencyStat == Card.DamageSource.Physical)
                    {
                        damage = Mathf.FloorToInt(damageDealer.stats.strength * potencyInfo.potency / 100f);
                    }
                    else if (potencyInfo.potencyStat == Card.DamageSource.Magical)
                    {
                        damage = Mathf.FloorToInt(damageDealer.stats.magic * potencyInfo.potency / 100f);
                    }

                    damageTaker.health.CurrentHealth += damage;

                    damageDealer.CallActorEvent(EventName.HealedOther);
                    damageTaker.CallActorEvent(EventName.WasHealed);
                }
                break;
                // Actor cardPlayer, Panel panelToChange, Panel.WhoCanUser newOwner
            case EffectType.ChangePanelOwner:
                {
                    Actor cardPlayer = payloads[0] as Actor;
                    Panel panelToChange = payloads[1] as Panel;
                    Panel.WhoCanUse newOwner = (Panel.WhoCanUse)payloads[2];

                    if (panelToChange.Unit == null)
                    {
                        panelToChange.Owner = newOwner;
                    }
                    else
                    {
                        Card.PotencyInfo potency = new Card.PotencyInfo();
                        potency.potency = 10;
                        potency.potencyStat = Card.DamageSource.Physical;
                        EffectLookup.Lookup(EffectType.DealDamage, cardPlayer, panelToChange.Unit, potency);
                    }
                }
                break;
            case EffectType.DrawCards:
                {
                    
                }
                break;
        }
    }
}
