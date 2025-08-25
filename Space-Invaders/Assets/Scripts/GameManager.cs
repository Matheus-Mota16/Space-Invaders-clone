using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public int rows = 5;
    public int cols = 11;
    public float spacing = 1.2f;

    public float horizontalSpeed = 1f; // Velocidade horizontal da horda
    public float verticalStep = 0.5f;  // Quanto a horda desce quando atinge a borda

    private int direction = 1; // 1 para direita, -1 para esquerda

    public GameObject gameOverPanel;
    void Start()
    {
        SpawnEnemies();
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }
    }

    public void GameOver()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
            Time.timeScale = 0f; // Pausa o jogo
        }
    }

    void Update()
    {
        // Move a horda inteira horizontalmente
        transform.Translate(Vector3.right * horizontalSpeed * direction * Time.deltaTime);

        // Verifica se a horda precisa mudar de direção
        CheckForDirectionChange();
    }

    void SpawnEnemies()
    {
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                float xPos = j * spacing - (cols * spacing / 2) + spacing / 2;
                float yPos = i * spacing;
                Vector3 spawnPosition = new Vector3(xPos, 5 - yPos, 0);

                // Cria o inimigo e o torna filho do GameManager
                Instantiate(enemyPrefab, spawnPosition, Quaternion.identity, transform);
            }
        }
    }

    private void CheckForDirectionChange()
    {
        // Encontre o inimigo mais à esquerda e mais à direita
        float rightmostX = float.MinValue;
        float leftmostX = float.MaxValue;

        // Itera por todos os filhos (os inimigos) do GameManager
        // A lista de filhos pode mudar, então é melhor iterar sobre o transform
        foreach (Transform enemy in transform)
        {
            if (enemy.position.x > rightmostX)
            {
                rightmostX = enemy.position.x;
            }
            if (enemy.position.x < leftmostX)
            {
                leftmostX = enemy.position.x;
            }
        }

        // Obtém os limites da tela
        float screenRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;
        float screenLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;

        // Margem de segurança para a borda
        float margin = 0.5f;

        // Se o inimigo mais à direita ou mais à esquerda ultrapassar o limite, inverte a direção
        if ((rightmostX > screenRight - margin && direction == 1) || (leftmostX < screenLeft + margin && direction == -1))
        {
            // Inverte a direção horizontal
            direction *= -1;

            // Move o GameManager (e a horda inteira) para baixo
            transform.Translate(Vector3.down * verticalStep);
        }
    }
}