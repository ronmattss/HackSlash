using System;
using JetBrains.Annotations;
using UnityEngine;
using MoreMountains;
using MoreMountains.Feedbacks;
using Unity.VisualScripting;

namespace ProjectAssets.Scripts.Player
{
    /// <summary>
    /// Weapon holds the data for the weapon
    /// Has Weapon Component and Weapon Collider
    /// Has Weapon Data
    /// This Belongs to a Weapon GAMEOBJECT where which can be attached to a Player
    /// </summary>
    public class Weapon : MonoBehaviour
    {
        WeaponColliderController weaponCollider;


        public WeaponType weaponType;
        public int damage;
        public GameObject leftWeapon;
        public GameObject rightWeapon;
        public GameObject twoHandedWeapon;
        public WeaponEffects effects;
        public WeaponHolder weaponHolder;
        // used to know if the weapon is on the left side or right side, to know what weapon to activate in the animation
        private WeaponColliderController _weaponObjectLeft;
        private WeaponColliderController _weaponObjectRight;
        private WeaponColliderController _weaponObjectTwoHanded;

        private GameObject _leftWeapon;
        private GameObject _rightWeapon;
        private GameObject _twoHandedWeapon;
        
        [SerializeField] private AudioSource _audioSource;

        //weapon properties
        private int _damage;
        private WeaponType _weaponType;
        [CanBeNull] private Collider _currentTarget;
        private Collider _lastTarget;

        void OnHitTarget(Collider target, Vector3 hitPoint)
        {
           // if (_currentTarget == target) return;
            _currentTarget = target;
            if (target == null)
            {
                ResetHitTarget();
                
            }
            else if (target.GetComponent<IDamageable>() != null && hitPoint != Vector3.zero)
            {
                if (_lastTarget == target) return;
                target.GetComponent<IDamageable>().OnHit(weaponHolder.transform.gameObject,_damage);
                SpawnEffect(hitPoint);
                Debug.Log("Hit From: " + gameObject.name+ " " + hitPoint);
                _lastTarget = target;
                
            }
            _currentTarget = null;
            
        }


        public WeaponType GetWeaponType() => _weaponType;
        public GameObject GetLeftWeaponObject() => _leftWeapon;
        public GameObject GetRightWeaponObject() => _rightWeapon;
        public GameObject GetTwoHandedWeaponObject() => _twoHandedWeapon;

        public void SetLeftWeaponActive(int active,int id = 0)
        {
            _weaponObjectLeft.isWeaponActive = Convert.ToBoolean(active);
            if (active == 0)
            {
                ResetHitTarget();
            }
            if (active != 1) return;
            if (effects.hitSound != null)
            {
                _audioSource = _weaponObjectLeft.GetComponent<AudioSource>();

                PlayEffectsSound(effects.swooshSound);
            }
        }

        public void SetRightWeaponActive(int active,int id = 1)
        {
            
            _weaponObjectRight.isWeaponActive = Convert.ToBoolean(active);
            if (active == 0)
            {
                ResetHitTarget();
            }
            if (active != 1) return;
            if (effects.hitSound != null)
            {
                _audioSource = _weaponObjectRight.GetComponent<AudioSource>();
                PlayEffectsSound(effects.swooshSound);
            }
        }

        public void SetTwoHandedWeaponActive(int active,int id = 2)
        {
            _weaponObjectRight.isWeaponActive = Convert.ToBoolean(active);
                        if (active == 0)
            {
                ResetHitTarget();
            }
            if (active != 1) return;
            if (effects.hitSound != null)
            {
                _audioSource = _weaponObjectRight.GetComponent<AudioSource>();
                PlayEffectsSound(effects.swooshSound);
            }
        }
        
        public void SpawnEffect(Vector3 hitPoint)
        {
            // spawn effect
            var effect = Instantiate(effects.hitEffect, hitPoint, Quaternion.identity);
            Destroy(effect, 1f);
        }

