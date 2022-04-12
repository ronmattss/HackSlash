using System;
using UnityEngine;

namespace ProjectAssets.Scripts.Player
{
    public class PlayerStatus : MonoBehaviour,IDamageable
    {
        
        [SerializeField] PlayerStateStatus _playerStateStatus;
        [SerializeField] EntityStats _basicStats;
        [SerializeField] private int currentHealth;
        [SerializeField] private int maxHealth;
        [SerializeField] private int currentMana;
        [SerializeField] private int maxMana;


        private void Awake()
        {
            maxHealth = _basicStats.GetMaxHealth;
            maxMana = _basicStats.GetMaxMana;
            currentHealth = maxHealth;
            currentMana = maxMana;
            
        }


        public void OnHit(GameObject source, int damage)
        {
            _playerStateStatus.animator.SetTrigger("isHurt");
        }
    }
}