using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1 : MonoBehaviour
{
    public List<GameObject> player1Marbles = new List<GameObject>();  // Player 1's marbles
    public List<GameObject> OpponentMarblesWon = new List<GameObject>();  // Marbles won by player 1 from player 2
    public int TotalHealthPoints = 60;


    // Start is called before the first frame update
    void Start()
    {
        AddPlayer1Marbles();
    }

    private void AddPlayer1Marbles()
    {
        player1Marbles.AddRange(GameObject.FindGameObjectsWithTag("Marble_1"));
    }
}
