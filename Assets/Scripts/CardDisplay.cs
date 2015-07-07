using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour {

    private RaycastHit2D hit;

    private Canvas canvas;

    private Image image;
    private Text nameText;
    private Text manaText;
    private Text infoText;

    private GameObject cardDisplayed;

    /// <summary>
    /// Updates the UI to match given card
    /// </summary>
    /// <param name="card"></param>
    private void UpdateDisplay(GameObject card)
    {
        //this.GetComponentInChildren<Image>().color = card.GetComponentInChildren<Image>().color;
        //this.GetComponentInChildren<Text>().text = card.name;

        image.color = card.GetComponentInChildren<Image>().color;

        Card cardinfo = card.GetComponentInChildren<Card>();

        nameText.text = cardinfo.cardName;
        manaText.text = cardinfo.manaCost.ToString();
        infoText.text = cardinfo.cardText;
    }

    // Use this for initialization
    void Start()
    {
        canvas = GetComponent<Canvas>();
        image = GetComponentInChildren<Image>();

        Text[] tmp = GetComponentsInChildren<Text>();
        foreach (Text t in tmp)
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

        if (hit.collider != null)
        {
            if (hit.collider.tag == "Card" && cardDisplayed == null)
            {
                // make the card display visible
                canvas.enabled = true;
                cardDisplayed = hit.collider.gameObject;
                cardDisplayed.GetComponent<CardUI>().SetElementsActive(false);
                UpdateDisplay(hit.collider.gameObject);
            }
            else if (hit.collider.tag == "Card" && hit.collider.gameObject != cardDisplayed)
            {
                // make the card display visible and make the card invisible
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
        if (Input.GetMouseButtonDown(0))
        {
            // if clicked on a card
            if (hit.collider != null && hit.collider.tag == "Card")
            {
                Card clickedCard = hit.collider.GetComponentInChildren<Card>();
                clickedCard.Play();
            }
        }
	}
}
