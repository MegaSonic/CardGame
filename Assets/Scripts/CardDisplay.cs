using UnityEngine;
using System.Collections;

public class CardDisplay : MonoBehaviour {

    private RaycastHit2D hit;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {      

        hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if (hit.collider != null)
        {
            print("ok");
            print(hit.collider.name);
        }
        else
        {
            print("");
        }
	}
}
