using UnityEngine;

namespace SG
{
    public class BossLocomotion : CharacterManager
    {
        EnemyStats enemyStats;
        Animator bossAnimator;
        public Transform playerTransform;
        public Rigidbody bossRigidBody;
        public float moveSpeed = 3f;
        public float rotationSpeed = 0.5f;
        public float strafeSpeed = 1.5f; // Slower speed for strafing
        public float strafeDistance = 6f; // Start strafing when within this range

        private void Update()
        {
            float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

            if (distanceToPlayer <= strafeDistance)
            {
                StrafeForward();
            }
            else
            {
                MoveTowardsTarget(moveSpeed);
            }
        }

        private void Awake()
        {
            bossAnimator = GetComponentInChildren<Animator>();
            bossRigidBody = GetComponent<Rigidbody>();
        }

        public void MoveTowardsTarget(float speed)
        {
            Vector3 targetPosition = playerTransform.position;
            Vector3 moveDirection = (targetPosition - transform.position).normalized;
            moveDirection.y = 0;
            bossRigidBody.velocity = moveDirection * speed;

            Vector3 localMoveDirection = transform.InverseTransformDirection(moveDirection);
            bossAnimator.SetFloat("Horizontal", localMoveDirection.x, 0.1f, Time.deltaTime);
            bossAnimator.SetFloat("Vertical", Mathf.Clamp(speed / moveSpeed, 0.5f, 1f), 0.1f, Time.deltaTime);

            HandleRotation();
        }

        private void StrafeForward()
        {
            Vector3 targetPosition = playerTransform.position;
            Vector3 moveDirection = (targetPosition - transform.position).normalized;
            moveDirection.y = 0;
            bossRigidBody.velocity = moveDirection * strafeSpeed; // Move forward but slower

            Vector3 localMoveDirection = transform.InverseTransformDirection(moveDirection);
            bossAnimator.SetFloat("Horizontal", localMoveDirection.x, 0.1f, Time.deltaTime);
            bossAnimator.SetFloat("Vertical", Mathf.Clamp(strafeSpeed / moveSpeed, 0.5f, 1f), 0.1f, Time.deltaTime);

            HandleRotation();
        }

        public void HandleRotation()
        {
                Vector3 targetPosition = playerTransform.position;
                Vector3 targetDirection = targetPosition - transform.position;
                targetDirection.y = 0;
                targetDirection.Normalize();

                Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            
        }
    }
}