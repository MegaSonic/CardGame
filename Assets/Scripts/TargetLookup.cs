using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TargetLookup {

	public enum TargetType {
		WideSword,
		LongSword
	}

	// This will return all actors in the area of effect
	// If you want it to "smart cast", aka not hit player units if cast by a player
	// You'll need another check
	public IEnumerable<Actor> Lookup(TargetType targetType, Board field, Actor cardUser) {

		switch (targetType) {

			// Hits all units in a 1x3 area towards the opposing area
		case TargetType.WideSword:
			foreach (Panel p in field.board) {
				if (cardUser == p.Unit) {
					int x = p.x;
					int y = p.y;

					if (cardUser is Player) {
						for (int i = y - 1; i < y + 2; i++) {
							if (i != -1 && i != 3 && field.board[x-1, i].Unit != null) {
								yield return field.board[x-1, i].Unit;
							}
						}
					}

					if (cardUser is Enemy) {
						for (int i = y - 1; i < y + 2; i++) {
							if (i != -1 && i != 3 && field.board[x+1, i].Unit != null) {
								yield return field.board[x+1, i].Unit;
							}
						}
					}
				}
			}
			break;

		}

	}
}
