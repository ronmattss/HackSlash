using System;
using System.Collections.Generic;
using UnityEngine;

namespace _ProjectAssets.Scripts.StateMachine
{
    /// <summary>
    /// This Scriptable Object is used to store state data of the player, where animation and animation parameter is the data
    /// and other State Parameters. We also need to create a class that accepts inputs that will be used in the state machine.
    /// and using enums for the states and inputs, have a state machine that will be used to determine what state the player is in.
    /// and output the corresponding state based on the input. 
    /// </summary>
    [Serializable] 
    [CreateAssetMenu(fileName = "New State", menuName = "EntityState/EntityState")]
    public class EntityScriptableStateData : ScriptableObject
    {
        
      //public List<OutputState> m_StateOutputs = new List<OutputState>();
        

        
    }

    public interface IGoToAttackState
    {
        void GoToAttackState();
    }
}