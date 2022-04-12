using UnityEngine;

namespace ProjectAssets.Scripts.Player
{
    [CreateAssetMenu(fileName = "New Entity Stats", menuName = "Entity/Base Stats", order = 0)]
    public class EntityStats : ScriptableObject
    {
        [SerializeField] private int _maxHealth;
        [SerializeField] private int _maxMana;
        [SerializeField] private int _baseDamage;
        [SerializeField] private int _moveSpeed;
        [SerializeField] private float _attackSpeed;
        [SerializeField] private float _attackRange;
        [SerializeField] private float detectionRange;
        
        
        
        // Getters
        public int GetMaxHealth => _maxHealth;
        public int GetMaxMana => _maxMana;
        public int GetBaseDamage => _baseDamage;
        public float GetAttackSpeed => _attackSpeed;
        public int GetMoveSpeed => _moveSpeed;
        public float GetAttackRange => _attackRange;
        public float GetDetectionRange => detectionRange;
        
    }
}