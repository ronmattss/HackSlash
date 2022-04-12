using ProjectAssets.Scripts;
using UnityEngine;

namespace _ProjectAssets.Scripts.StateMachine
{
    [CreateAssetMenu(fileName = "New MoveStateOutput", menuName = "EntityState/OutStates/Move")]

    public class OutputMoveState :StateOutput, IGoToMoveState
    {
        // override walk speed?
 public override void NextState()
        {
        //    Debug.Log($"is moveOut Animator null? {animator == null}");
           GoToMoveState();
        }

        public void GoToMoveState()
        {
            if (entityStateController.GetBaseState().GetCurrentState() == EntityState.Moving) return;
            if (animator.GetFloat("Speed") > 0 && entityStateController.GetBaseState().GetCurrentState() == EntityState.Idle)
            {
                nextState = new MoveState(animator,entityStateController,this);
                status = StateStatus.Exit;
            }
 
        }
    }
}