using UnityEngine;
using System.Collections;

/// <summary>
/// Internal representation of the Board/Field.
/// </summary>
public class Board : Extender {

	[SerializeField]
	// (0,0) refers to the top left panel
	// (5,2) refers to the bottom right panel
	public Panel[,] board =  new Panel[6,3];

	void Start() {
		foreach (Panel p in (Panel[]) GameObject.FindObjectsOfType<Panel> ()) {
			board[p.x, p.y] = p;

		}
	}

	public void GetIDFromActor(Actor actor, ref int x, ref int y) {
		foreach (Panel p in board) {
			if (p.Unit == actor) {

			}
		}
	}
}
