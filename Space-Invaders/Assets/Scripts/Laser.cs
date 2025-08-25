using UnityEngine;

public class Laser : MonoBehaviour
{
    public float moveSpeed = 10f;
    public bool isEnemyLaser = false;

    // O dano que o projétil causa.
    public int damage = 1;

    void Update()
    {
        // Movimento do laser para cima (jogador) ou para baixo (inimigo)
        if (isEnemyLaser)
        {
            transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
        }

        // Destrói o laser ao sair da tela para não sobrecarregar o jogo.
        Vector3 screenPos = Camera.main.WorldToViewportPoint(transform.position);
        if (screenPos.y > 1 || screenPos.y < 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // A colisão destrói o laser em todos os casos, a menos que ele passe por algo.
        // Adicionaremos a lógica de dano baseada nas tags.

        // Lógica de colisão do laser do JOGADOR
        if (!isEnemyLaser)
        {
            // Se o laser do jogador atingir um inimigo
            if (other.CompareTag("Enemy"))
            {
                EnemyController enemy = other.GetComponent<EnemyController>();
                if (enemy != null)
                {
                    enemy.TakeDamage(damage);
                }
                Destroy(gameObject);
            }
        }
        // Lógica de colisão do laser do INIMIGO
        else // isEnemyLaser == true
        {
            // Se o laser do inimigo atingir o jogador
            if (other.CompareTag("Player"))
            {
                PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(damage);
                }
                Destroy(gameObject);
            }
            // Se o laser do inimigo atingir uma barreira
            else if (other.CompareTag("Barrier"))
            {
                BarrierHealth barrierHealth = other.GetComponent<BarrierHealth>();
                if (barrierHealth != null)
                {
                    barrierHealth.TakeDamage(damage);
                }
                Destroy(gameObject);
            }
        }
    }
}