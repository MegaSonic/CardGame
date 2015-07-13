using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Keeps track of global game rules such as turn order.
/// </summary>
public class Battle : MonoBehaviour {

	/// <summary>
	/// The PlayerState - Should be loaded in at the beginning and saved to at the end.
	/// </summary>
	private PlayerState ps;
	private EnemyState es;

	/// <summary>
	/// The number of players found in playerlist - used to keep track of changes to the list
	/// </summary>
	private int psState;

	/// <summary>
	/// The number of enemies found in enemylist - used to keep track of changes to the list
	/// </summary>
	private int esState;

	/// <summary>
	/// The players and enemies participating in this battle, in turn order.
	/// </summary>
	public List<Actor> actorsList;

	/// <summary>
	/// The index in actorsList of who has the current turn.
	/// </summary>
	private int currentTurnIndex;

    /// <summary>
    /// The playing field.
    /// </summary>
    private Board board;

	/// <summary>
	/// The player's mana.
	/// </summary>
	private Mana mana;

	/// <summary>
	/// The player's hand of cards.
	/// </summary>
	private Hand hand;

	/// <summary>
	/// The collection of discarded cards.
	/// </summary>
	private List<GameObject> graveyard;

	/// <summary>
	/// Start this instance.
	/// </summary>
	void Start () {
		GameObject tmp = GameObject.Find ("PlayerState");
		ps = ExtensionMethods.GetSafeComponent<PlayerState>(tmp);

		GameObject tmp2 = GameObject.Find ("EnemyState");
		es = ExtensionMethods.GetSafeComponent<EnemyState>(tmp2);

		GameObject tmp3 = GameObject.Find ("Hand Canvas");
		hand = ExtensionMethods.GetSafeComponent<Hand> (tmp3);

        board = GameObject.FindGameObjectWithTag("Board").GetComponent<Board>();

		mana = GetComponent<Mana> ();

		graveyard = new List<GameObject> ();

		determineTurnOrder ();
		currentTurnIndex = 0;
	}

	/// <summary>
	/// Determines the turn order and sets up the ActorsList
	/// </summary>
	private void determineTurnOrder()
	{
		if (ps == null) {
			Debug.LogError ("PlayerState hasn't been initialized yet");
			return;
		}
		if (es == null) {
			Debug.LogError ("EnemyState hasn't been initialized yet");
			return;
		}

		psState = ps.playerList.Count;
		esState = es.enemyList.Count;

		// add players and enemies to a temporary list
		List<Actor> temp = new List<Actor> ();

		foreach (Player p in ps.playerList)
			temp.Add (p);
		foreach (Enemy e in es.enemyList)
			temp.Add (e);
			
		// Sort by speed
		actorsList = temp.OrderByDescending (o => o.stats.speed).ToList ();
	}

	/// <summary>
	/// Gets the turn order / ActorsList.
	/// </summary>
	public List<Actor> getTurnOrder(){
		if (actorsList == null)
			return new List<Actor> ();
		else
			return actorsList;
	}

	/// <summary>
	/// Gets the actor whose turn it is currently.
	/// </summary>
	/// <returns>The current actor.</returns>
    public Actor GetCurrentActor()
    {        
		if (actorsList == null || actorsList.Count == 0)
			return null;
		return actorsList[currentTurnIndex];
	}

	/// <summary>
	/// Changes the turn.
	/// </summary>
	public void changeTurns(){
		currentTurnIndex++;

		// loop back around - 
		//  reset remaining moves for each player
		if (currentTurnIndex >= actorsList.Count) {
			currentTurnIndex = 0;
			foreach (Actor a in actorsList)			
				a.stats.remainingMove = a.stats.maxMove;
		}
	}

	/// <summary>
	/// Update this instance.
	/// </summary>
	void Update () {	
		// re-initialize if the playerList or enemyList has changed
		if (ps != null) {
			if (ps.playerList.Count != psState)
				determineTurnOrder();
		}
		if (es != null) {
			if (es.enemyList.Count != esState)
				determineTurnOrder();
		}
		
		// update health displays
		if (actorsList != null)
			foreach (Actor a in actorsList)
				a.UpdateHealthDisplay ();

		Actor current = GetCurrentActor ();

		if (current is Player) {
			// handle player movement
			if (Input.GetButtonDown ("Up")) {
				MoveActor (GetCurrentActor (), Direction.Up);
			} else if (Input.GetButtonDown ("Left")) {
				MoveActor (GetCurrentActor (), Direction.Left);
			} else if (Input.GetButtonDown ("Right")) {
				MoveActor (GetCurrentActor (), Direction.Right);
			} else if (Input.GetButtonDown ("Down")) {
				MoveActor (GetCurrentActor (), Direction.Down);
			}
		}

		// FOR TESTING PURPOSES ONLY:
		//  enemies take a random amount (1-50) of damage and end their turn
		else if (current is Enemy) {
            EnemyAI ai = current.GetComponent<EnemyAI>();

            for (int i = 0; i < ai.movesPerTurn; i++)
            {
                BoardLocation newLocation = ai.RunAIRoutine();
                MoveActorToPanel(current, newLocation);
                if (ai.nextCardToUse != null) ai.nextCardToUse.Play();
            }

			//int damage = Random.Range(1, 50);
			//current.TakeDamage(damage);
			changeTurns();
		}

	}

    /// <summary>
    /// Determines whether the two given Panels are adjacent
    /// </summary>
    /// <returns><c>true</c> if the Panels are adjacent; otherwise, <c>false</c>.</returns>
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
        a.CallActorEvent(EventName.Moved);
        a.stats.remainingMove -= 1;
        return true;
    }

    /// <summary>
    /// Will move an actor to a new location, if possible. More for enemies to use than allies.
    /// </summary>
    /// <param name="a"></param>
    /// <param name="newLocation"></param>
    /// <returns></returns>
    public bool MoveActorToPanel(Actor a, BoardLocation newLocation)
    {
        if (board.board[newLocation.x, newLocation.y].Unit != null) return false; 

        board.board[a.location.x, a.location.y].Unit = null;
        a.location.x = newLocation.x;
        a.location.y = newLocation.y;
        board.board[a.location.x, a.location.y].Unit = a;
        a.transform.position = new Vector3(board.board[a.location.x, a.location.y].screenLocationX, board.board[a.location.x, a.location.y].screenLocationY, 0);
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

	/// <summary>
	/// Removes the actor from the game.
	/// </summary>
	/// <param name="a">The alpha component.</param>
	public void RemoveActor(Actor a)
	{
		if (actorsList.Contains (a)) {
			// remove actor from panel it was in
			board.board[a.location.x, a.location.y].Unit = null;
			// remove actor from our list
			actorsList.Remove (a);
			// destroy the actor
			UnityEngine.Object.Destroy (a.gameObject);
		}
	}

	/// <summary>
	/// Plays the card, if there is enough mana.
	/// </summary>
	/// <returns><c>true</c>, if card was played, <c>false</c> otherwise.</returns>
	/// <param name="c">C.</param>
	public bool PlayCard(Card c)
	{
		// check for the right amount of mana
		if (mana.manaState.CurrentMana < c.manaCost)
			return false;
		else
			// subtract the amount the card is using
			mana.SubtractMana(c.manaCost);

		// let the card do its thing
		c.Play ();

		// move the card to the graveyard
		graveyard.Add (c.transform.root.gameObject);
		hand.Discard (c.transform.root.gameObject);

		return true;
	}
}

