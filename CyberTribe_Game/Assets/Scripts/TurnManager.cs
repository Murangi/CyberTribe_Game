using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public static List<GameObject> player1Marbles = new List<GameObject>();  // Player 1's marbles
    public static List<GameObject> player2Marbles = new List<GameObject>();  // Player 2's marbles
    public float turnDuration = 15f;         // Duration of each turn in seconds
    private float timer;                     // Countdown timer
    public int currentPlayer = 1;           // 1 for Player 1, 2 for Player 2
    const int NUM_MARBLES_PER_PLAYER = 6;
    private const string SCRIPT_NAME = "MarblePlayer"; // Name of the script to disable
    private string scriptName;

    // Start is called before the first frame update
    void Start()
    {
        AddPlayer1Marbles();
        AddPlayer2Marbles();
        
        //disable player 2 marbles
        foreach (var marble in player2Marbles)
            if (marble.GetComponent<MarblePlayer2>() != null)
                Destroy(marble.GetComponent<MarblePlayer2>());

        StartTurn();
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        // Check if the timer has run out
        if (timer <= 0)
            EndTurn();

        if (currentPlayer == 1)
        {
            if (gameObject.GetComponent<MarblePlayer1>() != null)
                if (gameObject.GetComponent<MarblePlayer1>().isMoveMade)
                {
                    gameObject.GetComponent<MarblePlayer1>().isMoveMade = false;
                    EndTurn();
                }
        }
        else
        {
            if (gameObject.GetComponent<MarblePlayer2>() != null)
                if (gameObject.GetComponent<MarblePlayer2>().isMoveMade)
                {
                    gameObject.GetComponent<MarblePlayer2>().isMoveMade = false;
                    EndTurn();
                }
        }



    }

    void StartTurn()
    {
        // Reset timer for new turn
        timer = turnDuration;

        if (currentPlayer == 1)
        {
            // Enable Player 1's marbles and disable Player 2's marbles
            SetMarbleInteraction(player1Marbles, true);
            SetMarbleInteraction(player2Marbles, false);
        }
        else
        {
            // Enable Player 2's marbles and disable Player 1's marbles
            SetMarbleInteraction(player1Marbles, false);
            SetMarbleInteraction(player2Marbles, true);
        }

        // Debug.Log($"Player {currentPlayer}'s turn started. \t {gameObject.name}");
    }

    void EndTurn()
    {
        // Debug.Log($"Player {currentPlayer}'s turn ended. \t {gameObject.name}");
        DisableScripts();
        EnableScripts();

        // Switch player
        currentPlayer = (currentPlayer == 1) ? 2 : 1;

        // Start the next player's turn
        StartTurn();
    }

    private void SetMarbleInteraction(List<GameObject> marbles, bool isActive)
    {
        if (marbles == null)
            Debug.Log($"marbles do not exit");
        else
            foreach (GameObject marble in marbles)
                SetMarbleInteraction(marble, isActive);
    }

    private void SetMarbleInteraction(GameObject marble, bool isActive)
    {
        Color startColor = Color.red; // Start color for the gradient
        Color endColor = Color.blue; // End color for the gradient

        LineRenderer lr = marble.GetComponent<LineRenderer>();

        Gradient gradient = new Gradient();

        if (isActive)
        {//keep red
            // Define the gradient's color keys and alpha keys
            gradient.colorKeys = new GradientColorKey[]
                {
                new GradientColorKey(startColor, 0.0f), // Start color at time 0
                new GradientColorKey(startColor, 1.0f)   // End color at time 1
                };

            gradient.alphaKeys = new GradientAlphaKey[]
            {
                new GradientAlphaKey(1.0f, 0.0f), // Full opacity at time 0
                new GradientAlphaKey(1.0f, 1.0f)  // Full opacity at time 1
            };
        }
        else
        {//change blue

            // Define the gradient's color keys and alpha keys
            gradient.colorKeys = new GradientColorKey[]
                {
                new GradientColorKey(endColor, 0.0f), // Start color at time 0
                new GradientColorKey(endColor, 1.0f)   // End color at time 1
                };

            gradient.alphaKeys = new GradientAlphaKey[]
            {
                new GradientAlphaKey(1.0f, 0.0f), // Full opacity at time 0
                new GradientAlphaKey(1.0f, 0.0f)  // Full opacity at time 1
            };
        }
        lr.colorGradient = gradient;
    }

    //TEST THESES THEN ADD TO START FUNCTION
    private void AddPlayer1Marbles()
    {
        player1Marbles.AddRange(GameObject.FindGameObjectsWithTag("Marble_1"));
    }

    private void AddPlayer2Marbles()
    {
        player2Marbles.AddRange(GameObject.FindGameObjectsWithTag("Marble_6"));
    }

    void DisableScripts()
    {
        if (currentPlayer == 1)
        {
            foreach (var marble in player1Marbles)
                if (marble.GetComponent<MarblePlayer1>() != null)
                    Destroy(marble.GetComponent<MarblePlayer1>());
        }
        else
        {
            foreach (var marble in player2Marbles)
                if (marble.GetComponent<MarblePlayer2>() != null)
                    Destroy(marble.GetComponent<MarblePlayer2>());
        }
    }

    void EnableScripts()
    {
        if (currentPlayer == 1)
        {
            foreach (var marble in player2Marbles)
                if (marble.GetComponent<MarblePlayer2>() == null)
                    marble.AddComponent<MarblePlayer2>();
        }
        else
        {
            foreach (var marble in player1Marbles)
                if (marble.GetComponent<MarblePlayer1>() == null)
                    marble.AddComponent<MarblePlayer1>();
        }
    }

    public void MoveMade()
    {
        EndTurn();
    }
}
/*using UnityEngine;


public class MarbleHealth : MonoBehaviour
{
    public int health = 10; // Starting health points for the marble
    private string OpponentTag;

    private void OnCollisionEnter(Collision collision)
    {
        OpponentTag = (gameObject.GetComponent<TurnManager>().currentPlayer == 1) ? "Marble_1" : "Marble_6";

        // Check if the collision is with an opponent's marble
        if (collision.gameObject.CompareTag(OpponentTag))
        {
            collision.gameObject.GetComponent<MarbleHealth>().TakeDamage(1);
            Debug.Log($"{gameObject.name} was hit! Remaining health: {health}\t\t hit by marble{collision.gameObject.name}");

            // Check if health is zero or less
            if (health <= 0)
                DestroyMarble();
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
    }

    private void DestroyMarble()
    {
        // Debug.Log($"{gameObject.name} is destroyed!");
        Destroy(gameObject);
    }
}
*/