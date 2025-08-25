using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // Vari�veis para a vida do inimigo
    public int health = 1;

    // Vari�veis para o disparo
    public GameObject enemyLaserPrefab;   // Prefab do laser do inimigo
    public Transform shootPoint;         // Ponto de onde o laser vai sair

    // Vari�veis para o cooldown
    public float shootingCooldown = 3f;  // Intervalo m�nimo entre os disparos
    private float nextShotTime;          // Hora do pr�ximo disparo

    void Start()
    {
        // Define o tempo do primeiro disparo de forma aleat�ria
        // para que os inimigos n�o atirem todos ao mesmo tempo.
        nextShotTime = Time.time + Random.Range(1f, shootingCooldown);
    }

    void Update()
    {
        // Verifica se j� � hora de atirar
        if (Time.time > nextShotTime)
        {
            Shoot();
            // Reseta o temporizador para o pr�ximo disparo
            nextShotTime = Time.time + shootingCooldown;
        }
    }

    // M�todo para fazer o disparo
    void Shoot()
    {
        // Instancia o laser do inimigo a partir do prefab
        GameObject newLaser = Instantiate(enemyLaserPrefab, shootPoint.position, Quaternion.identity);

        // Define que este � um laser do inimigo
        newLaser.GetComponent<Laser>().isEnemyLaser = true;
    }

    // M�todo para receber dano (j� implementado)
    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}