namespace _ProjectAssets.Scripts.StateMachine.NPCAI
{
    [System.Serializable]
    public class Transition 
    {
        public Decision decision;
        public State trueState;
        public State falseState;
    }
}