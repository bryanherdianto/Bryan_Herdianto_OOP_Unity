using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class HitboxComponent : MonoBehaviour
{
    public HealthComponent health;
    public InvincibilityComponent invincibility;

    // Start is called before the first frame update
    void Start()
    {
        health = GetComponent<HealthComponent>();
        invincibility = GetComponent<InvincibilityComponent>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Damage(Bullet bullet)
    {
        if (bullet != null && health != null && invincibility != null && invincibility.isInvincible == false)
        {
            health.Subtract(bullet.damage);
            // Debug.Log("Bullet hit " + gameObject.name + " for " + bullet.damage + " damage.");
        }
        else if (bullet != null && health != null && invincibility == null)
        {
            health.Subtract(bullet.damage);
            // Debug.Log("Bullet hit " + gameObject.name + " for " + bullet.damage + " damage.");
        }
    }

    public void Damage(int damage)
    {
        if (health != null && invincibility != null && invincibility.isInvincible == false)
        {
            health.Subtract(damage);
            // Debug.Log("Bullet hit " + gameObject.name + " for " + damage + " damage.");
        }
        else if (health != null && invincibility == null)
        {
            health.Subtract(damage);
            // Debug.Log("Bullet hit " + gameObject.name + " for " + damage + " damage.");
        }
    }
}
