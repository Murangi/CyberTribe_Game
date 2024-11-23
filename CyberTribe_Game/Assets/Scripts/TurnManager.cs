using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public List<GameObject> player1Marbles = new List<GameObject>();  // Player 1's marbles
    public List<GameObject> player2Marbles = new List<GameObject>();  // Player 2's marbles
    public float turnDuration = 15f;         // Duration of each turn in seconds
    private float timer;                     // Countdown timer
    private int currentPlayer = 1;           // 1 for Player 1, 2 for Player 2
    const int NUM_MARBLES_PER_PLAYER = 5;

    public string scriptName = "MarblePlayer"; // Name of the script to disable

    //LineRenderer lr = marble.GetComponent<LineRenderer>();

    // Start is called before the first frame update
    void Start()
    {
        GameObject Marble1 = GameObject.FindGameObjectWithTag("Marble_1");
        GameObject Marble2 = GameObject.FindGameObjectWithTag("Marble_6");

        //REMOVE, INTIALIZED IN DECLARATION
        // player1Marbles = new List<GameObject>();
        // player2Marbles = new List<GameObject>();

        player1Marbles.Add(Marble1);
        player1Marbles.Add(Marble2);

        // AddPlayer1Marbles();
        // AddPlayer2Marbles();

        StartTurn();
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        // Check if the timer has run out
        if (timer <= 0)
        {
            EndTurn();
        }
    }

    void StartTurn()
    {
        // Reset timer for new turn
        timer = turnDuration;

        if (currentPlayer == 1)
        {
            // Enable Player 1's marbles and disable Player 2's marbles
            // SetMarbleInteraction(player1Marbles, true);
            // SetMarbleInteraction(player2Marbles, false);
            SetMarbleInteraction(GameObject.FindGameObjectWithTag("Marble_1"), true);
            SetMarbleInteraction(GameObject.FindGameObjectWithTag("Marble_6"), false);

        }
        else
        {
            // Enable Player 2's marbles and disable Player 1's marbles
            // SetMarbleInteraction(player1Marbles, false);
            // SetMarbleInteraction(player2Marbles, true);
            SetMarbleInteraction(GameObject.FindGameObjectWithTag("Marble_1"), false);
            SetMarbleInteraction(GameObject.FindGameObjectWithTag("Marble_6"), true);
        }

        Debug.Log("Player " + currentPlayer + "'s turn started.");
    }

    void EndTurn()
    {
        Debug.Log("Player " + currentPlayer + "'s turn ended.");
        // Switch player
        currentPlayer = (currentPlayer == 1) ? 2 : 1;

        // Start the next player's turn
        StartTurn();
    }

    private void SetMarbleInteraction(List<GameObject> marbles, bool isActive)
    {
        if (marbles == null)
        {
            Debug.Log($"marbles do not exit");
        }
        else
        {
            foreach (GameObject marble in marbles)
            {
                SetMarbleInteraction(marble, isActive);
            }
        }
    }

    private void SetMarbleInteraction(GameObject marble, bool isActive)
    {
        Color startColor = Color.red; // Start color for the gradient
        Color endColor = Color.blue; // End color for the gradient

        LineRenderer lr = marble.GetComponent<LineRenderer>();

        Gradient gradient = new Gradient();

        if (!isActive)
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
            scriptName = scriptName + Convert.ToString(currentPlayer) + ".cs";
            DisableScript(marble);

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
            // scriptName = scriptName + Convert.ToString(currentPlayer) + ".cs";
            // DisableScript(marble);
        }
        lr.colorGradient = gradient;
    }

    //TEST THESES THEN ADD TO START FUNCTION
    private void AddPlayer1Marbles()
    {
        GameObject marble;

        for (int i = 1; i <= NUM_MARBLES_PER_PLAYER; i++)
        {
            marble = GameObject.FindGameObjectWithTag("Marble_" + Convert.ToString(i));
            player1Marbles.Add(marble);
        }
    }

    private void AddPlayer2Marbles()
    {
        GameObject marble;

        for (int i = NUM_MARBLES_PER_PLAYER + 1; i <= NUM_MARBLES_PER_PLAYER * 2; i++)
        {
            marble = GameObject.FindGameObjectWithTag("Marble_" + Convert.ToString(i));
            player2Marbles.Add(marble);
        }
    }



    void DisableScript(GameObject marble)
    {
        // Find the script by name and disable it
        MonoBehaviour script = marble.GetComponent(scriptName) as MonoBehaviour;

        if (script != null)
        {
            script.enabled = false; // Disable the script
            Debug.Log($"{scriptName} has been disabled on {marble.name}");
        }
        else
        {
            Debug.LogWarning($"Script {scriptName} not found on {marble.name}");
        }
    }

    // Example of calling the function, e.g., on Start


}
