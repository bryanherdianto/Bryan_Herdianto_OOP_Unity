using UnityEngine;

public class EnemyForward : Enemy
{
    public float speed = 1f;
    private Vector2 screenBounds;
    private Vector2 direction;
    private Rigidbody2D rb;
    
    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

        float spawnX = Random.Range(-screenBounds.x + 2f, screenBounds.x - 2f);
        float spawnY = Random.value < 0.5f ? -screenBounds.y - 1f : screenBounds.y + 1f;

        transform.position = new Vector2(spawnX, spawnY);
        direction = spawnY < 0 ? Vector2.up : Vector2.down;

        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rb.velocity = direction * speed;

        if (transform.position.y > screenBounds.y + 1f && direction == Vector2.up)
        {
            direction = Vector2.down;
        }
        else if (transform.position.y < -screenBounds.y - 1f && direction == Vector2.down)
        {
            direction = Vector2.up;
        }
    }
}
