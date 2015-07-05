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
    RandomEnemy = 5
}

/// <summary>
/// A static class that handles all the different targetting methods for various cards.
/// </summary>
public class TargetLookup : Extender {

    public Battle battle;
    public Board field;

    void Start()
    {
        battle = GameObject.FindGameObjectWithTag("World").GetComponent<Battle>();
        field = GameObject.FindGameObjectWithTag("Board").GetComponent<Board>();
    }

	// This will return all actors in the area of effect
	// If you want it to "smart cast", aka not hit player units if cast by a player
	// You'll need another check
    public static IEnumerable<Actor> Lookup(TargetType targetType, Board field, Actor cardUser)
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
                        if (i != -1 && i != 3 && field.board[x - 1, i].Unit != null)
                        {
                            yield return field.board[x - 1, i].Unit;
                        }
                    }

                }

                else if (cardUser is Enemy)
                {
                    for (int i = y - 1; i < y + 2; i++)
                    {
                        if (i != -1 && i != 3 && field.board[x + 1, i].Unit != null)
                        {
                            yield return field.board[x + 1, i].Unit;
                        }
                    }
                }
                break;

                // Does a shockwave along the row, going through actors hit
            case TargetType.Shockwave:

                if (cardUser is Player)
                {
                    for (int i = x; i >= 0; i--)
                    {
                        yield return field.board[i, y].Unit;
                    }
                }
                else if (cardUser is Enemy)
                {
                    for (int i = x; i <= 5; i++)
                    {
                        yield return field.board[i, y].Unit;
                    }
                }

                break;

                // Fires a projectile along the row, hitting the first target it finds and stopping.
            case TargetType.Cannon:
                if (cardUser is Player)
                {
                    for (int i = x; i >= 0; i--)
                    {
                        if (field.board[i, y].Unit != null)
                        {
                            yield return field.board[i, y].Unit;
                            yield break;
                        }
                    }
                }
                else if (cardUser is Enemy)
                {
                    for (int i = x; i <= 5; i++)
                    {
                        if (field.board[i, y].Unit != null)
                        {
                            yield return field.board[i, y].Unit;
                            yield break;
                        }
                    }
                }

                break;
            case TargetType.RandomEnemy:
                if (cardUser is Player)
                {
                    
                }
                else if (cardUser is Enemy)
                {

                }
                break;
        }
    }
}
