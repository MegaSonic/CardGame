using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class CardDisplay : MonoBehaviour
{

    private RaycastHit2D hit;

    private Transform cardHit = null;
    private Transform panelHit = null;
    private Transform actorHit = null;
    private Transform fieldHit = null;

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
    private bool isDraggingCard = false;
    private GameObject cardToDrag;

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

        GameObject tmp = GameObject.Find("Battle");
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
    void Update()
    {

        cardHit = null;
        panelHit = null;
        actorHit = null;
        fieldHit = null;

        bool hitCardThisFrame = false;

        RaycastHit2D[] hits = Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);



        foreach (RaycastHit2D h in hits)
        {
            if (h.collider != null)
            {
                if (h.collider.tag == "Card" && !hitCardThisFrame)
                {
                    cardHit = h.collider.transform;
                    hitCardThisFrame = true;

                }
                if (h.collider.tag == "Panel") panelHit = h.collider.transform;
                if (h.collider.tag == "PlayingField") fieldHit = h.collider.transform;
            }
        }

        // cast a raycast to where the mouse is pointing
        hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if (cardHit != null && !Input.GetMouseButton(0))
        {
            if (cardDisplayed == null && !LeanTween.isTweening(cardHit.gameObject))
            {
                // make the card display visible
                this.gameObject.transform.position = new Vector3(cardHit.position.x, this.gameObject.transform.position.y, this.gameObject.transform.position.z);
                canvas.enabled = true;
                cardDisplayed = cardHit.gameObject;
                cardDisplayed.GetComponent<CardUI>().SetElementsActive(false);
                UpdateDisplay(cardHit.gameObject);
            }
            else if (cardDisplayed != null && cardHit.gameObject != cardDisplayed)
            {
                // make the card display visible and make the card invisible
                this.gameObject.transform.position = new Vector3(cardHit.position.x, this.gameObject.transform.position.y, this.gameObject.transform.position.z);
                canvas.enabled = true;
                cardDisplayed.GetComponent<CardUI>().SetElementsActive(true);
                cardDisplayed = cardHit.gameObject;
                cardDisplayed.GetComponent<CardUI>().SetElementsActive(false);
                UpdateDisplay(cardHit.gameObject);
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
            screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
            draggingOffset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));

            // if clicked on a card
            if (cardHit != null)
            {
                // keep track of card's original position
                cardScreenPos = cardHit.position;
                cardToDrag = cardHit.gameObject;
                isDraggingCard = true;
                cardDisplayed = null;
                canvas.enabled = false;
            }
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
            Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint); // + draggingOffset;

            // if clicked on a card
            if (isDraggingCard)
            {
                // move the card's position

                LeanTween.moveX(cardToDrag, curPosition.x, 0.02f);
                LeanTween.moveY(cardToDrag.gameObject, curPosition.y, 0.02f);
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            // if clicked on a card
            if (isDraggingCard)
            {
                if (fieldHit != null)
                {
                    Card clickedCard = cardToDrag.GetComponentInChildren<Card>();
                    if (battle.PlayCard(clickedCard))
                    {
                        cardToDrag = null;
                        isDraggingCard = false;
                    }
                    else
                    {
                        // return card to its original position
                        ReturnCardToOriginalPos();
                        //hit.transform.position = cardScreenPos;
                        print("NOT ENOUGH MANA");
                    }
                }
                else
                {
                    // return card to its original position
                    ReturnCardToOriginalPos();
                }
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            if (isDraggingCard)
            {
                ReturnCardToOriginalPos();
            }
        }

    }

    private void ReturnCardToOriginalPos()
    {
        LeanTween.cancel(cardToDrag);
        LeanTween.moveX(cardToDrag, cardScreenPos.x, 0.2f);
        LeanTween.moveY(cardToDrag, cardScreenPos.y, 0.2f);
        cardToDrag = null;
        isDraggingCard = false;
    }
}
