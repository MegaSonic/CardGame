using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Keeps track of global game rules such as turn order.
/// </summary>
public class World : MonoBehaviour {
	
	private PlayerState ps;
	private int psState;

	/// <summary>
	/// The turn order, represented as an ordered list of the indices of players in the PlayerState's playerList.
	/// </summary>
	private List<int> turnOrder;

	void Start () {
		GameObject tmp = GameObject.Find ("PlayerState");
		ps = ExtensionMethods.GetSafeComponent<PlayerState>(tmp);

		determineTurnOrder ();
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


	// Update is called once per frame
	void Update () {
	
		if (ps != null) {
			if (ps.playerList.Count != psState)
				determineTurnOrder();
		}


	}
}


