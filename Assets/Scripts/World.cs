using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Keeps track of global game rules such as turn order.
/// </summary>
public class World : MonoBehaviour {
	
	private PlayerState ps;
	private EnemyState es;

	/// <summary>
	/// The number of players found in playerlist - used to keep track of changes to the list
	/// </summary>
	private int psState;

	/// <summary>
	/// The turn order, represented as an ordered list of the indices of players in the PlayerState's playerList.
	/// </summary>
	private List<int> turnOrder;

	/// <summary>
	/// The index in turnOrder of who has the current turn.
	/// </summary>
	private int currentTurnIndex;

    /// <summary>
    /// The playing field.
    /// </summary>
    private Board board;

	/// <summary>
	/// Start this instance.
	/// </summary>
	void Start () {
		GameObject tmp = GameObject.Find ("PlayerState");
		ps = ExtensionMethods.GetSafeComponent<PlayerState>(tmp);

		GameObject tmp2 = GameObject.Find ("EnemyState");
		es = ExtensionMethods.GetSafeComponent<EnemyState>(tmp2);

        board = GameObject.FindGameObjectWithTag("Board").GetComponent<Board>();

		determineTurnOrder ();
		currentTurnIndex = 0;
	}

	/// <summary>
	/// Determines the turn order and sets turnOrder.
	/// </summary>
	private void determineTurnOrder()
	{
		if (ps == null) {
			Debug.LogError ("PlayerState hasn't been initialized yet");
			return;
		}

		psState = ps.playerList.Count;

		// Set up a temporary list containing KeyValuePairs (index in playerList, speed)
		List<KeyValuePair<int, int>> temp = new List<KeyValuePair<int, int>> ();

		for (int i=0; i<ps.playerList.Count; i++) {
			int speed = ps.playerList[i].stats.speed;
			temp.Add(new KeyValuePair<int, int>(i, speed));
		}

		// Sort by speed
		List<KeyValuePair<int, int>> tempSorted = temp.OrderByDescending (o => o.Value).ToList ();

		// Set turnOrder
		List<int> result = new List<int>();

		foreach (KeyValuePair<int,int> pair in tempSorted)
			result.Add (pair.Key);

		turnOrder = result;
	}

	/// <summary>
	/// Gets the turn order.
	/// </summary>
	public List<int> getTurnOrder()	{
		return turnOrder;
	}

	/// <summary>
	/// Gets the current actor.
	/// </summary>
	/// <returns>The current actor.</returns>
    public Actor GetCurrentActor()
    {
        return ps.playerList[turnOrder[currentTurnIndex]];
    }

	/// <summary>
	/// Gets the index in the playerList of who has the current turn.
	/// </summary>
	/// <returns>The current turn.</returns>
	public int getCurrentTurn(){
		if (turnOrder.Count == 0)
			return 0;
		return turnOrder[currentTurnIndex];
	}

	/// <summary>
	/// Changes the turn.
	/// </summary>
	public void changeTurns(){
		currentTurnIndex++;

		// loop back around - 
		//  reset remaining moves for each player
		if (currentTurnIndex >= turnOrder.Count) {
			currentTurnIndex = 0;
			foreach (Player p in ps.playerList)			
				p.stats.remainingMove = p.stats.maxMove;
		}
	}

	/// <summary>
	/// Update this instance.
	/// </summary>
	void Update () {	
		// re-initialize if the playerList has changed
		if (ps != null) {
			if (ps.playerList.Count != psState)
				determineTurnOrder();
		}

        if (Input.GetButtonDown("Up"))
        {
            MoveActor(GetCurrentActor(), Direction.Up);
        }
        else if (Input.GetButtonDown("Left"))
        {
            MoveActor(GetCurrentActor(), Direction.Left);
        }
        else if (Input.GetButtonDown("Right"))
        {
            MoveActor(GetCurrentActor(), Direction.Right);
        }
        else if (Input.GetButtonDown("Down"))
        {
            MoveActor(GetCurrentActor(), Direction.Down);
        }
	}

    /// <summary>
    /// Determines whether this panel is adjacent the specified player.
    /// </summary>
    /// <returns><c>true</c> if this instance is adjacent the specified p; otherwise, <c>false</c>.</returns>
    /// <param name="p">Player.</param>
    public static bool ArePanelsAdjacent(Panel p1, Panel p2)
    {
        int diffX = Mathf.Abs(p1.x - p2.x);
        int diffY = Mathf.Abs(p1.y - p2.y);

        if (diffX + diffY != 1)
            return false;
        else
            return true;
    }

