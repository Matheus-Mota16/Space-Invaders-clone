using UnityEngine;

public class GameEndObserver : MonoBehaviour
{
    private GameObject player;
    private GameManager gameManager;

    void Start()
    {
        // Encontra o objeto do jogador na cena
        player = GameObject.FindGameObjectWithTag("Player");

        // Encontra o script GameManager
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        // Se o objeto do jogador não existe mais, ative a tela de game over
        if (player == null && gameManager != null)
        {
            gameManager.GameOver();
            // Desativa este script para que a função não seja chamada múltiplas vezes
            enabled = false;
        }
    }
}