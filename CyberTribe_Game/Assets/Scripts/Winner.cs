using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Winner : MonoBehaviour
{

    //To determine the winner, we need to check if either player has no marbles left, or the collective health points
    // Start is called before the first frame update

    private string GameState = "Playing";
    private bool GameEnded = false;
    public List<GameObject> RemainingPlayer1Marbles = new List<GameObject>();  // Player 1's marbles
    public List<GameObject> RemainingPlayer2Marbles = new List<GameObject>();
    public int TotalPlayer1HP;
    public int TotalPlayer2HP;


    void Awake()
    {

        RemainingPlayer1Marbles.AddRange(GameObject.FindGameObjectsWithTag("Marble_1"));
        RemainingPlayer2Marbles.AddRange(GameObject.FindGameObjectsWithTag("Marble_6"));

        // Debug.Log($"Player 1 remaining marbles: {RemainingPlayer1Marbles.Count}");
        // Debug.Log($"Player 2 remaining marbles: {RemainingPlayer2Marbles.Count}");

        TotalPlayer1HP = MarbleHealth.MAX_MARBLE_HP * RemainingPlayer1Marbles.Count;
        TotalPlayer2HP = MarbleHealth.MAX_MARBLE_HP * RemainingPlayer2Marbles.Count;

        Debug.Log($"Player 1 total health: {TotalPlayer1HP}");
        Debug.Log($"Player 2 total health: {TotalPlayer2HP}");

        UpdateRemainingMarbles(); // Initialize marble lists dynamically
    }

    // Update is called once per frame
    void Update()
    {
        UpdateRemainingMarbles();

        if (RemainingPlayer1Marbles.Count == 0 && RemainingPlayer2Marbles.Count == 0)
            gameObject.GetComponent<GameTimer>().timeRemaining = 0f;

        if (RemainingPlayer1Marbles.Count == 0)
            gameObject.GetComponent<GameTimer>().timeRemaining = 0f;

        if (RemainingPlayer2Marbles.Count == 0)
            gameObject.GetComponent<GameTimer>().timeRemaining = 0f;

        /*string newGameState = DetermineWinner();

        //It can either be player1 that wins, player2 that wins, or a draw.
        GameState = DetermineWinner();
        Debug.Log(GameState);
 
        if (GameState != "Playing" && !GameEnded)
        {
            GameEnded = true;
            Debug.Log("The winner is: " + GameState);
 
            // Load GameOver scene
            SceneManager.LoadScene(8);
        }
 
        if(GameState == "Player1")
        {
            Debug.Log("The winner is: " + GameState);
            GameEnded = true;
 
        }
        else if(GameState == "Player2")
        {
            Debug.Log("The winner is: " + GameState);
            GameEnded = true;
        }
        else if(GameState == "Draw")
        {
            Debug.Log("The winner is: " + GameState);
            GameEnded = true;
        }
 
        if(GameEnded)
        {  
            //This takes us to the GameOver scene
            SceneManager.LoadScene(8);
        }*/
    }

    private void UpdateRemainingMarbles()
    {
        // Remove null entries for destroyed marbles
        RemainingPlayer1Marbles.RemoveAll(marble => marble == null);
        RemainingPlayer2Marbles.RemoveAll(marble => marble == null);

        // Debug.Log($"Player 1 remaining marbles: {RemainingPlayer1Marbles.Count}");
        // Debug.Log($"Player 2 remaining marbles: {RemainingPlayer2Marbles.Count}");
    }

    public string DetermineWinner()
    {
        int Marble1TotalHealth = 0;
        int Marble2TotalHealth = 0;
        string winner = "Draw";

        UpdateRemainingMarbles();

        if (RemainingPlayer1Marbles.Count == 0 && RemainingPlayer2Marbles.Count == 0)
            return "Draw";

        if (RemainingPlayer1Marbles.Count > RemainingPlayer2Marbles.Count)
            return "Player1";

        if (RemainingPlayer1Marbles.Count < RemainingPlayer2Marbles.Count)
            return "Player2";

        if (RemainingPlayer1Marbles.Count == RemainingPlayer2Marbles.Count)
        {
            foreach (var marble in RemainingPlayer1Marbles)
                if (marble != null)
                    if (marble.GetComponent<MarbleHealth>())
                        Marble1TotalHealth += marble.GetComponent<MarbleHealth>().health;

            foreach (var marble in RemainingPlayer2Marbles)
                if (marble != null)
                    if (marble.GetComponent<MarbleHealth>())
                        Marble2TotalHealth += marble.GetComponent<MarbleHealth>().health;

            if (Marble1TotalHealth > Marble2TotalHealth)
                return "Player1";

            if (Marble2TotalHealth > Marble1TotalHealth)
                return "Player2";

            return "Draw";
        }

        return winner;

    }
}

