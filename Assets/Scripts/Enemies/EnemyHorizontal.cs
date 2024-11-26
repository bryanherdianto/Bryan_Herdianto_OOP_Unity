using UnityEngine;

public class EnemyHorizontal : Enemy
{
    public float speed = 1f;
    private Vector2 screenBounds;
    private Vector2 direction;
    private Rigidbody2D rb;
    
    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

        float spawnX = Random.value < 0.5f ? -screenBounds.x - 1f : screenBounds.x + 1f;
        float spawnY = Random.Range(-screenBounds.y + 2f, screenBounds.y - 2f);

        transform.position = new Vector2(spawnX, spawnY);
        direction = spawnX < 0 ? Vector2.right : Vector2.left;

        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rb.velocity = direction * speed;

        if (transform.position.x > screenBounds.x + 1f && direction == Vector2.right)
        {
            direction = Vector2.left;
        }
        else if (transform.position.x < -screenBounds.x - 1f && direction == Vector2.left)
        {
            direction = Vector2.right;
        }
    }
}
