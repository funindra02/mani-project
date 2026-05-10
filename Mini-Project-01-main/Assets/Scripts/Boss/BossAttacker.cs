using UnityEngine;

namespace SG
{
    public class BossAttacker : MonoBehaviour
    {
        private Animator anim;
        private PlayerInventory playerInventory;
        private WeaponSlotManager weaponSlotManager;
        private AnimatorManager animatorManager;
        private BossLocomotion bossLocomotion;
        private Transform playerTransform;
        private BossDodgeHandler bossDodgeHandler; // Reference to BossDodgeHandler

        public float attackRange = 5f; // Maximum distance for attack
        public float attackRadius = 1f; // Radius for attack check
        public string[] attackAnimations = { "OH_Light_Attack_01 Boss", "OH_Light_Attack_02 Boss" }; // Attack choices

        private void Awake()
        {
            playerInventory = GetComponent<PlayerInventory>();
            weaponSlotManager = GetComponentInChildren<WeaponSlotManager>();
            anim = GetComponentInChildren<Animator>();
            animatorManager = GetComponentInChildren<AnimatorManager>();
            bossLocomotion = GetComponent<BossLocomotion>();
            playerTransform = bossLocomotion.playerTransform; // Get player reference
            bossDodgeHandler = BossDodgeHandler.Instance; // Get dodge handler reference
        }

        private void Update()
        {
            if (anim.GetBool("isInteracting"))
                return; // Prevent multiple actions at once

            float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

            if (distanceToPlayer < 3f)
            {
                // Check if boss should dodge instead of attacking based on weight
                if (bossDodgeHandler != null && Random.value < bossDodgeHandler.dodgeWeight)
                {
                    bossDodgeHandler.TryDodge();
                    Debug.Log("Dodging instead of Attacking");
                }
                else
                {
                    Attack(playerInventory.rightWeapon);
                    Debug.Log("Attacking");
                }
            }
        }

        private void Attack(WeaponItem weapon)
        {
            if (weapon == null) return;

            // Pick a random attack animation
            string attackAnim = Random.value > 0.5f ? weapon.OH_Light_Attack_1 : weapon.OH_Light_Attack_2;
            weaponSlotManager.attackingWeapon = weapon;
            animatorManager.PlayTargetAnimation(attackAnim, true);
        }
    }
}