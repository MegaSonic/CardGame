using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ManaDisplay : MonoBehaviour {

    Mana mana;
    Text textDisplay;

	// Use this for initialization
	void Start () {
        mana = GameObject.FindGameObjectWithTag("World").GetSafeComponent<Mana>();
        textDisplay = this.gameObject.GetSafeComponent<Text>();
        textDisplay.text = mana.manaState.CurrentMana.ToString() + " / " + mana.manaState.MaxMana.ToString();
	}
	
	// Update is called once per frame
	void Update () {
        if (mana.manaStateChanged)
        {
            textDisplay.text = mana.manaState.CurrentMana.ToString() + " / " + mana.manaState.MaxMana.ToString();
            mana.manaStateChanged = false;
        }
	}
}
