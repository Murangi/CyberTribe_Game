using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public float turnDuration = 15f;         // Duration of each turn in seconds
    private float timer;                     // Countdown timer
    public int currentPlayer = 1;           // 1 for Player 1, 2 for Player 2
    private const string SCRIPT_NAME = "MarblePlayer"; // Name of the script to disable
    private string scriptName;
    GameObject player1;
    GameObject player2;
    List<GameObject> player1Marbles = new List<GameObject>();
    List<GameObject> player2Marbles = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        player1 = GameObject.FindGameObjectWithTag("Player_1");
        player2 = GameObject.FindGameObjectWithTag("Player_2");

        player1Marbles = player1.GetComponent<Player1>().marbles;
        player2Marbles = player2.GetComponent<Player2>().marbles;

        //disable player 2 marbles
        if (player2Marbles != null)
            foreach (var marble in player2Marbles)
                if (marble.GetComponent<MarbleShooter>() != null)
                    Destroy(marble.GetComponent<MarbleShooter>());

        StartTurn();
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        // Check if the timer has run out
        if (timer <= 0)
            EndTurn();
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

        Debug.Log($"Player {currentPlayer}'s turn started. \t {gameObject.name}");
    }

    void EndTurn()
    {
        Debug.Log($"Player {currentPlayer}'s turn ended. \t {gameObject.name}");
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

    void DisableScripts()
    {
        if (currentPlayer == 1)
        {
            if (player1Marbles != null)
                foreach (var marble in player1Marbles)
                    if (marble.GetComponent<MarbleShooter>() != null)
                        Destroy(marble.GetComponent<MarbleShooter>());
        }
        else
        {
            if (player2Marbles != null)
                foreach (var marble in player2Marbles)
                    if (marble.GetComponent<MarbleShooter>() != null)
                        Destroy(marble.GetComponent<MarbleShooter>());
        }
    }

    void EnableScripts()
    {
        if (currentPlayer == 1)
        {
            if (player2Marbles != null)
                foreach (var marble in player2Marbles)
                    if (marble.GetComponent<MarbleShooter>() == null)
                        marble.AddComponent<MarbleShooter>();
        }
        else
        {
            if (player1Marbles != null)
                foreach (var marble in player1Marbles)
                    if (marble.GetComponent<MarbleShooter>() == null)
                        marble.AddComponent<MarbleShooter>();
        }
    }

    public void MoveMade()
    {
        EndTurn();
    }
}