using _ProjectAssets.Scripts.Player;
using ProjectAssets.Scripts;
using ProjectAssets.Scripts.Player;
using UnityEngine;

namespace _ProjectAssets.Scripts.StateMachine
{
    public class IdleState : BaseState
    {
        public IdleState(Animator animator, EntityStateController entityStateController, StateOutput outputState) :
            base(animator,
                entityStateController, outputState)
        {
            _entityState = EntityState.Idle;
        }

        public override void Enter()
        {
            base.Enter();
            //  Debug.Log($"Is controller null: {_entityStateController == null}");
            //  PlayAnimation(animationName);
            //          Debug.Log("AM I Entering IDle????");
            _entityStateController.ResetAttackCounter(); // reset the attack counter on idle state
            _entityStateController.CanAttack(1);
            _entityState = EntityState.Idle;
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void Update()
        {
            // wait for input
            base.Update();
            //      if (((PlayerStateController)_entityStateController).GetPlayerController().interact)
            //      {
            // //         Debug.Log("I AM PRESSING THE ATTACK BUTTON");
            //      }
            if (_entityStateController.GetType() != typeof(PlayerStateController)) return;
            if (((PlayerStateController)_entityStateController).GetPlayerController().interact &&
                ((PlayerStateController)_entityStateController).GetPlayerController().move == Vector2.zero)
            {
                //    Debug.Log("Interact is Pressed");
                // go to next State
                ((PlayerStateController)_entityStateController).GetPlayerInteract().Interact();
                ((PlayerStateController)_entityStateController).GetPlayerController().interact = false;
                // nextState = new InteractionState(animator, _entityStateController,outState);
                // _status = StateStatus.Exit;
                return;
            }

            // Runs the OutState


            // if (_entityStateController.GetPlayerController().attack &&
            //     _entityStateController.GetPlayerControllerScript().Grounded)
            // {
            //     //     Debug.Log("Interact is Pressed");
            //     // go to next State
            //     nextState = new AttackState(animator, _entityStateController, outState,.025f,.45f);
            //     _status = StateStatus.Exit;
            //     return;
            // }
        }
    }
}