using System;
using _ProjectAssets.Scripts.StarterAssets.InputSystem;
using _ProjectAssets.Scripts.StarterAssets.ThirdPersonController.Scripts;
using UnityEngine;

namespace ProjectAssets.Scripts.Player
{
    /// <summary>
    /// This Behavior is used to track player's current status
    /// </summary>
    public class PlayerStateStatus : MonoBehaviour
    {
        
        [SerializeField] ThirdPersonController _playerController;
        [SerializeField] StarterAssetsThirdPerson playerControllerThirdPerson;
         public Animator animator;

        
        
        public bool isHurt = false;
        public bool isDead = false;
        public bool isGrounded = false;
        public bool isFalling = false;
        public bool isInvincible = false;
        public bool isStunned = false;
        public bool isMoving = false;
        public bool isAttacking = false;
        public bool isBlocking = false;
        public bool isDashing = false;
        public bool isCasting = false;
        public bool isCharging = false;
        public bool isCharged = false;
        public bool isChargingAttack = false;
        public bool isChargedAttack = false;
        public bool isChargedAttackFinished = false;
        public bool isChargedAttackCancelled = false;
        public bool isChargedAttackStarted = false;
        public bool isChargedAttackEnded = false;


        void Start()
        {
            //get animator
            animator = GetComponent<Animator>();
        }

        private void LateUpdate()
        {
            CheckIfPlayerIsMoving();
            CheckIfPlayerIsGrounded();
            CheckIfPlayerIsAttacking();
        }

        void CheckIfPlayerIsAttacking() =>  isAttacking = playerControllerThirdPerson.attack;

        void CheckIfPlayerIsMoving()
        {
            if(playerControllerThirdPerson.move.x != 0 || playerControllerThirdPerson.move.y != 0)
            {
                isMoving = true;
            }
            else
            {
                isMoving = false;
            }
        }
        void CheckIfPlayerIsGrounded()
        {
            if(_playerController.Grounded)
            {
                isGrounded = true;
                isFalling = false;
            }
            else
            {
                isGrounded = false;
                isFalling = true;
            }
        }

    }
}