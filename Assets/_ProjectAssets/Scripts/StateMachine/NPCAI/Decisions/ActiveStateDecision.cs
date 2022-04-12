using UnityEngine;

namespace _ProjectAssets.Scripts.StateMachine.NPCAI.Decisions
{
    [CreateAssetMenu (menuName = "PluggableAI/Decisions/ActiveState")]
    public class ActiveStateDecision : Decision 
    {
        public override bool Decide (StateController controller)
        {
            bool chaseTargetIsActive = controller.chaseTarget.gameObject.activeSelf;
            return chaseTargetIsActive;
        }
    }
}