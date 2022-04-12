using UnityEngine;

namespace _ProjectAssets.Scripts.StateMachine.NPCAI.Decisions
{
    [CreateAssetMenu (menuName = "PluggableAI/Decisions/Look")]
    public class LookDecision : Decision {

        public override bool Decide(StateController controller)
        {
            if (controller.chaseTarget != null)
            {
                return true;
            }
            bool targetVisible = Look(controller);
            return targetVisible;
        }

        
        // change this
        private bool Look(StateController controller)
        {
            RaycastHit hit;
            
            Debug.DrawRay (controller.eyes.position, controller.eyes.forward.normalized * controller.enemyStats.GetDetectionRange(), Color.green);
            
            if (Physics.SphereCast (controller.eyes.position, controller.enemyStats.GetDetectionRange(), controller.eyes.forward, out hit, controller.enemyStats.GetDetectionRange())
                && hit.collider.CompareTag ("Player")) {
                controller.chaseTarget = hit.transform;
                return true;
            } else 
            {
               return false;
            }
        }
    }
}