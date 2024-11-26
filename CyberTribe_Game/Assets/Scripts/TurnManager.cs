using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public List<GameObject> player1Marbles = new List<GameObject>();  // Player 1's marbles
    public List<GameObject> player2Marbles = new List<GameObject>();  // Player 2's marbles
    public float turnDuration = 15f;         // Duration of each turn in seconds
    private float timer;                     // Countdown timer
    private int currentPlayer = 1;           // 1 for Player 1, 2 for Player 2
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
        {
            Destroy(marble.GetComponent<MarblePlayer2>());
            // marble.AddComponent<MarblePlayer2>();
        }
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

        // Debug.Log("Player " + currentPlayer + "'s turn started.");
    }

    void EndTurn()
    {
        // Debug.Log("Player " + currentPlayer + "'s turn ended.");
        EnableScripts();
        DisableScripts();

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

            /* if (currentPlayer == 1)
                 marble.GetComponent<MarblePlayer1>().enabled = isActive;
             else
                 marble.GetComponent<MarblePlayer2>().enabled = isActive;*/
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

            /*scriptName = SCRIPT_NAME + Convert.ToString((currentPlayer == 1) ? 2 : 1);// + ".cs";
            // Debug.Log(scriptName + " script is disabled");
            // DisableScript(marble, scriptName);
            if (currentPlayer == 1)
                marble.GetComponent<MarblePlayer1>().enabled = !isActive;
            else
                marble.GetComponent<MarblePlayer2>().enabled = !isActive;*/
        }
        lr.colorGradient = gradient;
        // ToggleScripts();
    }

    //TEST THESES THEN ADD TO START FUNCTION
    private void AddPlayer1Marbles()
    {
        //remove
        /* GameObject marble;

         for (int i = 1; i <= NUM_MARBLES_PER_PLAYER; i++)
         {
             marble = GameObject.FindGameObjectWithTag("Marble_" + Convert.ToString(i));
             marble = GameObject.findGame
             // marble = GameObject.FindGameObjectWithTag("Marble_1");
             player1Marbles.Add(marble);
         }*/

        player1Marbles.AddRange(GameObject.FindGameObjectsWithTag("Marble_1"));


        //remove - just for testing
        /*Debug.Log("Player 1 marbles");
        foreach (var item in player1Marbles)
            Debug.Log("\t" + item.name);*/
    }

    private void AddPlayer2Marbles()
    {
        //remove 
        /*GameObject marble;

        for (int i = NUM_MARBLES_PER_PLAYER + 1; i <= NUM_MARBLES_PER_PLAYER * 2; i++)
        // for (int i = 1; i <= NUM_MARBLES_PER_PLAYER; i++)
        {
            marble = GameObject.FindGameObjectWithTag("Marble_" + Convert.ToString(i));
            // marble = GameObject.FindGameObjectWithTag("Marble_6");
            player2Marbles.Add(marble);
        }*/

        player2Marbles.AddRange(GameObject.FindGameObjectsWithTag("Marble_6"));

        //remove - just for testing
        /*Debug.Log("Player 2 marbles");
        foreach (var item in player2Marbles)
            Debug.Log("\t" + item.name);*/
    }

    void DisableScript(GameObject marble, string scriptName)
    {
        // Find the script by name and disable it
        /*MonoBehaviour script = marble.GetComponent(scriptName) as MonoBehaviour;

        if (script != null)
        {
            script.enabled = false; // Disable the script
            Debug.Log($"{scriptName} has been disabled on {marble.name}");
        }
        else
        {
            Debug.LogWarning($"Script {scriptName} not found on {marble.name}");
        }*/

        if (currentPlayer == 1)
        {
            if (marble.TryGetComponent<MarblePlayer1>(out var marblePlayer1))
                marblePlayer1.enabled = false;
            // marblePlayer1.
        }
        else
        {
            if (marble.TryGetComponent<MarblePlayer2>(out var marblePlayer2))
                marblePlayer2.enabled = false;
        }
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

    void EnableScript(GameObject marble, string scriptName)
    {
        // Find the script by name and disable it
        /*MonoBehaviour script = marble.GetComponent(scriptName) as MonoBehaviour;

        if (script != null)
        {
            script.enabled = false; // Disable the script
            Debug.Log($"{scriptName} has been disabled on {marble.name}");
        }
        else
        {
            Debug.LogWarning($"Script {scriptName} not found on {marble.name}");
        }*/

        if (currentPlayer == 1)
        {
            if (marble.TryGetComponent<MarblePlayer1>(out var marblePlayer1))
                marblePlayer1.enabled = false;
        }
        else
        {
            if (marble.TryGetComponent<MarblePlayer2>(out var marblePlayer2))
                marblePlayer2.enabled = false;
        }
    }

    void ToggleScripts()
    {
        // Debug.Log($"{player1Marbles[0].name} script state {player1Marbles[0].GetComponent<MarblePlayer1>().enabled}");
        // Debug.Log($"{player2Marbles[0].name} script state {player2Marbles[0].GetComponent<MarblePlayer2>().enabled}");
        foreach (var marble in player1Marbles)
        {
            // Debug.Log($"{marble.name} script state {marble.GetComponent<MarblePlayer1>().enabled}");
            marble.GetComponent<MarblePlayer1>().enabled = !marble.GetComponent<MarblePlayer1>().enabled;
            // Debug.Log($"{marble.name} script state {marble.GetComponent<MarblePlayer1>().enabled}");
        }

        foreach (var marble in player2Marbles)
        {
            // Debug.Log($"{marble.name} script state {marble.GetComponent<MarblePlayer2>().enabled}");
            marble.GetComponent<MarblePlayer2>().enabled = !marble.GetComponent<MarblePlayer2>().enabled;
            // Debug.Log($"{marble.name} script state {marble.GetComponent<MarblePlayer2>().enabled}");
        }

        // Debug.Log($"{player1Marbles[0].name} script state {player1Marbles[0].GetComponent<MarblePlayer1>().enabled}");
        // Debug.Log($"{player2Marbles[0].name} script state {player2Marbles[0].GetComponent<MarblePlayer2>().enabled}");
    }

}
