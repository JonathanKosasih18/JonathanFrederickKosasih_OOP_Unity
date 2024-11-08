using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField] Weapon weaponHolder;
    [SerializeField] GameObject portal;
    Weapon weapon;
    public static WeaponPickup Instance { get; private set; }

    void Awake()
    {
        weapon = Instantiate(weaponHolder, transform.position, transform.rotation);
    }

    void Start()
    {
        if (weapon != null)
        {
            TurnVisual(false, weapon);
        }
        Debug.Log("WeaponPickup successfuly instantiated!");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Weapon successfuly picked up!");
            if (weapon != null)
            {
                Weapon currentWeapon = other.GetComponentInChildren<Weapon>();
                if (currentWeapon != null)
                {
                    currentWeapon.gameObject.SetActive(false);
                }
                Weapon newWeapon = Instantiate(weapon, other.transform.position, Quaternion.identity);
                newWeapon.transform.SetParent(other.transform);
                newWeapon.transform.localPosition = new Vector3(0, 0, 1);
                newWeapon.gameObject.SetActive(true);
            }
            else Debug.LogWarning("Weapon is not instantiated in WeaponPickup.");
            if (portal != null)
            {
                portal.SetActive(true);
            }
        }
    }


    void TurnVisual(bool on)
    {
        weapon.gameObject.SetActive(on);
    }

    void TurnVisual(bool on, Weapon weapon)
    {
        if (weapon != null)
        {
            weapon.gameObject.SetActive(on);
        }
    }
}