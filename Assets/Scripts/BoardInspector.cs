using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(Board))]
public class BoardInspector : Editor {

	public override void OnInspectorGUI() {
		Board BoardScript = (Board)target;

		if (BoardScript == null)
			return;
		if (BoardScript.board == null)
			return;

		int col = 0;

		foreach (Panel p in BoardScript.board) {
			if (p == null) return;

			if (col == 0) GUILayout.BeginHorizontal();

			GUIStyle boxStyle = new GUIStyle(GUI.skin.box);
			
			switch (p.Owner) {
			case 0:
				boxStyle.normal.textColor = Color.blue;
				break;
			case 1:
				boxStyle.normal.textColor = Color.red;
				break;
			default:
				boxStyle.normal.textColor = Color.gray;
				break;
			}
			
			
			if (p.Unit == null) {
				GUILayout.Box(".", boxStyle);
			}
			else if (p.Unit is Enemy) {
				GUILayout.Box("x", boxStyle);
			}

			col++;

			if (col == 6) { 
				GUILayout.EndHorizontal();
				col = 0;
			}
		}
	}
}
