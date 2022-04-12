using UnityEngine;

namespace _ProjectAssets.Scripts.StateMachine.NPCAI.Decisions
{
    [CreateAssetMenu (menuName = "PluggableAI/Decisions/Scan")]
    public class ScanDecision : Decision 
    {
        public override bool Decide (StateController controller)
        {
            bool noEnemyInSight = Scan (controller);
            return noEnemyInSight;
        }

        private bool Scan(StateController controller)
        {
            controller.navMeshAgent.Stop ();
            controller.transform.Rotate (0, 80 * Time.deltaTime, 0);
            return controller.CheckIfCountDownElapsed (5);
            return false;
        }
    }
}