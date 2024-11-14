using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Weapon : MonoBehaviour
{
    [Header("Weapon Stats")]
    [SerializeField] private float shootIntervalInSeconds = 3f;

    [Header("Bullets")]
    public Bullet bullet;
    [SerializeField] private Transform bulletSpawnPoint;

    [Header("Bullet Pool")]
    private IObjectPool<Bullet> playerObjectPool;

    private readonly bool collectionCheck = false;
    private readonly int defaultCapacity = 30;
    private readonly int maxSize = 100;
    private float timer;
    public Transform parentTransform;

    private void Awake()
    {
        playerObjectPool = new ObjectPool<Bullet>(CreateBullet, OnGetFromPool, OnReleaseToPool, OnDestroyPooledObject, collectionCheck, defaultCapacity, maxSize);
    }

    private void FixedUpdate()
    {
        if (Input.GetButton("Fire1") && Time.time > timer && playerObjectPool != null)
        {
            Bullet bullet = playerObjectPool.Get();

            if (bullet == null)
            {
                return;
            }

            bullet.transform.SetPositionAndRotation(bulletSpawnPoint.position, bulletSpawnPoint.rotation);

            bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(0, bullet.bulletSpeed);

            bullet.Deactivate();

            timer = Time.time + shootIntervalInSeconds;
        }
    }

    private Bullet CreateBullet()
    {
        Bullet bulletInstance = Instantiate(bullet);
        bulletInstance.ObjectPool = playerObjectPool;
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