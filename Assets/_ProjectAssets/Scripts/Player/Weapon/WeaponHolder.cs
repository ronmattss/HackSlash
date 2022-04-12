using System;
using MoreMountains.Feedbacks;
using UnityEngine;

namespace ProjectAssets.Scripts.Player
{
    public class WeaponHolder : MonoBehaviour
    {
        public GameObject currentWeaponGO;
        private GameObject _currentWeaponGO;

        private Weapon currentWeapon;

        public Action<int,int> OnWeaponAttack;
        public Action OnAttack;

        [SerializeField] GameObject weaponHolderL;
        [SerializeField] GameObject weaponHolderR;
        [SerializeField] GameObject weaponHolderTwoHanded;
        [SerializeField] private MMFeedbacks _feedbacks;

        public int weaponAttackStatus = 0;


        void Awake()
        {
            if (currentWeaponGO == null) return;
            _currentWeaponGO = Instantiate(currentWeaponGO, this.transform);
            currentWeapon = _currentWeaponGO.GetComponent<Weapon>();
            currentWeapon.weaponHolder = this;
            currentWeapon.InitializeWeapon();
            currentWeapon.RegisterWeaponLocation(this);
            SetWeaponLocation(currentWeapon.GetWeaponType());
        }
        
        
        
        
        void WeaponAttackEnable(int id)
        {
            Debug.Log("Where this Activated: "+this.transform.name);
            Debug.Log("Weapon attack Listeners: "+OnWeaponAttack.GetInvocationList().Length);
            this.OnWeaponAttack?.Invoke(1,id);
            Reset();
         //   Debug.Log($"Weapon Attack: {id} Weapon Attack Status : {weaponAttackStatus}");
        }
        
        void WeaponAttackDisable(int id)
        {
            OnWeaponAttack?.Invoke(0,id);
            Reset();
        }



        public void ResetWeapon(int id)
        {
            OnWeaponAttack?.Invoke(0,id);
            Reset();
        }

        public void ResetTarget()
        {
            currentWeapon.ResetHitTarget();
        }

        void Reset()
        {
            OnAttack?.Invoke();
        }

        void SetWeaponLocation(WeaponType type)
        {
            switch (type)
            {
                case WeaponType.DualWield:
                    SetWeaponTransform(currentWeapon.GetLeftWeaponObject(), weaponHolderL);
                    SetWeaponTransform(currentWeapon.GetRightWeaponObject(), weaponHolderR);
                    break;
                case WeaponType.TwoHanded:
                    SetWeaponTransform(currentWeapon.GetTwoHandedWeaponObject(), weaponHolderTwoHanded);

                    break;
                case WeaponType.Ranged:
                    SetWeaponTransform(currentWeapon.GetTwoHandedWeaponObject(), weaponHolderTwoHanded);
                    break;
                case WeaponType.OneHanded:
                {
                    SetWeaponTransform(currentWeapon.GetRightWeaponObject(), weaponHolderR);
                    currentWeapon.GetRightWeaponObject().name = "RightHand Weapon of: "+ this.transform.root.name;
                    break;
                }
                case WeaponType.Unarmed:
                    SetWeaponTransform(currentWeapon.GetLeftWeaponObject(), weaponHolderL);
                    SetWeaponTransform(currentWeapon.GetRightWeaponObject(), weaponHolderR);
                    break;

            }
        }

        void SetWeaponTransform(GameObject weapon, GameObject positionParent)
        {
            weapon.transform.parent = positionParent.transform;
            weapon.transform.position = positionParent.transform.position;
            weapon.transform.rotation = positionParent.transform.rotation;
        }


    }
}