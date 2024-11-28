using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2 : MonoBehaviour
{
    public List<GameObject> marbles;// = new List<GameObject>();  // Player 2's marbles
    public List<GameObject> OpponentMarblesWon = new List<GameObject>();  // Marbles won by player 2 from player 1
    private const int NUM_MARBLES_PER_PLAYER = 6;
    public int TotalHealthPoints = NUM_MARBLES_PER_PLAYER * MarbleHealth.MAX_MARBLE_HEALTH;
    public bool isMoveMade = false;

    // Start is called before the first frame update
    void Start()
    {
        AddPlayer2Marbles();
    }

    void Update()
    {
        if (isMoveMade)
        {
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<TurnManager>().MoveMade();
            isMoveMade = false;
        }
    }

    private void AddPlayer2Marbles()
    {
        marbles.AddRange(GameObject.FindGameObjectsWithTag("Player2_Marble"));
    }
    
    public void TakeDamage(int damage)
    {
        TotalHealthPoints -= damage;
    }
}
