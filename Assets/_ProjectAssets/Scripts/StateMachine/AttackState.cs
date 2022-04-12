using _ProjectAssets.Scripts.Player;
using ProjectAssets.Scripts;
using ProjectAssets.Scripts.Player;
using UnityEngine;

namespace _ProjectAssets.Scripts.StateMachine
{
    public class AttackState : BaseState
    {
        public float attackLock;
        float _transitionTime;

        public AttackState(Animator animator, EntityStateController entityStateController, StateOutput outputState,
            float transitionTime, float attackLock) : base(animator,
            entityStateController, outputState)
        {
            this._transitionTime = transitionTime;
            this.attackLock = attackLock;
        }

        public override void Enter()
        {
            _entityStateController.currentAttackLock = attackLock;
            ((PlayerStateController)_entityStateController).GetPlayerController().attack =
                false; // when entering an attack state, lock the attack input

            animator.CrossFade(outState.animationName,
                _transitionTime); // play the attack animation on 
            
            _entityStateController.IncrementAttackCounter();
            // play the attack animation on 
            //_entityStateController.canAttack = false; // lock the attack state
            _entityState = EntityState.Attacking; // set state to attacking
            base.Enter();
        }


        public override void Exit()
        {
            _entityStateController.SetExitState(0);
//           Debug.Log("outState.status: "+ outState.status);
            //   Debug.Log($"Exiting Attack State {GetType().Name}");
            base.Exit();
        }

        public override void Update()
        {
            base.Update();
            // lock the movement input
            // wait for input
            //  Debug.Log($"Current attack Lock: {currentAttackLock}");
            animator.SetBool("isAttacking", false);
            // if (_entityStateController.currentAttackLock <= 0) ???


            // foreach (var outputState in outState.stateOutputs)
            // {
            //     outputState.NextState();
            //
            //     if (outputState.status == StateStatus.Exit)
            //     {
            //         outState.status = StateStatus.Exit;
            //         nextState = outputState.nextState;
            //         _status = StateStatus.Exit;
            //
            //         break;
            //     }
            // }

            // if (_entityStateController.GetPlayerController().attack)
            // {
            //     nextState = new AttackState(animator,_entityStateController,outState,.15f); // change this
            //     _status = StateStatus.Exit;
            //     return;
            // }
            //
            // if (_entityStateController.GetExitState())
            // {
            //     nextState = new IdleState(animator,_entityStateController,outState); // change this
            //     _status = StateStatus.Exit;
            //     return;
            // }


//               Debug.Log("Im in attack state");
        }

        private void PlayAnimation(string nameParameter, bool status = true)
        {
            Debug.Log("Animator is null? " + animator != null);
            if (animator != null)
            {
                if (status)
                    animator.SetBool(nameParameter, true);
                else
                    animator.SetBool(nameParameter, false);
//                    Debug.Log("Is transitioning? "+animator.IsInTransition(1));
            }
        }
    }
}