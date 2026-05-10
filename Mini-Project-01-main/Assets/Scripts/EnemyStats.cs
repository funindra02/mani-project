using System.Collections;
using UnityEngine;

namespace SG
{
    public class EnemyStats : CharacterStats
    {
        public BossLocomotion bossLocomotion;
        public BossDodgeHandler bossDodgeHandler;
        public BossHealthBar bossHealthBar; // Reference to boss health UI
        public bool isBoss = false; // Mark if this enemy is a boss

        private Animator animator;

        private void Awake()
        {
            bossDodgeHandler = GetComponent<BossDodgeHandler>();
            bossLocomotion = GetComponent<BossLocomotion>();
            animator = GetComponentInChildren<Animator>();

            if (isBoss) // Only assign health bar if this is a boss
            {
                bossHealthBar = FindObjectOfType<BossHealthBar>();
            }
        }

        private void Start()
        {
            maxHealth = SetMaxHealthFromHealthLevel();
            currentHealth = maxHealth;

            if (isBoss && bossHealthBar != null)
            {
                bossHealthBar.SetMaxHealth(maxHealth);
                bossHealthBar.SetCurrentHealth(currentHealth);
            }
        }

        private int SetMaxHealthFromHealthLevel()
        {
            return healthLevel * 10; // Modify this for balance if needed
        }

        public void TakeDamage(int damage)
        {
            if (isDead)
                return;

            currentHealth -= damage;

            if (isBoss && bossHealthBar != null)
            {
                bossHealthBar.SetCurrentHealth(currentHealth);
            }

            // Notify PlayerCombatTracker that the boss was hit
            if (isBoss)
            {
                PlayerCombatTracker.Instance?.RegisterPlayerHit();
            }

            if (currentHealth <= 0)
            {
                Die();
            }
        }


        private void Die()
        {
            if (isDead)
                return;

            isDead = true;
            animator.Play("Death");

            // Disable AI scripts to prevent movement/dodging
            if (bossLocomotion != null) bossLocomotion.enabled = false;
            if (bossDodgeHandler != null) bossDodgeHandler.enabled = false;

            // Destroy the boss after 5 seconds
            Invoke(nameof(DestroyBoss), 5f);
        }

        private void DestroyBoss()
        {
            Destroy(gameObject);
        }
    }
}
