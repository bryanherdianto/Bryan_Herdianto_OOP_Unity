using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    public float maxHealth = 100;
    private float health;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    public float getHealth
    {
        get { return health; }
    }

    public void Subtract(float value)
    {
        health -= value;
        if (health < 0)
        {
            Destroy(gameObject);
        }
    }
}
