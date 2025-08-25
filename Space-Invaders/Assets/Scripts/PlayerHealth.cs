using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health = 3;

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("Vida do jogador: " + health);

        if (health <= 0)
        {
            // A destrui��o do player ser� detectada pelo GameEndObserver
            Destroy(gameObject);
        }
    }
}
