using UnityEngine;

namespace ProjectAssets.Scripts.Player
{
    [CreateAssetMenu(fileName = "New Weapon Data", menuName = "Weapon/Weapon Data", order = 0)]
    public class WeaponData : ScriptableObject
    {
        public string weaponName;
        public WeaponType weaponType;
        public int damage;
        public GameObject leftWeapon;
        public GameObject rightWeapon;
        public GameObject twoHandedWeapon;
        public WeaponEffects effects;
        public GameObject hitPrefab;
        public Animator animator; // Subject to Change
    }
}