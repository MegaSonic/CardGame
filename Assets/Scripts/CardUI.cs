using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CardUI : MonoBehaviour {

    public Text cardNameUI;
    public Text manaTextUI;
    public Text cardDescUI;
    public Image cardBack;

    public Color fighterColor;
    public Color thiefColor;
    public Color mageColor;
    public Color anyColor;

    private Card cardScript;

	// Use this for initialization
	void Start () {
        cardScript = GetComponentInChildren<Card>();

        switch (cardScript.characterRestriction)
        {
            case PlayerType.Warrior:
                cardBack.color = fighterColor;
                break;
            case PlayerType.Thief:
                cardBack.color = thiefColor;
                break;
            case PlayerType.Mage:
                cardBack.color = mageColor;
                break;
            case PlayerType.None:
                cardBack.color = anyColor;
                break;
        }

        StartCoroutine("UpdateUI");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    IEnumerator UpdateUI()
    {
       

        cardNameUI.text = cardScript.cardName;
        manaTextUI.text = cardScript.manaCost.ToString();
        cardDescUI.text = cardScript.cardText;
        yield return new WaitForSeconds(0.2f);
    }

    public void SetElementsActive(bool active)
    {
        foreach (Image i in gameObject.GetComponentsInChildren<Image>())
        {
            i.enabled = active;
        }
        foreach (Text t in gameObject.GetComponentsInChildren<Text>())
        {
            t.enabled = active;
        }
    }
}
