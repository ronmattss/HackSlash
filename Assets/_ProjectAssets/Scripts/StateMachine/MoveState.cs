using _ProjectAssets.Scripts.StateMachine.NPCAI;
using ProjectAssets.Scripts;
using UnityEngine;

namespace _ProjectAssets.Scripts.StateMachine
{
    public class MoveState : BaseState
    {
        public MoveState(Animator animator, EntityStateController entityStateController, StateOutput outState) : base(animator, entityStateController, outState)
        {
            // For now there is no need to do anything here
        }


        public override void Enter()
        {
            base.Enter();
            _entityState = EntityState.Moving;
        }

        public override void Update()
        {
            base.Update();
            // if (_entityStateController.GetType() == typeof(EntityAIStateController))
            // {
            //  //   ((EntityAIStateController)_entityStateController).GetNavMeshAgent().destination = ((EntityAIStateController)_entityStateController).GetEntitySensors().player.transform.position;
            //
            // }
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}