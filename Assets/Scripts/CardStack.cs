using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Used for any stack of cards. Decks, discard piles, etc.
/// </summary>
public class CardStack : Extender {

	/// <summary>
	/// The list of cards in this stack.
	/// </summary>
	public List<Card> cardList;

	/// <summary>
	/// Index of the card on top of the deck.
	/// </summary>
	public int index;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Shuffle() {
		for (int i = 0; i < cardList.Count; i++) {
			Card temp = cardList[i];
			int randomIndex = Random.Range(i, cardList.Count);
			cardList[i] = cardList[randomIndex];
			cardList[randomIndex] = temp;
		}
	}
}
