using UnityEngine;

namespace _ProjectAssets.Scripts.StateMachine.NPCAI.Actions
{
    [CreateAssetMenu (menuName = "PluggableAI/Actions/Patrol")]
    public class PatrolAction : Action
    {
        public override void Act(StateController controller)
        {
            Patrol (controller);
        }

        private void Patrol(StateController controller)
        {

            if (!controller.animator.GetBool("isMoving"))
            {
                controller.animator.SetBool("isMoving", true);
            }
            controller.navMeshAgent.speed = controller.enemyStats.GetSpeed();

            controller.navMeshAgent.destination = controller.wayPointList [controller.nextWayPoint].position;
            controller.navMeshAgent.Resume ();

            if (controller.navMeshAgent.remainingDistance <= controller.navMeshAgent.stoppingDistance && !controller.navMeshAgent.pathPending) 
            {
                controller.nextWayPoint = (controller.nextWayPoint + 1) % controller.wayPointList.Count;
            }
        }
    }
}