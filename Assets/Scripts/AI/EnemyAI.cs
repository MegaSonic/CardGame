using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum AIStyle
{
    Mettaur = 0,
    Cannondumb = 1,
    Teleport = 2
}


public class EnemyAI : Extender {

    public AIStyle aiStyle;
    public List<Card> usableCards;
    public int movesPerTurn = 1;

    [HideInInspector]
    public Card nextCardToUse;
    


    private int round = 1;
    private bool[] flags;

    private Enemy enemy;
    private Board board;
    private Battle battle;

	// Use this for initialization
	void Start () {
        enemy = this.gameObject.GetSafeComponent<Enemy>();
        board = GameObject.FindGameObjectWithTag("Board").GetSafeComponent<Board>();
        flags = new bool[10];
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    /// <summary>
    /// Returns where this enemy's AIStyle decides it should move
    /// </summary>
    /// <returns>A board location with its new location</returns>
    public BoardLocation RunAIRoutine()
    {
        BoardLocation newLocation = new BoardLocation(enemy.location.x, enemy.location.y);

        switch (aiStyle)
        {
                // Stays in column, follows closest enemy. Two cards, Fight and Shockwave
            case AIStyle.Mettaur:

                bool dontMove = false;
                for (int i = enemy.location.x + 1; i <= 5; i++) 
                {
                    // If there's an enemy in the same row, don't move
                    if (board.board[i, enemy.location.y].Unit != null && board.board[i, enemy.location.y].Unit is Player)
                    {
                        dontMove = true;
                        break;
                    }
                }
                    // Otherwise, find the enemy in the highest row and move closer to it
                if (!dontMove)
                {
                    for (int i = enemy.location.x + 1; i <= 5; i++)
                    {
                        for (int j = 0; j <= 2; j++)
                        {
                            if (board.board[i, j].Unit != null && board.board[i, j].Unit is Player)
                            {
                                if (enemy.location.y > j)
                                {
                                    newLocation.y--;
                                    break;
                                }
                                else if (enemy.location.y < j)
                                {
                                    newLocation.y++;
                                    break;
                                }
                            }
                        }
                    }
                }
                
                if (usableCards.Count != 0)
                    nextCardToUse = usableCards[Random.Range(0, usableCards.Count)];
                return newLocation;


            case AIStyle.Teleport:
                List<Panel> movablePanels = new List<Panel>();

                foreach (Panel p in board.board)
                {
                    if (p.Owner == Panel.WhoCanUse.Enemy && p.Unit == null)
                    {
                        movablePanels.Add(p);
                    }
                }
                Panel randomPanel = movablePanels[Random.Range(0, movablePanels.Count - 1)];
                newLocation.x = randomPanel.x;
                newLocation.y = randomPanel.y;
                break;


            // Doesn't move, one card, Cannon
            case AIStyle.Cannondumb:
                nextCardToUse = usableCards[Random.Range(0, usableCards.Count)];
                break;
        }

        Debug.Log(newLocation.x + ", " + newLocation.y);
        return newLocation;
    }
}
