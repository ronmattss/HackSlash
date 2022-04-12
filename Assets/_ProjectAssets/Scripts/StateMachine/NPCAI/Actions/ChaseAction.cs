using UnityEngine;

namespace _ProjectAssets.Scripts.StateMachine.NPCAI.Actions
{
    [CreateAssetMenu (menuName = "PluggableAI/Actions/Chase")]
    public class ChaseAction : Action 
    {
        public override void Act (StateController controller)
        {
            Chase (controller);    
        }

        private void Chase(StateController controller)
        {
            if (!controller.animator.GetBool("isMoving"))
            {
                controller.animator.SetBool("isMoving", true);
            }

            controller.navMeshAgent.speed = controller.enemyStats.GetSpeed() * 1.5f;

            controller.navMeshAgent.destination = controller.chaseTarget.position;
            controller.navMeshAgent.isStopped = false; // replace this line
            
        }
    }
}