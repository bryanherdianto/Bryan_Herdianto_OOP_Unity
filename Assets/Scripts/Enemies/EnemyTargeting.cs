using UnityEngine;

public class EnemyTargeting : Enemy
{
    public float speed = 5f;
    private Vector2 screenBounds;
    private Rigidbody2D rb;
    private Transform player;

    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

        float spawnX = Random.value < 0.5f ? -screenBounds.x - 1f : screenBounds.x + 1f;
        float spawnY = Random.Range(-screenBounds.y + 2f, screenBounds.y - 2f);

        transform.position = new Vector2(spawnX, spawnY);

        rb = GetComponent<Rigidbody2D>();

        player = GameObject.Find("Player").transform; 
    }

    void Update()
    {
        if (player == null) return;

        Debug.Log(player.position);
        Vector2 directionToPlayer = (player.position - transform.position).normalized;

        rb.velocity = directionToPlayer * speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
