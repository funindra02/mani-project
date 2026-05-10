using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SG
{
	[CreateAssetMenu(menuName = "Items / Weapon Item")]
	public class WeaponItem : Item
	{
		public GameObject modelPrefab;
		public bool isUnarmed;

		[Header("Idle Animations")]
		public string RIGHT_HAND_IDLE;
		public string LEFT_HAND_IDLE;

		[Header("One Handed Attack Animations")]
		public string OH_Light_Attack_1;
		public string OH_Light_Attack_2;
		public string OH_Heavy_Attack_1;
	}
}