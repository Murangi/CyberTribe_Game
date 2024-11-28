using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameTimer : MonoBehaviour
{
    public TextMeshProUGUI timerText; // Reference to the TextMeshPro object
    public float timeRemaining = 120f; // Starting time in seconds
    public bool timerRunning = true;  // Controls whether the timer is active

    void Update()
    {
        if (timerRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                UpdateTimerDisplay();
            }
            else
            {
                timeRemaining = 0; // Ensure the timer doesn't go negative
                timerRunning = false;
                UpdateTimerDisplay();
                OnTimerEnd(); // Optional: Handle timer end event
            }
        }
    }

    void UpdateTimerDisplay()
    {
        // Format the time as MM:SS
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);
        timerText.text = $"{minutes:00}:{seconds:00}";
    }

    void OnTimerEnd()
    {
        SceneManager.LoadScene(8); // Load the "GameOver" scene when the timer ends
        // Add any additional behavior for when the timer ends
        Debug.Log($"Winner: {gameObject.GetComponent<Winner>().DetermineWinner()}");
    
    }
}

