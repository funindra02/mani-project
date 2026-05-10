using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SG
{
	public class WeaponSlotManager : MonoBehaviour
	{
		WeaponHolderSlot leftHandSlot;
		WeaponHolderSlot rightHandSlot;

		DamageCollider leftHandDamageCollider;
		DamageCollider rightHandDamageCollider;

		public WeaponItem attackingWeapon;

		Animator animator;

		PlayerStats playerStats;

		public void Awake()
		{
			animator = GetComponent<Animator>();
			playerStats = GetComponentInParent<PlayerStats>();

			WeaponHolderSlot[] weaponHolderSlots = GetComponentsInChildren<WeaponHolderSlot>();
			foreach (WeaponHolderSlot weaponSlot in weaponHolderSlots)
			{
				if(weaponSlot.isLeftHandSlot)
				{
					leftHandSlot = weaponSlot;
				}
				else if (weaponSlot.isRightHandSlot)
				{
					rightHandSlot = weaponSlot;
				}
			}
		}

		public void LoadWeaponOnSLot(WeaponItem weaponItem, bool isLeft)
		{
			if (isLeft)
			{
				leftHandSlot.LoadWeaponModel(weaponItem);
				LoadLeftWeaponDamageCollider();


                if (weaponItem != null)
                {
					animator.CrossFade(weaponItem.LEFT_HAND_IDLE, 0.2f);
                }
				else
                {
					animator.CrossFade("Left Arm Empty", 0.2f);
                }
			}
			else
			{
				rightHandSlot.LoadWeaponModel(weaponItem);
				LoadRightWeaponDamageCollider();

				if (weaponItem != null)
				{
					animator.CrossFade(weaponItem.RIGHT_HAND_IDLE, 0.2f);
				}
				else
				{
					animator.CrossFade("Right Arm Empty", 0.2f);
				}
			}

        }

        #region Handle Weapon's Damage Collider

        private void LoadLeftWeaponDamageCollider()
        {
			leftHandDamageCollider = leftHandSlot.currentWeaponModel.GetComponentInChildren<DamageCollider>();
        }

		private void LoadRightWeaponDamageCollider()
		{
			rightHandDamageCollider = rightHandSlot.currentWeaponModel.GetComponentInChildren<DamageCollider>();
		}

		public void OpenRightDamageCollider()
        {
			rightHandDamageCollider.EnableDamageCollider();
        }

		public void OpenLeftDamageCollider()
		{
			leftHandDamageCollider.EnableDamageCollider();
		}

		public void CloseRightDamageCollider()
		{
			rightHandDamageCollider.DisableDamageCollider();
		}

		public void CloseLeftDamageCollider()
		{
			leftHandDamageCollider.DisableDamageCollider();
		}

        #endregion

    }
}
