using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public class Hand : MonoBehaviour {

    [SerializeField]
    private Transform spawnPoint;
    [SerializeField]
    private Transform leftPoint;
    [SerializeField]
    private Transform rightPoint;
    [SerializeField]
    private Transform midPoint;

    private GameObject hand;

    private List<GameObject> cards;

    private int drawnCardsIndex;

	// Use this for initialization
	void Start () {
        hand = this.gameObject;
        cards = new List<GameObject>();
        drawnCardsIndex = 0;
	}
	
	// Update is called once per frame
	void Update () {
        

        if (Input.GetKeyDown(KeyCode.Z))
        {

            // not the best way to draw a card, but obviously we'll change this later

            GameObject newCard = Instantiate(Resources.Load("Card Canvas")) as GameObject;

            // NOT WORKING YET
            /*
            GameObject[] tmpcards = Resources.LoadAll<GameObject>("Cards");

            newCard.GetComponentInChildren<Card> = Instantiate(tmpcards[Random.Range(0, tmpcards.Length)]) as GameObject;
            
             */ 
             
             
            newCard.transform.position = spawnPoint.position;

             
            // giving it an id number for easier identification during testing            
            newCard.name = "Card Canvas: " + drawnCardsIndex.ToString();
            
            // set up correct layer order (to properly allow for mouse over)
            Canvas ncCanv = newCard.GetComponent<Canvas>();
            ncCanv.sortingOrder = drawnCardsIndex;

            // increment index
            drawnCardsIndex++;

            newCard.GetComponentInChildren<Image>().color = new Color(Random.value, Random.value, Random.value);
            cards.Add(newCard);

            foreach (GameObject go in cards)
            {
                LeanTween.moveX(go, (midPoint.transform.position.x - ((cards.Count / 2f) - cards.IndexOf(go))), 0.3f);
            }          
        }
	}

    public void UpdateCardPlacement()
    {
        
    }
}
