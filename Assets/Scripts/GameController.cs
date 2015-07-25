using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
    private GameStates gameStates;
    private int totalScoreMarkers = 0;
    private int numTagedScoreMarkers = 0;

    public float powerSlideSpeed = 1;

	// Use this for initialization
	void Start () {
        gameStates = GetComponent<GameStates>();
        foreach (GameObject marker in GameObject.FindGameObjectsWithTag("ScoreMarker"))
        {
            totalScoreMarkers += 1;
        }
        numTagedScoreMarkers = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SetMarkerToCollected()
    {
        numTagedScoreMarkers++;
    }

    public void EndTurn()
    {
        gameStates.playerTurn += 1;
        if (gameStates.playerTurn > gameStates.totalPlayers)
            gameStates.playerTurn = 1;
    }
}
