using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField] public Weapon weaponHolder;
    private Weapon weapon;
    private static Weapon currentWeapon;

    void Awake()
    {
        weapon = weaponHolder;
    }

    void Start()
    {
        if (weapon != null)
        {
            TurnVisual(false, weapon);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (currentWeapon != null && weapon != currentWeapon)
            {
                TurnVisual(false, currentWeapon);
            }

            TurnVisual(true);
            currentWeapon = weapon;

            weapon.transform.SetParent(other.transform);
            weapon.transform.localPosition = Vector3.zero;
            weapon.transform.localRotation = Quaternion.identity;
        }
    }

    void TurnVisual(bool on, Weapon targetWeapon)
    {
        targetWeapon?.gameObject.SetActive(on);
    }

    void TurnVisual(bool on)
    {
        weapon.gameObject.SetActive(on);
    }
}
