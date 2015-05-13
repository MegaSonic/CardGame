using UnityEngine;
using System.Collections;

public class Board : Extender {

	// (0,0) refers to the top left panel
	// (5,2) refers to the bottom right panel
	public Panel[,] board =  new Panel[6,3];

	void Start() {
		for (int i = 0; i < 6; i++) {
			for (int j = 0; j < 3; j++) {
				board [i, j] = new Panel ();
				board[i,j].x = i;
				board[i,j].y = j;
			}
		}
	}

	public void GetIDFromActor(Actor actor, ref int x, ref int y) {
		foreach (Panel p in board) {
			if (p.Unit == actor) {

			}
		}
	}
}

[System.Serializable]
public class Panel {
	[HideInInspector]
	public int x;

	[HideInInspector]
	public int y;

	// 0 for player controlled, 1 for enemy controlled, 2 for neither
	public int Owner;
	
	// Null if no unit is in panel
	public Actor Unit;

}
