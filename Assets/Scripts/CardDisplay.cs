using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour {

    private RaycastHit2D hit;

    private Canvas canvas;

    private GameObject cardDisplayed;

    private void UpdateDisplay(GameObject card)
    {
        this.GetComponentInChildren<Image>().color = card.GetComponentInChildren<Image>().color;
        this.GetComponentInChildren<Text>().text = card.name;
    }

    // Use this for initialization
	void Start () {
        canvas = GetComponent<Canvas>();
    }
	
	// Update is called once per frame
	void Update () {      

        hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if (hit.collider != null)
        {
            print(hit.collider.name);

            if (hit.collider.tag == "Card" && cardDisplayed == null)
            {
                canvas.enabled = true;
                cardDisplayed = hit.collider.gameObject;
                cardDisplayed.GetComponent<CardUI>().SetElementsActive(false);
                UpdateDisplay(hit.collider.gameObject);
            }
            else if (hit.collider.tag == "Card" && hit.collider.gameObject != cardDisplayed)
            {
                canvas.enabled = true;
                cardDisplayed.GetComponent<CardUI>().SetElementsActive(true);
                cardDisplayed = hit.collider.gameObject;
                cardDisplayed.GetComponent<CardUI>().SetElementsActive(false);
                UpdateDisplay(hit.collider.gameObject);
            }
        }
        else
        {
            //print("");
            if (cardDisplayed != null)
            {
                canvas.enabled = false;
                cardDisplayed.GetComponent<CardUI>().SetElementsActive(true);
                cardDisplayed = null;
            }
        }
	}
}
