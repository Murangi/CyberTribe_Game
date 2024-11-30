using UnityEngine;


public class MarbleHealth : MonoBehaviour
{
    public const int MAX_MARBLE_HP = 10;
    public int health = MAX_MARBLE_HP; // Starting health points for the marble
    private string OpponentTag;

    private void OnCollisionEnter(Collision collision)
    {
        OpponentTag = (gameObject.GetComponent<TurnManager>().currentPlayer == 1) ? "Marble_6" : "Marble_1";

        // Check if the collision is with an opponent's marble
        if (collision.gameObject.CompareTag(OpponentTag))
        {
            collision.gameObject.GetComponent<MarbleHealth>().TakeDamage(1);

            if (OpponentTag == "Marble_6")
                GameObject.FindGameObjectWithTag("GameManager").GetComponent<Winner>().TotalPlayer2HP--;
            else
                GameObject.FindGameObjectWithTag("GameManager").GetComponent<Winner>().TotalPlayer1HP--;
                
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
