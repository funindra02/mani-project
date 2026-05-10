using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SG
{
    public class PlayerStats : CharacterStats
    {
        public int staminaLevel = 10;
        public int maxStamina;

        public PlayerManager playerManager;
        public HealthBar healthBar;

        AnimatorHandler animatorHandler;

        private void Awake()
        {
            healthBar = FindObjectOfType<HealthBar>();
            playerManager = GetComponent<PlayerManager>(); 
            animatorHandler = GetComponentInChildren<AnimatorHandler>();
            //staminaBar = FindObjectOfType<StaminaBar>();
        }

        private void Start()
        {
            maxHealth = SetMaxHealthFromHealthLevel();
            currentHealth = maxHealth;
            healthBar.SetMaxHealth(maxHealth);
            healthBar.SetCurrentHealth(currentHealth);
        }
        private int SetMaxHealthFromHealthLevel()
        {
            maxHealth = healthLevel * 10;
            return maxHealth;
        }


        public void TakeDamage(int damage)
        {
            if (isDead)
                return;
            if (playerManager.isInvulnerable)
                return;
            currentHealth = currentHealth - damage;

            healthBar.SetCurrentHealth(currentHealth);

            animatorHandler.PlayTargetAnimation("Damage", true);

            if(currentHealth <= 0)
            {
                currentHealth = 0;
                animatorHandler.PlayTargetAnimation("Death", true);
                isDead = true;
            }
        }
    }
}