using _ProjectAssets.Scripts.Player;
using ProjectAssets.Scripts;
using ProjectAssets.Scripts.Player;
using UnityEngine;

namespace _ProjectAssets.Scripts.StateMachine
{
    public class InteractionState : BaseState
    {
        
        public InteractionState(Animator animator, EntityStateController entityStateController,StateOutput outputState) : base(animator, entityStateController,outputState)
        {
            _entityState = EntityState.Interacting;
        }

        // this is kinda tricky to implement, but it's a good idea to have a state that can be used to do something when the player is interacting with something
        public override void Enter()
        {
            base.Enter();
            // lock player movement

            animator.CrossFade(outState.animationName,
                .5f);
            _entityState = EntityState.Interacting;

        }
        
        public override void Exit()
        {
          
            ((PlayerStateController)_entityStateController).GetPlayerController().interact = false;
            Debug.Log("I am exiting ");

            _entityStateController.SetExitState(0);
            base.Exit();
        }
        
        public override void Update()
        {   // wait for input
            base.Update();
            animator.SetBool("IsInteracting",false);
            // if( _entityStateController.GetExitState())
            // {
            //   
            //     nextState = new IdleState(animator, _entityStateController, outState);
            //     _status = StateStatus.Exit;
            //     return;
            // }




        }





    }
}