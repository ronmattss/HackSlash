using System;
using System.Collections.Generic;
using _ProjectAssets.Scripts.StateMachine.NPCAI;
using UnityEngine;
using UnityEngine.Events;

namespace ProjectAssets.Scripts.Player
{
    public class EntityStatus : MonoBehaviour,IDamageable
    {
        // Enity Status checks the current status of a state
        [SerializeField] private Animator _animator;
        [SerializeField]  private AudioSource audioSource;
        [SerializeField] private StateController _stateController;
        [SerializeField] EntityStats stats;
        [SerializeField] private int currentHealth;
        [SerializeField] private int currentMana;
        [SerializeField] private int attackDamage;
        [SerializeField] private int speed;
        [SerializeField] private float _attackSpeed;
        [SerializeField] private float _attackRange;
        [SerializeField] private float detectionRange;
        [SerializeField] private List<String> listOfPossibleAttacks = new List<String>();
        private int maxHealth;
        private int maxMana;
        public UnityEvent hitEvent;
        public UnityEvent deathEvent;
        
        
        
        public int GetCurrentHealth() => currentHealth;
        public int GetCurrentMana() => currentMana;
        public int GetAttackDamage() => attackDamage;
        public int GetSpeed() => speed;
        public int GetMaxHealth() => maxHealth;
        public int GetMaxMana() => maxMana;
        public float GetAttackSpeed() => _attackSpeed;
        public float GetAttackRange() => _attackRange;
        public float GetDetectionRange() => detectionRange;
        public List<String> GetListOfPossibleAttacks() => listOfPossibleAttacks;
        

        // on awake set status from stats
        private void Start()
        {

            _animator = GetComponent<Animator>();
            audioSource = GetComponent<AudioSource>();
            _stateController = GetComponent<StateController>();
            maxHealth = stats.GetMaxHealth;
            maxMana = stats.GetMaxMana;
            attackDamage = stats.GetBaseDamage;
            currentHealth = maxHealth;
            currentMana = maxMana;
            speed = stats.GetMoveSpeed;
            _attackSpeed = stats.GetAttackSpeed;
            _attackRange = stats.GetAttackRange;
            detectionRange = stats.GetDetectionRange;
            hitEvent.AddListener(PlayOnHitSound);
            
        }

        public void OnHit(GameObject source,int damage)
        {
            hitEvent?.Invoke();
            _stateController.chaseTarget = source.transform;
            currentHealth -= damage;
            
            if (currentHealth <= 0)
            {
                // events that happen when the entity dies
                currentHealth = 0;
                OnDeath();
            }
            else
            {
                _animator.SetTrigger("isHurt");
            }
        }
        
        public void OnDeath()
        {
           // deactivate this
           _animator.SetTrigger("isDead");
           InvokeOnDeathEvents();
           Destroy(this.gameObject,5f);
        }

        public void InvokeOnDeathEvents()
        {
            deathEvent?.Invoke();
        }
        
        public void OnHeal(int heal)
        {
            // events that happen when the entity is healed
            // heal the entity
            currentHealth += heal; // add heal to current health but don't exceed with maximum health
            if (currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
            }
        }
        
        public void OnMana(int mana)
        {
            // events that happen when the entity gains mana
        }
        
        public void OnStamina(int stamina)
        {
            // events that happen when the entity gains stamina
        }
        
        void PlayOnHitSound()
        {
            // play sound on hit
            audioSource.Play();
        }
        

        
    }
}