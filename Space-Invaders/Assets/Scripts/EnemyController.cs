using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // Variáveis para a vida do inimigo
    public int health = 1;

    // Variáveis para o disparo
    public GameObject enemyLaserPrefab;   // Prefab do laser do inimigo
    public Transform shootPoint;         // Ponto de onde o laser vai sair

    // Variáveis para o cooldown
    public float shootingCooldown = 3f;  // Intervalo mínimo entre os disparos
    private float nextShotTime;          // Hora do próximo disparo

    void Start()
    {
        // Define o tempo do primeiro disparo de forma aleatória
        // para que os inimigos não atirem todos ao mesmo tempo.
        nextShotTime = Time.time + Random.Range(1f, shootingCooldown);
    }

    void Update()
    {
        // Verifica se já é hora de atirar
        if (Time.time > nextShotTime)
        {
            Shoot();
            // Reseta o temporizador para o próximo disparo
            nextShotTime = Time.time + shootingCooldown;
        }
    }

    // Método para fazer o disparo
    void Shoot()
    {
        // Instancia o laser do inimigo a partir do prefab
        GameObject newLaser = Instantiate(enemyLaserPrefab, shootPoint.position, Quaternion.identity);

        // Define que este é um laser do inimigo
        newLaser.GetComponent<Laser>().isEnemyLaser = true;
    }

    // Método para receber dano (já implementado)
    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}