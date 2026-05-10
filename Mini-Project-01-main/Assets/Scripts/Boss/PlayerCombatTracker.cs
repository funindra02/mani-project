using UnityEngine;
using System.Collections.Generic;

namespace SG
{
    public class PlayerCombatTracker : MonoBehaviour
    {
        public static PlayerCombatTracker Instance; // Singleton for easy access

        private List<float> attackTimestamps = new List<float>(); // Stores attack times
        private Dictionary<string, int> attackTypeCount = new Dictionary<string, int>(); // Tracks attack patterns

        public float aggressionThreshold = 3f; // Time window for "aggressive" player attacks
        public int attackPatternThreshold = 3; // If the same attack is used this many times, increase dodge weight

        private int missedAttacks = 0; // Tracks missed attacks
        private int missedAttackThreshold = 3; // If this many attacks miss, reduce dodge weight

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
        }

        public void RegisterPlayerHit()
        {
            float currentTime = Time.time;
            attackTimestamps.Add(currentTime);

            // Remove old attacks beyond the threshold time
            attackTimestamps.RemoveAll(time => currentTime - time > aggressionThreshold);

            // Notify the boss to adjust dodge behavior
            BossDodgeHandler.Instance?.UpdateDodgeWeight(CalculateDodgeWeight());
        }

        private float CalculateDodgeWeight()
        {
            float dodgeWeight = 0.2f; // Base dodge weight

            // If the player is attacking frequently in a short time
            if (attackTimestamps.Count >= 3)
                dodgeWeight += 0.3f;

            return Mathf.Clamp(dodgeWeight, 0.2f, 0.9f); // Prevent extreme values
        }

    }
}