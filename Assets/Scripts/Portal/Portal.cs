using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float rotateSpeed;
    private Vector2 newPosition;
    private LevelManager levelManager;
    private bool firstTime = false;

    private GameObject weapon1;
    private GameObject weapon2;
    private GameObject weapon3;
    private GameObject weapon4;

    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();

        GameObject weaponRack = GameObject.Find("WeaponRack");
        if (weaponRack != null)
        {
            weapon1 = weaponRack.transform.Find("Weapon1")?.gameObject;
            weapon2 = weaponRack.transform.Find("Weapon2")?.gameObject;
            weapon3 = weaponRack.transform.Find("Weapon3")?.gameObject;
            weapon4 = weaponRack.transform.Find("Weapon4")?.gameObject;

            Debug.Log($"Weapon1 active: {weapon1?.activeSelf}");
            Debug.Log($"Weapon2 active: {weapon2?.activeSelf}");
            Debug.Log($"Weapon3 active: {weapon3?.activeSelf}");
            Debug.Log($"Weapon4 active: {weapon4?.activeSelf}");
        }
        else
        {
            Debug.LogError("WeaponRack not found!");
        }

        ChangePosition();
    }

    void Update()
    {
        if (AnyWeaponPickupActive())
        {
            if (!firstTime)
            {
                firstTime = true;
                transform.position = new Vector2(0, -2f);
            }

            Debug.Log($"Current Position: {transform.position}, Target: {newPosition}");

            transform.position = Vector2.MoveTowards(transform.position, newPosition, speed * Time.deltaTime);
            transform.Rotate(Vector3.forward * rotateSpeed * Time.deltaTime);

            if (Vector2.Distance(transform.position, newPosition) < 0.1f)
            {
                ChangePosition();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && AnyWeaponPickupActive())
        {
            levelManager.LoadScene("Main");
        }
    }

    void ChangePosition()
    {
        float x = Random.Range(-10f, 10f);
        float y = Random.Range(-5f, 4.4f);
        newPosition = new Vector2(x, y);
    }

    bool AnyWeaponPickupActive()
    {
        return (weapon1 != null && weapon1.activeSelf) ||
               (weapon2 != null && weapon2.activeSelf) ||
               (weapon3 != null && weapon3.activeSelf) ||
               (weapon4 != null && weapon4.activeSelf);
    }
}
