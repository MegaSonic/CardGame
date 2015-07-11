using UnityEngine;
using System.Collections;

/// <summary>
/// Represents a panel on the game board.
/// </summary>
public class Panel : Extender {

	/// <summary>
	/// The panel's x coordinate.
	/// Public so as to appear in the inspector
	/// </summary>
	public int x;

	/// <summary>
	/// The panel's y coordinate.
	/// Public so as to appear in the inspector
	/// </summary>
	public int y;

	/// <summary>
	/// The x value of the panel's screen location.
	/// </summary>
	private float _screenLocationX;

	/// <summary>
	/// The y value of the panel's screen location.
	/// </summary>
	private float _screenLocationY;

	/// <summary>
	/// Gets or sets the screen location x value.
	/// </summary>
	/// <value>The screen location x.</value>
	public float screenLocationX {
		get {
			return _screenLocationX;
		}
		private set {
			_screenLocationX = value;
		}
	}

	/// <summary>
	/// Gets or sets the screen location y.
	/// </summary>
	/// <value>The screen location y.</value>
	public float screenLocationY {
		get {
			return _screenLocationY;
		}
		private set {
			_screenLocationY = value;
		}
	}

	/// <summary>
	/// Indicates who controls this panel.
	/// Public so as to appear in the inspector
	/// </summary>
    public WhoCanUse Owner;
	    
	/// <summary>
	/// The actor occupying this panel - null if no unit is in panel.
	/// </summary>
    private Actor _Unit;

	/// <summary>
	/// Gets or sets the unit.
	/// </summary>
	/// <value>The unit.</value>
	public Actor Unit {
		get {
			return _Unit;
		}
		set {
			_Unit = value;
		}
	}

	/// Represents the possible parties that can control a panel.
	public enum WhoCanUse {
		Player = 1,
		Enemy = 2,
		Neither = 3
	}

	private SpriteRenderer sprite;
	private Battle battle;
	private PlayerState ps;
	private Board theBoard;

    private bool isFlashing = false;

	/// <summary>
	/// Awake this instance.
	/// </summary>
	void Awake() {
		screenLocationX = this.transform.position.x;
		screenLocationY = this.transform.position.y;
	}

	/// <summary>
	/// Start this instance.
	/// </summary>
	void Start() {
		sprite = GetComponent<SpriteRenderer> ();

		GameObject tmp = GameObject.Find ("Battle");
		battle = ExtensionMethods.GetSafeComponent<Battle>(tmp);
		
		GameObject tmp2 = GameObject.Find ("PlayerState");
		ps = ExtensionMethods.GetSafeComponent<PlayerState>(tmp2);

		GameObject tmp3 = GameObject.Find ("Board");
		theBoard = ExtensionMethods.GetSafeComponent<Board>(tmp3);
	}

    public IEnumerator Flash()
    {
        isFlashing = true;

        sprite.color = Color.black;
        yield return new WaitForSeconds(0.1f);
        UpdateColor();
        yield return new WaitForSeconds(0.1f);
        sprite.color = Color.black;
        yield return new WaitForSeconds(0.1f);
        UpdateColor();
        yield return new WaitForSeconds(0.1f);
        sprite.color = Color.black;
        yield return new WaitForSeconds(0.1f);
        UpdateColor();

        isFlashing = false;
    }

	/// <summary>
	/// Update this instance.
	/// </summary>
	void Update() {

		// apply color based on whocanuse

        if (!isFlashing) {
            UpdateColor();
		}
	}

    private void UpdateColor()
    {
        switch (Owner)
        {
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
}
