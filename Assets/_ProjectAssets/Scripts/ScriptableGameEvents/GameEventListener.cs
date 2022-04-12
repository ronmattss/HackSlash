using UnityEngine;
using UnityEngine.Events;

namespace _ProjectAssets.Scripts.ScriptableGameEvents
{
    public class GameEventListener : MonoBehaviour
    {
        [SerializeField] private GameEvent _gameEvent;
        [SerializeField] protected UnityEvent _unityEvent;
        [SerializeField] public int eventID = 0;
        void Awake() => _gameEvent.Register(this);
        void OnDestroy() => _gameEvent.UnRegister(this);
        
        public virtual void RaiseEvent() => _unityEvent.Invoke();
    }
}