using System;
using System.Collections;
using System.Collections.Generic;
using ProjectAssets.Scripts.Player;
using UnityEngine;
using UnityEngine.AI;


namespace _ProjectAssets.Scripts.StateMachine.NPCAI
{
    public class StateController : MonoBehaviour
    {
        public State currentState;
         public EntityStatus enemyStats;
        public Transform eyes;
        public State remainState;


        [HideInInspector] public NavMeshAgent navMeshAgent;
        // [HideInInspector] public Complete.TankShooting tankShooting;
                          public List<Transform> wayPointList;
        [HideInInspector] public int nextWayPoint;
        [HideInInspector] public Transform chaseTarget;
        [HideInInspector] public float stateTimeElapsed;
        public Animator animator;
        public bool exitState = false; // used in the animator to set the exit state
        


        // TODO: Make Animation Work
        private void Start () 
        {
          //  tankShooting = GetComponent<Complete.TankShooting> ();
            navMeshAgent = GetComponent<NavMeshAgent>();
            enemyStats = GetComponent<EntityStatus>();
            GetComponent<NavMeshAgent>().speed = enemyStats.GetSpeed();

            
            //Set navMeshAgent Speed
            
            
            
            SetupAI();
        }

        public void SetupAI()
        {
            navMeshAgent.enabled = true;
        }

        private void Update()
        {

            currentState.UpdateState (this);
        }

        private void OnDrawGizmos()
        {
            if (currentState != null && eyes != null) 
            Gizmos.color = currentState.sceneGizmoColor;
            //      Gizmos.DrawWireSphere (eyes.position, enemyStats.lookSphereCastRadius);
        }

        public void TransitionToState(State nextState)
        {

            if (nextState != remainState) 
            {
                currentState = nextState;
                exitState = false;
                OnExitState ();
            }

        }

        public bool CheckIfCountDownElapsed(float duration)
        {
            stateTimeElapsed += Time.deltaTime;
            Debug.Log("State Time Elapsed: " + stateTimeElapsed);
            return stateTimeElapsed >= duration;
        }
        public bool AttackReset(float duration)
        {
            stateTimeElapsed += Time.deltaTime;
            return stateTimeElapsed >= duration;
        }
        
        public void SetExitState(int state)
        {
            exitState = Convert.ToBoolean(state);
        }

        private void OnExitState()
        {

                stateTimeElapsed = 0;
        }

        // since you are exiting the state, you are also entering a state 
        private void OnEnterState()
        {
            // play animation
            
        }
        public void RotateTowards()
        {
           
        
            Vector3 direction = (chaseTarget.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));    // flattens the vector3
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 2);
        }
    }
}