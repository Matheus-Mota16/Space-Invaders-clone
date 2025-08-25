using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public GameObject laserPrefab;
    public Transform shootPoint;

    private float minX, maxX;

    void Start()
    {
        // Define os limites de movimento da tela
        Vector3 playerSize = GetComponent<Renderer>().bounds.size;
        minX = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + playerSize.x / 2;
        maxX = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - playerSize.x / 2;
    }

    void Update()
    {
        // Movimentação do jogador
        float horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * horizontalInput * moveSpeed * Time.deltaTime);

        // Limita o movimento dentro da tela
        Vector3 currentPos = transform.position;
        currentPos.x = Mathf.Clamp(currentPos.x, minX, maxX);
        transform.position = currentPos;

        // Disparo de projéteis
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        Instantiate(laserPrefab, shootPoint.position, Quaternion.identity);
    }
}