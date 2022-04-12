using System;
using System.Collections.Generic;
using _ProjectAssets.Scripts.Helpers;
using ProjectAssets.Scripts;
using Unity.VisualScripting;
using UnityEngine;

namespace _ProjectAssets.Scripts.StateMachine
{
    /// <summary>
    /// This Class will be inside a Scriptable Object that will be used to store the state machine.
    /// 
    /// </summary>
///TODO: Create a class that copies the data of the StateOutputs and StateInputs to the StateMachine.
    [Serializable]

    public abstract class StateOutput : ScriptableObject
    {
     [NonSerialized] public EntityStateController entityStateController;
     [NonSerialized] public BaseState nextState;
     [NonSerialized] public StateStatus status;


     public EntityState state;  
     public  Animator animator;
     public string animationName;
     public  BaseState currentState;
     public List<StateOutput> stateOutputs = new List<StateOutput>();
     

     //initialize this State
     public virtual void Initialize(EntityStateController stateController)
     {
         entityStateController = stateController;
         animator = entityStateController.GetAnimator();
         currentState = entityStateController.GetBaseState();
         foreach (var states in stateOutputs)
         {
             states.status = StateStatus.Enter;
         }

         
     }

        public abstract void NextState();
      protected  StateOutput SetNextState(EntityState nextState)
        {
            foreach (var thisState in stateOutputs)
            {
                if (nextState == thisState.state)
                {
                    return thisState;
                }
            }

            return entityStateController.GetDefaultState();
        }
    }
}