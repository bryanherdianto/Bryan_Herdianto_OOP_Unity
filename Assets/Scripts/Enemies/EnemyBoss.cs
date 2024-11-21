using UnityEngine;
using UnityEngine.Pool;

public class EnemyBoss : Enemy
{
    public float speed = 1f;
    private Vector2 screenBounds;
    private Vector2 direction;
    private Rigidbody2D rb;

    [Header("Weapon Stats")]
    [SerializeField] private float shootIntervalInSeconds = 3f;

    [Header("Bullets")]
    public Bullet bullet;
    [SerializeField] private Transform bulletSpawnPoint;

    [Header("Bullet Pool")]
    private IObjectPool<Bullet> enemyObjectPool;

    private readonly bool collectionCheck = false;
    private readonly int defaultCapacity = 30;
    private readonly int maxSize = 100;
    private float timer;
    public Transform parentTransform;
    
    public int level = 4;

    private void Awake()
    {
        enemyObjectPool = new ObjectPool<Bullet>(CreateBullet, OnGetFromPool, OnReleaseToPool, OnDestroyPooledObject, collectionCheck, defaultCapacity, maxSize);
    }

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

    private void FixedUpdate()
    {
        if (Time.time > timer && enemyObjectPool != null)
        {
            Bullet bullet = enemyObjectPool.Get();

            if (bullet == null)
            {
                return;
            }

            bullet.transform.SetPositionAndRotation(bulletSpawnPoint.position, bulletSpawnPoint.rotation);

            bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(rb.velocity.x, -bullet.bulletSpeed);

            bullet.Deactivate();

            timer = Time.time + shootIntervalInSeconds;
        }
    }

    private Bullet CreateBullet()
    {
        Bullet bulletInstance = Instantiate(bullet);
        bulletInstance.ObjectPool = enemyObjectPool;
        return bulletInstance;
    }

    private void OnGetFromPool(Bullet pooledObject)
    {
        pooledObject.gameObject.SetActive(true);
    }

    private void OnReleaseToPool(Bullet pooledObject)
    {
        pooledObject.gameObject.SetActive(false);
    }

    private void OnDestroyPooledObject(Bullet pooledObject)
    {
        Destroy(pooledObject.gameObject);
    }
}
