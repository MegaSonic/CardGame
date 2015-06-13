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

	// Use this for initialization
	void Start () {
        hand = this.gameObject;
        cards = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {
        
        if (Input.GetKeyDown(KeyCode.Z))
        {
            GameObject newCard = Instantiate(Resources.Load("cardTest")) as GameObject;
            newCard.transform.SetParent(this.transform, false);
            newCard.transform.position = spawnPoint.position;
            
            newCard.GetComponent<Image>().color = new Color(Random.value, Random.value, Random.value);
            cards.Add(newCard);
        }

        foreach (GameObject go in cards)
        {
            go.transform.position = Vector3.Lerp(go.transform.position, midPoint.transform.position, 0.4f);
        }
	}
}
