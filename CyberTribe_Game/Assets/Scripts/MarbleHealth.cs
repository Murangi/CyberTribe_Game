using UnityEngine;


public class MarbleHealth : MonoBehaviour
{
    public const int MAX_MARBLE_HEALTH = 10;
    public int health = MAX_MARBLE_HEALTH; // Starting health points for the marble
    private string OpponentTag;
    GameObject gameManager;

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
    }

    private void OnCollisionEnter(Collision collision)
    {
        OpponentTag = (gameManager.GetComponent<TurnManager>().currentPlayer == 1) ? "Player1_Marble" : "Player2_Marble";

        // Check if the collision is with an opponent's marble
        if (collision.gameObject.CompareTag(OpponentTag))
        {
            collision.gameObject.GetComponent<MarbleHealth>().TakeDamage(1);
            if (OpponentTag == "Player1_Marble")
                GameObject.FindGameObjectWithTag("Player1").GetComponent<Player1>().TakeDamage(1);
            else
                GameObject.FindGameObjectWithTag("Player1").GetComponent<Player2>().TakeDamage(1);
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