    /// <summary>
    /// Attempts to move an actor in the given Direction.
    /// Will swap actors if needed.
    /// Returns true if move was successful.
    /// </summary>
    /// <param name="d">A cardinal direction.</param>
    public bool MoveActor(Actor a, Direction d)
    {
        if (a.stats.remainingMove <= 0) return false;

        switch (d)
        {
            case Direction.Up:
                if (!IsMoveAllowed(a, d)) return false;
                if (CheckForSwap(a, Direction.Up))
                {
                    SwapActors(a, board.board[a.location.x, a.location.y - 1].Unit);
                }
                else
                {
                    board.board[a.location.x, a.location.y].Unit = null;
                    a.location.y -= 1;
                    board.board[a.location.x, a.location.y].Unit = a;
                    a.transform.position = new Vector3(board.board[a.location.x, a.location.y].screenLocationX, board.board[a.location.x, a.location.y].screenLocationY, 0);
                }
                
                break;
            case Direction.Left:
				if (!IsMoveAllowed(a, d)) return false;
                if (CheckForSwap(a, Direction.Left))
                {
                    SwapActors(a, board.board[a.location.x - 1, a.location.y].Unit);
                }
                else
                {
                    board.board[a.location.x, a.location.y].Unit = null;
                    a.location.x -= 1;
                    board.board[a.location.x, a.location.y].Unit = a;
                    a.transform.position = new Vector3(board.board[a.location.x, a.location.y].screenLocationX, board.board[a.location.x, a.location.y].screenLocationY, 0);
                }
                
                break;
            case Direction.Right:
				if (!IsMoveAllowed(a, d)) return false;
                if (CheckForSwap(a, Direction.Right))
                {
                    SwapActors(a, board.board[a.location.x + 1, a.location.y].Unit);
                }
                else
                {
                    board.board[a.location.x, a.location.y].Unit = null;
                    a.location.x += 1;
                    board.board[a.location.x, a.location.y].Unit = a;
                    a.transform.position = new Vector3(board.board[a.location.x, a.location.y].screenLocationX, board.board[a.location.x, a.location.y].screenLocationY, 0);
                }
                
                break;
            case Direction.Down:
				if (!IsMoveAllowed(a, d)) return false;
                if (CheckForSwap(a, Direction.Down))
                {
                    SwapActors(a, board.board[a.location.x, a.location.y + 1].Unit);
                }
                else
                {
                    board.board[a.location.x, a.location.y].Unit = null;
                    a.location.y += 1;
                    board.board[a.location.x, a.location.y].Unit = a;
                    a.transform.position = new Vector3(board.board[a.location.x, a.location.y].screenLocationX, board.board[a.location.x, a.location.y].screenLocationY, 0);
                }
   
                break;
        }
        a.stats.remainingMove -= 1;
        return true;
    }

	/// <summary>
	/// Determines whether the given actor is allowed to move into the panel at given direction.
	/// </summary>
	/// <returns><c>true</c> if the given actor can move in the given direction; otherwise, <c>false</c>.</returns>
	/// <param name="a">The actor.</param>
	/// <param name="d">The cardinal direction.</param>
	public bool IsMoveAllowed(Actor a, Direction d)
	{
		switch (d)
		{
			case Direction.Up:
				if (a.location.y == 0) return false;
			if (board.board[a.location.x, a.location.y - 1].Owner == Panel.WhoCanUse.Player) return true;
				break;
			case Direction.Left:
				if (a.location.x == 0) return false;
			if (board.board[a.location.x - 1, a.location.y].Owner == Panel.WhoCanUse.Player) return true;
				break;
			case Direction.Right:
				if (a.location.x == 5) return false;
			if (board.board[a.location.x + 1, a.location.y].Owner == Panel.WhoCanUse.Player) return true;
				break;
			case Direction.Down:
				if (a.location.y == 2) return false;
			if (board.board[a.location.x, a.location.y + 1].Owner == Panel.WhoCanUse.Player) return true;
				break;
		}
		return false;

	}
		

    /// <summary>
    /// Checks if moving an actor in the given direction will require a swap.
    /// </summary>
    /// <param name="a"></param>
    /// <param name="d"></param>
    /// <returns>Returns true if a swap is required.</returns>
    public bool CheckForSwap(Actor a, Direction d)
    {
        switch (d)
        {
            case Direction.Up:
                if (a.location.y == 0) return false;
                if (board.board[a.location.x, a.location.y - 1].Unit != null) return true;
                break;
            case Direction.Left:
                if (a.location.x == 0) return false;
                if (board.board[a.location.x - 1, a.location.y].Unit != null) return true;
                break;
            case Direction.Right:
                if (a.location.x == 5) return false;
                if (board.board[a.location.x + 1, a.location.y].Unit != null) return true;
                break;
            case Direction.Down:
                if (a.location.y == 2) return false;
                if (board.board[a.location.x, a.location.y + 1].Unit != null) return true;
                break;
        }
        return false;
    }

    public bool SwapActors(Actor a1, Actor a2)
    {
        int tempX = a1.location.x;
        int tempY = a1.location.y;

        a1.location.x = a2.location.x;
        a1.location.y = a2.location.y;

        a2.location.x = tempX;
        a2.location.y = tempY;

        board.board[a1.location.x, a1.location.y].Unit = a1;
        board.board[a2.location.x, a2.location.y].Unit = a2;

        a1.transform.position = new Vector3(board.board[a1.location.x, a1.location.y].screenLocationX, board.board[a1.location.x, a1.location.y].screenLocationY, 0);
        a2.transform.position = new Vector3(board.board[a2.location.x, a2.location.y].screenLocationX, board.board[a2.location.x, a2.location.y].screenLocationY, 0);
        return true;
    }
}


