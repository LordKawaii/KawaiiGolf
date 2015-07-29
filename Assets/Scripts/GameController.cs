using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
    private GameStates gameStates;
    private PlayerCurcularList players;
    private int totalScoreMarkers = 0;
    private int numTagedScoreMarkers = 0;
    private int totalPlayers = 1;
    private CameraControll camCon;
    
    private GameObject player1;
    private GameObject player2;
    private GameObject player3;
    private GameObject player4;

    public float powerSlideSpeed = 1;
    public GameObject playerObject;
    public GameObject mainCamera;

	// Use this for initialization
	void Start () {
        gameStates = GetComponent<GameStates>();
        players = new PlayerCurcularList();
        camCon = mainCamera.GetComponent<CameraControll>();
	}
	
	// Update is called once per frame
	void Update () {
        if (gameStates.levelStart)
            SetupLevel();
	}

    void SetupLevel() 
    {
        
        foreach (GameObject marker in GameObject.FindGameObjectsWithTag("ScoreMarker"))
        {
            totalScoreMarkers += 1;
        }

        numTagedScoreMarkers = 0;

        player1 = Instantiate(playerObject) as GameObject;
        players.Add(ref player1);
        camCon.SetTarget(players.GetCurrent());

        gameStates.levelStart = false;

        bool temp = players.GetController().GetPlayerStates();
        if (temp == null)
            Debug.Log("thisPlayersTurn is true");

        //players.GetController().GetPlayerStates().thisPlayersTurn = true;

        
    }

    public void SetMarkerToCollected()
    {
        numTagedScoreMarkers++;
    }

    public void EndTurn()
    {
        //players.GetController().GetPlayerStates().thisPlayersTurn = false;
        players.MoveToNext();
        //players.GetController().GetPlayerStates().thisPlayersTurn = true;
    }

    public GameObject GetCurrentPlayer()
    {
        return players.GetCurrent();
    }
}
