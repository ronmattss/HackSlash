using _ProjectAssets.Scripts.Player;
using ProjectAssets.Scripts;
using ProjectAssets.Scripts.Player;
using UnityEngine;

namespace _ProjectAssets.Scripts.StateMachine
{
    [CreateAssetMenu(fileName = "New AttackStateOutput", menuName = "EntityState/OutStates/Hurt")]

    public class OutputHurtState : StateOutput,IGoToHurtState
    {
        public override void NextState()
        {
            GoToHurtState();
        }

        public void GoToHurtState()
        {
            if (entityStateController.GetBaseState().GetCurrentState() == EntityState.Hurt) return;
            if (((PlayerStateController)entityStateController).GetPlayerStateStatus().isHurt)
            {
                
                Debug.Log("Going to HurtState state");
                nextState = new HurtState(animator, entityStateController,this);
                status = StateStatus.Exit;
                return;
            }
        }
    }
}