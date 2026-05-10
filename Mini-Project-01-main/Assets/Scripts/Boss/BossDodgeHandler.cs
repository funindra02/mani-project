using UnityEngine;

namespace SG
{
    public class BossDodgeHandler : MonoBehaviour
    {
        public static BossDodgeHandler Instance; // Singleton

        private AnimatorManager animatorManager;
        private bool isDodging = false;
        public float dodgeWeight = 0.2f; // Base dodge probability
        public float dodgeCooldown = 1f; // Cooldown before dodging again
        public float dodgeCheckInterval = 2f; // How often the boss considers dodging
        public float dodgeDistanceThreshold = 6f; // Player needs to be within this distance to dodge

        private Transform playerTransform;
        private float dodgeCheckTimer = 0f;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);

            animatorManager = GetComponentInChildren<AnimatorManager>();
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }

        private void Update()
        {
            dodgeCheckTimer += Time.deltaTime;

            if (dodgeCheckTimer >= dodgeCheckInterval)
            {
                dodgeCheckTimer = 0f; // Reset timer
                TryDodge();
            }
        }

        public void UpdateDodgeWeight(float newWeight)
        {
            dodgeWeight = newWeight;
        }

        public void TryDodge()
        {
            if (isDodging) return; // Prevent dodging again if already dodging

            float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
            Debug.Log(dodgeWeight);
            // Dodge if the player is close and based on weighted chance
            if (distanceToPlayer <= dodgeDistanceThreshold && Random.value < dodgeWeight)
            {
                Debug.Log("Boss is Dodging");
                PerformDodge();
            }
        }

        private void PerformDodge()
        {
            isDodging = true;
            string dodgeAnimation = Random.value > 0.5f ? "Dodge_Right" : "Dodge_Left";
            animatorManager.PlayTargetAnimation(dodgeAnimation, true);

            Invoke(nameof(ResetDodge), dodgeCooldown);
        }

        private void ResetDodge()
        {
            isDodging = false;
        }
    }
}
