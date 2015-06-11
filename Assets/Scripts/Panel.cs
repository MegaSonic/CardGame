using UnityEngine;
using System.Collections;

public class Panel : Extender {

	public int x;
	public int y;

	public float screenLocationX;
	public float screenLocationY;

    public WhoCanUse Owner;

    // Null if no unit is in panel
    public Actor Unit;

	/// Who is allowed to move onto this panel
	public enum WhoCanUse {
		Player = 1,
		Enemy = 2,
		Neither = 3
	}

	private SpriteRenderer sprite;
	private World world;
	private PlayerState ps;
	private Board theBoard;

	private float doubleClickSensitivity = 0.3f;
	private float clickTime = 0f;

	void Start() {
		sprite = GetComponent<SpriteRenderer> ();

		GameObject tmp = GameObject.Find ("World");
		world = ExtensionMethods.GetSafeComponent<World>(tmp);
		
		GameObject tmp2 = GameObject.Find ("PlayerState");
		ps = ExtensionMethods.GetSafeComponent<PlayerState>(tmp2);

		GameObject tmp3 = GameObject.Find ("Board");
		theBoard = ExtensionMethods.GetSafeComponent<Board>(tmp3);

		screenLocationX = this.transform.position.x;
		screenLocationY = this.transform.position.y;
	}

	void Update() {
		switch (Owner) {
		case WhoCanUse.Player:
			sprite.color = Color.blue;
			break;
		case WhoCanUse.Enemy:
			sprite.color = Color.red;
			break;
		default:
			sprite.color = Color.grey;
			break;
		}
	}

	/// <summary>
	/// Raises the mouse up event.
	/// </summary>
	void OnMouseUp()
	{
		/*
		if ((Time.time - clickTime) < doubleClickSensitivity) {
			OnDoubleClick ();
			clickTime = -1f;
		} else {
			clickTime = Time.time;
		}
	*/
	}

    void OnMouseDown()
    {

    }

	/// <summary>
	/// Raises the double click event.
	/// </summary>
	void OnDoubleClick()
	{
		/*
		if (Owner == WhoCanUse.Player && Unit == null)
			MoveCurrentPlayerIntoMe ();
			*/
	}

	/// <summary>
	/// Moves the current player into this panel.
	/// </summary>
	private void MoveCurrentPlayerIntoMe()
	{
		/*
		Player curPlayer = ps.playerList[world.getCurrentTurn()];

		// if player isn't adjacent, don't move
		if (!IsAdjacent (curPlayer))
			return;

		// if player can't move, don't
		if (curPlayer.stats.remainingMove <= 0)
			return;
		else
			curPlayer.stats.remainingMove -= 1;
		
		// remove player from it's previous panel
		BoardLocation oldLoc = curPlayer.location;
		Panel oldPanel = theBoard.board [oldLoc.x, oldLoc.y];
		oldPanel.Unit = null;

		// move player into this panel
		curPlayer.location = new BoardLocation (x, y);
		curPlayer.transform.position = new Vector3 (screenLocationX, screenLocationY, 0);
		Unit = curPlayer;
		*/
	}
	 
	/// <summary>
	/// Determines whether this panel is adjacent the specified player.
	/// </summary>
	/// <returns><c>true</c> if this instance is adjacent the specified p; otherwise, <c>false</c>.</returns>
	/// <param name="p">Player.</param>
	public bool IsAdjacent(Player p)
	{
		// get player's x and y values
		int pX = p.location.x;
		int pY = p.location.y;

		int diffX = Mathf.Abs (pX - x);
		int diffY = Mathf.Abs (pY - y);
		
		if (diffX + diffY != 1)
			return false;
		else
			return true;
	}

}