        public void PlayEffectsSound(AudioClip clip)
        {
            // play sound
            if (clip == null) return;
            _audioSource.clip = clip;
            _audioSource.Play();
        }

        public void InitializeWeapon()
        {
            _audioSource = gameObject.GetComponent<AudioSource>();
            SpawnWeapons();
            _weaponType = weaponType;
            _damage = damage;
            switch (weaponType)
            {
                case WeaponType.DualWield:
                    SetWeapons(_leftWeapon.GetComponent<WeaponColliderController>(),
                        _rightWeapon.GetComponent<WeaponColliderController>());
                    break;
                case WeaponType.TwoHanded:
                    SetWeapons(null, null, _twoHandedWeapon.GetComponent<WeaponColliderController>());
                    break;
                case WeaponType.Ranged:
                    SetWeapons(null, null, _twoHandedWeapon.GetComponent<WeaponColliderController>());
                    break;
                case WeaponType.OneHanded:
                {
                    SetWeapons(null, _rightWeapon.GetComponent<WeaponColliderController>());
                    break;
                }
            }
            
            // then set appropriate Animations
            
        }

      public  void ResetHitTarget()
        {
            //reset current target to null when the weapon is not active
            // if(_currentTarget != null)
                _currentTarget = null;
                _lastTarget = null;
//                Debug.Log("Current Target: "+_currentTarget);
            
        }

        public void RegisterWeaponLocation(WeaponHolder holder)
        {
            switch (weaponType)
            {
                case WeaponType.DualWield:
                    holder.OnWeaponAttack += SetLeftWeaponActive;
                    holder.OnWeaponAttack += SetRightWeaponActive;
                    _weaponObjectLeft.OnMeleeHit += OnHitTarget;
                    _weaponObjectRight.OnMeleeHit += OnHitTarget;

                    break;
                case WeaponType.TwoHanded:
                    holder.OnWeaponAttack += SetTwoHandedWeaponActive;
                    _weaponObjectTwoHanded.OnMeleeHit += OnHitTarget;
                    break;
                case WeaponType.Ranged:
                    holder.OnWeaponAttack += SetTwoHandedWeaponActive;
                    break;
                case WeaponType.OneHanded:
                {
                    holder.OnWeaponAttack += SetRightWeaponActive;
                    _weaponObjectRight.OnMeleeHit += OnHitTarget;

                    break;
                }
            }
            holder.OnAttack += ResetHitTarget;

        }

        public void UnRegisterEvents(WeaponHolder holder)
        {
            switch (weaponType)
            {
                case WeaponType.DualWield:
                    holder.OnWeaponAttack -= SetLeftWeaponActive;
                    holder.OnWeaponAttack -= SetRightWeaponActive;

                    break;
                case WeaponType.TwoHanded:
                    holder.OnWeaponAttack -= SetTwoHandedWeaponActive;

                    break;
                case WeaponType.Ranged:
                    holder.OnWeaponAttack -= SetTwoHandedWeaponActive;
                    break;
                case WeaponType.OneHanded:
                {
                    holder.OnWeaponAttack -= SetRightWeaponActive;
                    break;
                }
            }
            holder.OnAttack -= ResetHitTarget;

        }

        void SetWeapons(WeaponColliderController lWeapon = null, WeaponColliderController rWeapon = null,
            WeaponColliderController twoHanded = null)
        {
            _weaponObjectLeft = lWeapon;
            _weaponObjectRight = rWeapon;
            _weaponObjectTwoHanded = twoHanded;
            
        }

        void SpawnWeapons()
        {
            if (leftWeapon != null)
            {
                _leftWeapon = Instantiate(leftWeapon, null);
            }

            if (rightWeapon != null)
            {
                _rightWeapon = Instantiate(rightWeapon, null);

            }

            if (twoHandedWeapon != null)
            {
                _twoHandedWeapon = Instantiate(twoHandedWeapon, null);
            }
        }
    }
}