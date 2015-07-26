using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Note: give each enum a value. Otherwise, if we decide to rearrange enums or remove them, the inspector value will break.
public enum TargetType
{
    WideSword = 1,
    LongSword = 2,
    Shockwave = 3,
    Cannon = 4,
    AllOpponents = 5,
    AllPlayers = 6,
    AllEnemies = 7,
    AllActors = 8,
    RandomOpponent = 9,
    FirstColumnWithOpponentPanel = 10
}

public enum ManualTarget
{
    Player = 0,
    Ally = 1,
    Enemy = 2,
    Actor = 3,
    Panel = 4,
    DamagedEnemy = 5,
    DamagedAlly = 6
}

/// <summary>
/// A static class that handles all the different targetting methods for various cards.
/// </summary>
public class TargetLookup : Extender {

    public static Battle battle;
    public static Board field;

    void Start()
    {
        battle = GameObject.FindGameObjectWithTag("World").GetComponent<Battle>();
        field = GameObject.FindGameObjectWithTag("Board").GetComponent<Board>();
    }

	// This will return all actors in the area of effect
	// If you want it to "smart cast", aka not hit player units if cast by a player
	// You'll need another check
    public static IEnumerable<BoardLocation> Lookup(TargetType targetType, Actor cardUser)
    {
        int x = cardUser.location.x;
        int y = cardUser.location.y;

        switch (targetType)
        {

            // Hits all units in a 1x3 area towards the opposing area
            case TargetType.WideSword:
                if (cardUser is Player)
                {
                    for (int i = y - 1; i < y + 2; i++)
                    {
                        if (i >= 0 && i <= 2)
                        {
                            yield return new BoardLocation(x - 1, i);
                        }
                    }

                }

                else if (cardUser is Enemy)
                {
                    for (int i = y - 1; i < y + 2; i++)
                    {
                        if (i >= 0 && i <= 2)
                        {
                            yield return new BoardLocation(x + 1, i);
                        }
                    }
                }
                break;

                // Does a shockwave along the row, going through actors hit
            case TargetType.Shockwave:

                if (cardUser is Player)
                {
                    for (int i = x - 1; i >= 0; i--)
                    {
                        StaticCoroutine.DoCoroutine(field.board[i, y].Flash());
                        yield return new BoardLocation(i, y);
                    }
                }
                else if (cardUser is Enemy)
                {
                    for (int i = x + 1; i <= 5; i++)
                    {
                        StaticCoroutine.DoCoroutine(field.board[i, y].Flash());
                        yield return new BoardLocation(i, y);
                    }
                }

                break;

                // Fires a projectile along the row, hitting the first target it finds and stopping.
            case TargetType.Cannon:
                if (cardUser is Player)
                {
                    for (int i = x - 1; i >= 0; i--)
                    {
                        StaticCoroutine.DoCoroutine(field.board[i, y].Flash());
                        yield return new BoardLocation(i, y);
                        if (field.board[i, y].Unit != null)
                        {
                            break;
                        }
                    }
                }
                else if (cardUser is Enemy)
                {
                    for (int i = x + 1; i <= 5; i++)
                    {
                        StaticCoroutine.DoCoroutine(field.board[i, y].Flash());
                        yield return new BoardLocation(i, y);
                        if (field.board[i, y].Unit != null)
                        {
                            break;
                        }
                    }
                }

                break;
            case TargetType.AllOpponents:
                if (cardUser is Player)
                {
                    foreach (Actor a in battle.actorsList)
                    {
                        if (a is Enemy)
                            yield return a.location;
                    }
                }
                else if (cardUser is Enemy)
                {
                    foreach (Actor a in battle.actorsList)
                    {
                        if (a is Player)
                            yield return a.location;
                    }
                }
                break;
            case TargetType.AllEnemies:
                foreach (Actor a in battle.actorsList)
                {
                    if (a is Enemy)
                        yield return a.location;
                }
                break;
            case TargetType.AllPlayers:
                foreach (Actor a in battle.actorsList)
                {
                    if (a is Player)
                        yield return a.location;
                }
                break;
            case TargetType.AllActors:
                foreach (Actor a in battle.actorsList)
                {
                    yield return a.location;
                }
                break;
            case TargetType.RandomOpponent:
                {
                    List<Actor> actorList = new List<Actor>();

                    if (cardUser is Player)
                    {
                        foreach (Actor a in battle.actorsList)
                        {
                            if (a is Enemy)
                                actorList.Add(a);
                        }
                    }
                    else if (cardUser is Enemy)
                    {
                        foreach (Actor a in battle.actorsList)
                        {
                            if (a is Player)
                                actorList.Add(a);
                        }
                    }

                    Actor b = actorList[Random.Range(0, actorList.Count)];
                    StaticCoroutine.DoCoroutine(field.board[b.location.x, b.location.y].Flash());
                    yield return b.location;
                    yield break;
                }
            case TargetType.FirstColumnWithOpponentPanel:
                {
                    bool foundCol = false;
                    if (cardUser is Player)
                    {
                        for (int i = 5; i >= 0; i--)
                        {
                            for (int j = 0; j <= 2; j++)
                            {
                                if (field.board[i, j].Owner == Panel.WhoCanUse.Enemy && !foundCol)
                                {
                                    foundCol = true;
                                    j = -1;
                                }
                                else if (foundCol)
                                {
                                    yield return new BoardLocation(i, j);
                                    if (j == 2) yield break;
                                }
                            }
                        }
                    }
                    else if (cardUser is Enemy)
                    {
                        for (int i = 0; i <= 5; i++)
                        {
                            for (int j = 0; j <= 2; j++)
                            {
                                if (field.board[i, j].Owner == Panel.WhoCanUse.Player && !foundCol)
                                {
                                    foundCol = true;
                                    j = 0;
                                }
                                else if (foundCol)
                                {
                                    yield return new BoardLocation(i, j);
                                    if (j == 2) yield break;
                                }
                            }
                        }
                    }
                }
                break;
        }
    }

    public static IEnumerable<Actor> GetActorsFromLocations(List<BoardLocation> targets)
    {
        foreach (BoardLocation location in targets)
        {
            if (field.board[location.x, location.y].Unit != null)
            {
                yield return field.board[location.x, location.y].Unit;
            }
        }
    }

    public static IEnumerable<Panel> GetPanelsFromLocations(List<BoardLocation> targets)
    {
        foreach (BoardLocation location in targets)
        {
            yield return field.board[location.x, location.y];
        }
    }

    /// <summary>
    /// Takes a list of targets, and filters out a particular group of them, ie. ignore allies or enemies.
    /// Useful for when you don't want friendly fire in your damaging aoe effects!
    /// </summary>
    /// <param name="targets">The list of targets (probably from Lookup)</param>
    /// <param name="ignoreWhich">true to ignore player characters, false to ignore enemies</param>
    /// <returns>A new list of targets without the group you specified</returns>
    public static IEnumerable<Actor> SmartTarget(List<Actor> targets, bool ignoreWhich)
    {

        foreach (Actor a in targets)
        {
            if (ignoreWhich)
            {
                if (a is Enemy) yield return a;
            }
            else
            {
                if (a is Player) yield return a;
            }
        }

    }
}
