using UnityEngine;
using UnityEngine.AI;

namespace _ProjectAssets.Scripts.StateMachine.NPCAI
{
    public class NavMeshTest : MonoBehaviour
    {
        public Transform goal;
       
        void Start () {
            NavMeshAgent agent = GetComponent<NavMeshAgent>();
            agent.destination = goal.position; 
        }
    }
}