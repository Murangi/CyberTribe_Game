using UnityEngine;

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
            // Debug.Log($"{gameObject.name} was hit! Remaining health: {health}\t{collision.gameObject.name}");

            // Check if health is zero or less
            if (health <= 0)
            {
                DestroyMarble();
            }
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
    }

    private void DestroyMarble()
    {
        Debug.Log($"{gameObject.name} is destroyed!");
        Destroy(gameObject);
    }
}
