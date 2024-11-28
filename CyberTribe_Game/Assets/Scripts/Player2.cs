using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2 : MonoBehaviour
{
    public List<GameObject> player2Marbles = new List<GameObject>();  // Player 2's marbles
    public List<GameObject> OpponentMarblesWon = new List<GameObject>();  // Marbles won by player 2 from player 1
    public int TotalHealthPoints = 60;

    // Start is called before the first frame update
    void Start()
    {
        AddPlayer2Marbles();
    }

    private void AddPlayer2Marbles()
    {
        player2Marbles.AddRange(GameObject.FindGameObjectsWithTag("Marble_6"));
    }
}
