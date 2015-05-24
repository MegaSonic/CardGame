using UnityEngine;
using System.Collections;

public class Panel : Extender {
	public int x;

	public int y;

	/// Who is allowed to move onto this panel
	public enum WhoCanUse {
		Player = 1,
		Enemy = 2,
		Neither = 3
	}

	public WhoCanUse Owner;
	
	// Null if no unit is in panel
	public Actor Unit;

	private SpriteRenderer sprite;

	void Start() {
		sprite = GetComponent<SpriteRenderer> ();

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
}
