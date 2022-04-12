using UnityEngine;

namespace _ProjectAssets.Scripts.StateMachine.NPCAI
{
    
    public abstract class Action : ScriptableObject 
    {
        public abstract void Act (StateController controller);
    }
}