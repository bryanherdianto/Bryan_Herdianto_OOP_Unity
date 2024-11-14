using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class AttackComponent : MonoBehaviour
{
    public Bullet bullet;
    public int damage = 0;

    private int appliedDamage;

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.tag);

        if (bullet != null && bullet.tag == other.tag)
        {
            return;
        }
        else if (damage != 0 && other.tag == "Enemy")
        {
            return;
        }

        HitboxComponent hitbox = other.GetComponent<HitboxComponent>();

        if (bullet != null)
        {
            appliedDamage = bullet.damage;
        }
        else if (damage != 0)
        {
            appliedDamage = damage;
        }

        hitbox?.Damage(appliedDamage);

        InvincibilityComponent invincibility = other.GetComponent<InvincibilityComponent>();

        if (invincibility != null)
        {
            invincibility.StartInvincibility();
        }
    }
}