using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour {

    private RaycastHit2D hit;

    private Canvas canvas;
    

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

            if (hit.collider.tag == "Card")
            {
                // TODO: make it so this only happens once
                canvas.enabled = true;
                UpdateDisplay(hit.collider.gameObject);
            }
            else
            {
                canvas.enabled = false;
            }
        }
        else
        {
            //print("");
            canvas.enabled = false;
        }
	}
}
