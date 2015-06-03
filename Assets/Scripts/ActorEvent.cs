using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// First crack at a custom event class for Actors to use. See Actor as well.
/// </summary>
public class ActorEvent {

    /// <summary>
    /// Any new types of event should go here.
    /// </summary>
    public enum EventType
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
    /// The type of event.
    /// </summary>
    public EventType eventType;

    /// <summary>
    /// The list of events that are subscribed to this.
    /// </summary>
    public List<EffectLookup.EffectType> eventList;

    /// <summary>
    /// Creates a new Actor Event.
    /// </summary>
    /// <param name="typeOfEvent"></param>
    public ActorEvent(EventType typeOfEvent)
    {
        eventType = typeOfEvent;
        eventList = new List<EffectLookup.EffectType>();
    }

    /// <summary>
    /// Calls all lookup effects in this event
    /// </summary>
    public void CallEvent()
    {
        foreach (EffectLookup.EffectType e in eventList)
        {
            EffectLookup.Lookup(e);
        }
    }

    /// <summary>
    /// Overloaded operator to be able to add "methods" to an event the same way regular events would be able to
    /// </summary>
    /// <param name="a"></param>
    /// <param name="effect"></param>
    public static ActorEvent operator +(ActorEvent a, EffectLookup.EffectType effect)
    {
        if (effect != null)
            a.eventList.Add(effect);

        return a;
    }
}
