using UnityEngine;

namespace _ProjectAssets.Scripts.StateMachine.NPCAI.Decisions
{
    [CreateAssetMenu (menuName = "PluggableAI/Decisions/EnemyNearDecision")]

    public class EnemyNearDecision :Decision
    {
        public override bool Decide(StateController controller)
        {
            return Vector3.Distance(controller.chaseTarget.transform.position, controller.transform.position) <=
                   controller.enemyStats.GetAttackRange();
        }
    }
}