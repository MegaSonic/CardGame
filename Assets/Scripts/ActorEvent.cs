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
    public List<EffectType> eventList;

    /// <summary>
    /// Creates a new Actor Event.
    /// </summary>
    public ActorEvent()
    {

        eventList = new List<EffectType>();
    }

    /// <summary>
    /// Calls all lookup effects in this event
    /// </summary>
    public void CallEvent()
    {
        foreach (EffectType e in eventList)
        {
            EffectLookup.Lookup(e);
        }
    }

    /// <summary>
    /// Overloaded operator to be able to add "methods" to an event the same way regular events would be able to
    /// </summary>
    /// <param name="a"></param>
    /// <param name="effect"></param>
    public static ActorEvent operator +(ActorEvent a, EffectType effect)
    {
        if (effect != null)
            a.eventList.Add(effect);

        return a;
    }
}
