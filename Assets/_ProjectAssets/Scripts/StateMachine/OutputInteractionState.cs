using _ProjectAssets.Scripts.Player;
using ProjectAssets.Scripts;
using ProjectAssets.Scripts.Player;
using UnityEngine;

namespace _ProjectAssets.Scripts.StateMachine
{
    [CreateAssetMenu(fileName = "New MoveStateOutput", menuName = "EntityState/OutStates/Interaction")]

    public class OutputInteractionState : StateOutput, IGotoInteractionState
    {
        public override void NextState()
        {
            GoToInteractionState();
        }

        public void GoToInteractionState()
        {
            if (entityStateController.GetBaseState().GetCurrentState() == EntityState.Interacting) return;
            if (((PlayerStateController)entityStateController).GetPlayerController().interact &&
                ((PlayerStateController)entityStateController).GetPlayerController().move == Vector2.zero)
            {
                
                Debug.Log("Going to Interaction state");
                nextState = new InteractionState(animator, entityStateController,this);
                status = StateStatus.Exit;
                return;
            }
        }
    }
}