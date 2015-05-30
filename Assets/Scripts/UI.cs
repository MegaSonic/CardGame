using UnityEngine;
using System.Collections;

public class UI : Extender {

    World world;

	// Use this for initialization
	void Start () {
        world = GameObject.FindGameObjectWithTag("World").GetSafeComponent<World>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void EndTurn()
    {
        world.changeTurns();
        world.GetCurrentActor().CallEndTurnEvent();
    }
}
