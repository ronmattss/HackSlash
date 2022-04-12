using _ProjectAssets.Scripts.Player;
using ProjectAssets.Scripts;
using ProjectAssets.Scripts.Player;
using UnityEngine;

namespace _ProjectAssets.Scripts.StateMachine
{
    [CreateAssetMenu(fileName = "New AttackStateOutput", menuName = "EntityState/OutStates/Attack")]
    public class OutputAttackState : StateOutput, IGoToAttackState
    {
        [Header("Attack Properties")] public float attackLock = .45f;

        public override void NextState()
        {
            GoToAttackState();
        }

        public void GoToAttackState()
        {
            if (entityStateController.currentAttackLock <= 0)
            {
                Debug.Log("You Can Attack");

          //      Debug.Log("is this null? "+ ((PlayerStateController)entityStateController).GetPlayerController() != null);
                if (((PlayerStateController)entityStateController).GetPlayerController().attack &&
                    ((PlayerStateController)entityStateController).GetPlayerControllerScript().Grounded)
                {
                    Debug.Log("ATTACK PRESSEDDDDDD");

                    nextState = new AttackState(animator, entityStateController,
                        this /*SetNextState(EntityState.Attacking)*/, .15f, attackLock); // change this
                    status = StateStatus.Exit;
                    return;
                }
            }

            return;
        }
    }
}