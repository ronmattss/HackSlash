using UnityEngine;

namespace _ProjectAssets.Scripts.StateMachine.NPCAI
{
    public abstract class Decision : ScriptableObject
    {
        public abstract bool Decide (StateController controller);
    }
}