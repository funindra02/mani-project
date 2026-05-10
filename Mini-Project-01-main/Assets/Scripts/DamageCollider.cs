using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SG
{
    public class DamageCollider : MonoBehaviour
    {
        public GameObject character;
        public Collider damageCollider;
        public Collider characterCollider;

        public int currentWeaponDamage = 25;

        private void Awake()
        {
            damageCollider = GetComponent<Collider>();
            damageCollider.gameObject.SetActive(true);
            damageCollider.isTrigger = true;
            damageCollider.enabled = false;

            Transform rootTransform = transform.root; // Get the root object (the boss)

            if (rootTransform.CompareTag("Enemy")) // Ensure this is an enemy's weapon
            {
                Collider[] bossColliders = rootTransform.GetComponentsInParent<Collider>(); // Get all boss colliders

                foreach (Collider bossCollider in bossColliders)
                {
                    Physics.IgnoreCollision(damageCollider, bossCollider);
                }
            }
        }
        public void EnableDamageCollider()
        {
            damageCollider.enabled = true;
        }

        public void DisableDamageCollider()
        {
            damageCollider.enabled = false;
        }

        private void OnTriggerEnter(Collider collision)
        {
            // Get the root of the object this script is attached to (likely the boss)
            Transform rootTransform = transform.root;

            if (rootTransform.CompareTag("Enemy") && collision.CompareTag("Player"))
            {
                PlayerStats playerStats = collision.GetComponent<PlayerStats>();
                if (playerStats != null)
                {
                    playerStats.TakeDamage(currentWeaponDamage);
                }
            }

            if (rootTransform.CompareTag("Player") && collision.CompareTag("Enemy"))
            {
                EnemyStats enemyStats = collision.GetComponent<EnemyStats>();
                if (enemyStats != null)
                {
                    enemyStats.TakeDamage(currentWeaponDamage);
                }
            }
        }

    }
}
