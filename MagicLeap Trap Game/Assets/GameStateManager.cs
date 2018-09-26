using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.MagicLeap;

public class GameStateManager : MonoBehaviour {

    public enum GameState { Mapping, Setup, Running, Win, Loss};
    public GameState gameState;
    bool navMesh = false;
    GameState previousState;
    GameSetup setupObject;

    private void Awake()
    {
        gameState = GameState.Mapping;
        previousState = gameState;
        setupObject = GameObject.Find("MeshHolder").GetComponent<GameSetup>();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(previousState == GameState.Mapping && gameState == GameState.Setup)
        {
            GameObject.FindGameObjectWithTag("MLSpatialMapper").GetComponent<MLSpatialMapper>().enabled = false;
            setupObject.GenerateNavMesh();
            gameState = GameState.Running;
        }
        if (Input.GetKeyUp(KeyCode.U))
        {
            gameState = GameState.Setup;
        }
        switch (gameState)
        {
            case GameState.Mapping:
                print("Mapping enabled");
                break;
            case GameState.Setup:
                print("Setup");
                break;
            case GameState.Running:
                print("Game is running");
                break;
            case GameState.Win:
                print("You win!");
                break;
            case GameState.Loss:
                print("You lose.");
                break;
            default:
                break;
        }
    }

    void Win()
    {
        print("YOU WIN YAY");
    }

    void Loss()
    {
        print("YOU LOSE GIT GUD SCRUB");
    }
}
