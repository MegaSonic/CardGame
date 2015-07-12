using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour {

    private RaycastHit2D hit;

    private Canvas canvas;

    public Image image;
    private Text nameText;
    private Text manaText;
    private Text infoText;

	private Vector3 screenPoint;
	private Vector3 draggingOffset;
	private Vector3 cardScreenPos;

    private GameObject cardDisplayed;
	private Battle battle;

    /// <summary>
    /// Updates the UI to match given card
    /// </summary>
    /// <param name="card"></param>
    private void UpdateDisplay(GameObject card)
    {
        //this.GetComponentInChildren<Image>().color = card.GetComponentInChildren<Image>().color;
        //this.GetComponentInChildren<Text>().text = card.name;

        image.color = card.GetComponent<CardUI>().cardBack.color;

        Card cardinfo = card.GetComponentInChildren<Card>();

        nameText.text = cardinfo.cardName;
        manaText.text = cardinfo.manaCost.ToString();
        infoText.text = cardinfo.cardText;
    }

    // Use this for initialization
    void Start()
    {
        canvas = GetComponent<Canvas>();

		GameObject tmp = GameObject.Find ("Battle");
		battle = ExtensionMethods.GetSafeComponent<Battle>(tmp);

        Text[] tmp2 = GetComponentsInChildren<Text>();
        foreach (Text t in tmp2)
        {
            if (t.transform.parent.name == "cardName")
            {
                nameText = t;
            }
            else if (t.transform.parent.name == "manaDisplay")
            {
                manaText = t;
            }
            else if (t.transform.parent.name == "cardInfo")
            {
                infoText = t;
            }
        }
    }

	// Update is called once per frame
	void Update () {      

        // cast a raycast to where the mouse is pointing
        hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        
		if (hit.collider != null && hit.collider.tag == "Card" && !Input.GetMouseButton(0))
        {
            if (cardDisplayed == null)
            {
                // make the card display visible
                this.gameObject.transform.position = new Vector3(hit.collider.gameObject.transform.position.x, this.gameObject.transform.position.y, this.gameObject.transform.position.z);
                canvas.enabled = true;
                cardDisplayed = hit.collider.gameObject;
                cardDisplayed.GetComponent<CardUI>().SetElementsActive(false);
                UpdateDisplay(hit.collider.gameObject);
            }
            else if (hit.collider.gameObject != cardDisplayed)
            {
                // make the card display visible and make the card invisible
                this.gameObject.transform.position = new Vector3(hit.collider.gameObject.transform.position.x, this.gameObject.transform.position.y, this.gameObject.transform.position.z);
                canvas.enabled = true;
                cardDisplayed.GetComponent<CardUI>().SetElementsActive(true);
                cardDisplayed = hit.collider.gameObject;
                cardDisplayed.GetComponent<CardUI>().SetElementsActive(false);
                UpdateDisplay(hit.collider.gameObject);
            }
        }
        else
        {
            if (cardDisplayed != null)
            {
                // hide the card display
                canvas.enabled = false;
                cardDisplayed.GetComponent<CardUI>().SetElementsActive(true);
                cardDisplayed = null;
            }
        }

        // check for mouse click

		if (Input.GetMouseButtonDown (0)) {
			screenPoint = Camera.main.WorldToScreenPoint (gameObject.transform.position);
			draggingOffset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));

			// if clicked on a card
			if (hit.collider != null && hit.collider.tag == "Card")
			{
				// keep track of card's original position
				cardScreenPos = hit.transform.position;
			}
		}

		if (Input.GetMouseButton(0)){
			Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
			Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint); // + draggingOffset;

			// if clicked on a card
			if (hit.collider != null && hit.collider.tag == "Card")
			{
				// move the card's position
				hit.transform.position = curPosition;
			}
		}

		if (Input.GetMouseButtonUp (0)) {
			// if clicked on a card
			if (hit.collider != null && hit.collider.tag == "Card")
			{
				Card clickedCard = hit.collider.GetComponentInChildren<Card>();
				if (battle.PlayCard(clickedCard))
					return;
				else{
					// return card to its original position
					hit.transform.position = cardScreenPos;
					print ("NOT ENOUGH MANA");
				}
			}
		}



		// play card by single click
		/*
        if (Input.GetMouseButtonDown(0))
        {
            // if clicked on a card
            if (hit.collider != null && hit.collider.tag == "Card")
            {
                Card clickedCard = hit.collider.GetComponentInChildren<Card>();
				if (battle.PlayCard(clickedCard))
					return;
				else
					print ("NOT ENOUGH MANA");
            }
        }
        */	
	}
}
