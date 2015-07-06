using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CardUI : MonoBehaviour {

    public Text cardNameUI;
    public Text manaTextUI;
    public Text cardDescUI;

    private Card cardScript;

	// Use this for initialization
	void Start () {
        cardScript = GetComponentInChildren<Card>();
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
