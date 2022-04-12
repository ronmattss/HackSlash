using UnityEngine;

namespace _ProjectAssets.Scripts.StateMachine.NPCAI.Decisions
{
    [CreateAssetMenu (menuName = "PluggableAI/Decisions/Stop")]

    public class AttackCooldownDecision : Decision
    {
        public override bool Decide(StateController controller)
        {
            bool nearEnemy = Stop(controller);
            return nearEnemy;
        }
        
        private bool Stop(StateController controller)
        {
            var decide = controller.CheckIfCountDownElapsed(controller.enemyStats.GetAttackSpeed());
            if (decide)
            {
                controller.exitState = false;
            }

            return decide;


        }
    }
}