using _ProjectAssets.Scripts.StateMachine;
using UnityEngine;

namespace ProjectAssets.Scripts
{
    [CreateAssetMenu(fileName = "New StateOutput", menuName = "EntityState/OutStates/Idle")]
    public class OutputIdleState : StateOutput, IGoToIdleState
    {
        public override void NextState()
        {
            GoToIdleState();
        }

        public void GoToIdleState()
        {
            // condition
            if (animator.GetFloat("Speed") < 1 &&
                entityStateController.GetBaseState().GetCurrentState() == EntityState.Moving)
            {
                //          Debug.Log("Moving to Idle");
                nextState =
                    new IdleState(animator, entityStateController, this);
                status = StateStatus.Exit;
            }

            if (entityStateController.GetBaseState().GetCurrentState() == EntityState.Idle) return;
            if (entityStateController.GetExitState() &&
                entityStateController.GetBaseState().GetCurrentState() != EntityState.Moving)
            {
                //           Debug.Log("IDle to");
                nextState = new IdleState(animator, entityStateController,
                    this /*SetNextState(EntityState.Idle)*/);
                status = StateStatus.Exit;
            }
        }
    }
}