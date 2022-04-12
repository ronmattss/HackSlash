using System;
using System.Collections.Generic;
using UnityEngine;

namespace _ProjectAssets.Scripts.StateMachine.NPCAI.Actions
{
    [CreateAssetMenu(menuName = "PluggableAI/Actions/Attack")]
    public class AttackAction : Action
    {
        public override void Act(StateController controller)
        {
            Attack(controller);
        }
        
        // this should play an attack animation, then change state after playing it

        private void Attack(StateController controller)
        {
            controller.animator.SetBool("isMoving", false);
            controller.navMeshAgent.isStopped = true;
            if (controller.exitState) return;
             controller.animator.CrossFade(RandomAttackAnimation(controller.enemyStats.GetListOfPossibleAttacks()),
                 .15f);
           // controller.animator.SetTrigger("isAttacking");
        }

        private String RandomAttackAnimation(List<String> animations)
        {
            return animations[UnityEngine.Random.Range(0, animations.Count)];
        }


    }
}