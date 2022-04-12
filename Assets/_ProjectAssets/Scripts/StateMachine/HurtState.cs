using _ProjectAssets.Scripts.Player;
using ProjectAssets.Scripts;
using ProjectAssets.Scripts.Player;
using UnityEngine;

namespace _ProjectAssets.Scripts.StateMachine
{
    // if You want events to trigger  when entering the state, add a Game event in this state then add a listener to whatever object you want it to trigger
    public class HurtState : BaseState
    {
        public HurtState(Animator animator, EntityStateController entityStateController, StateOutput outState) : base(
            animator, entityStateController, outState)
        {
        }

        public override void Enter()
        {

            animator.SetTrigger("isHurt");
            // animator.CrossFade(outState.animationName,
            //     .1f); // play the attack animation on 

            ((PlayerStateController)_entityStateController).GetPlayerStateStatus().isHurt = false;
            // play the attack animation on 
            //_entityStateController.canAttack = false; // lock the attack state
            _entityState = EntityState.Hurt; // set state to attacking
            base.Enter();
        }


        public override void Exit()
        {
            // animator.SetBool("isHurt", false);
            _entityStateController.SetExitState(0);
            base.Exit();
        }

        public override void Update()
        {
            base.Update();


        }
    }
}