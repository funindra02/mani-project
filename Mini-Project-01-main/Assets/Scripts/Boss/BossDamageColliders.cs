using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SG
{
    public class BossDamageCollider : MonoBehaviour
    {

        public Collider damageCollider;

        public int currentWeaponDamage = 25;

        private void Awake()
        {
            damageCollider = GetComponent<Collider>();
            damageCollider.gameObject.SetActive(true);
            damageCollider.isTrigger = true;
            damageCollider.enabled = false;
        }

        public void EnableDamageCollider()
        {
            damageCollider.enabled = true;
        }

        public void DisableDamageCollider()
        {
            damageCollider.enabled = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerStats playerStats))
            {
                playerStats.TakeDamage(currentWeaponDamage);
            }
        }
    }
}