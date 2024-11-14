using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour
{
    [Header("Bullet Stats")]
    public float bulletSpeed = 20;
    public int damage = 10;
    private Rigidbody2D rb;

    private IObjectPool<Bullet> objectPool;
    public IObjectPool<Bullet> ObjectPool
    {
        get => objectPool;
        set => objectPool = value;
    }

    private Vector2 screenBounds;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    }

    void Update()
    {

    }

    IEnumerator DeactivateRoutine(float delay)
    {
        yield return new WaitForSeconds(delay);
        DeactivateImmediately();
    }

    IEnumerator CheckOutOfBounds()
    {
        while (true)
        {
            if (transform.position.y > screenBounds.y || transform.position.y < -screenBounds.y ||
                transform.position.x > screenBounds.x || transform.position.x < -screenBounds.x)
            {
                DeactivateImmediately();
                yield break;
            }

            yield return null;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != tag)
        {
            // Debug.Log("Tag: " + tag);
            // Debug.Log("Other Tag: " + other.tag);
            StartCoroutine(DeactivateRoutine(0.01f));
        }
    }

    public void Deactivate()
    {
        StartCoroutine(DeactivateRoutine(3f));
        StartCoroutine(CheckOutOfBounds());
    }

    private void DeactivateImmediately()
    {
        if (rb == null) return;
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;
        objectPool.Release(this);
    }
}
