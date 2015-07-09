using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// List of event names. Any new events should be created here!
/// </summary>
public enum EventName
{
    Test = 0,
    WasHealed = 1,
    WasDamaged = 2,
    Moved = 3,
    EndedTurn = 4,
    GainedMana = 5,
    BeganTurn = 6,
    HealedOther = 7,
    DamagedOther = 8,
    WasKilled = 9,
    KilledOther = 10
}

/// <summary>
/// First crack at a custom event class for Actors to use. See Actor as well.
/// </summary>
public class ActorEvent {

    /// <summary>
    /// The list of events that are subscribed to this.
    /// </summary>
    public List<CustomEvent> eventList;

    /// <summary>
    /// Creates a new Actor Event.
    /// </summary>
    public ActorEvent()
    {

        eventList = new List<CustomEvent>();
    }

    /// <summary>
    /// Calls all lookup effects in this event
    /// </summary>
    public void CallEvent()
    {
        foreach (CustomEvent e in eventList)
        {
            e.CallEvent();
        }
    }

    public void AddEvent(EffectType effect, params object[] p)
    {
            eventList.Add(new CustomEvent(effect, p));
    }


    public class CustomEvent
    {
        EffectType effectType;
        object[] payload;

        public CustomEvent(EffectType e, params object[] p)
        {
            effectType = e;
            payload = p;
        }

        public void CallEvent()
        {
            EffectLookup.Lookup(effectType, payload);
        }
    }
}
