using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _ProjectAssets.Scripts.ScriptableGameEvents
{
    /// <summary>
    /// This event is called on a MonoBehaviour, and is listened to the GameEventListeners.
    /// </summary>
    [CreateAssetMenu(fileName = "Game Event", menuName = "New Game Event", order = 0)]
    public class GameEvent : ScriptableObject
    {
        private HashSet<GameEventListener> _listeners = new HashSet<GameEventListener>();

        public void Invoke(int eventID)
        {
            foreach (var globaleEventListener in _listeners)
            {
                if (eventID == globaleEventListener.eventID)
                {
                    globaleEventListener.RaiseEvent();
                }
            }
            // conver to for loop


            
        }
            public void Register(GameEventListener gameEventListener) => _listeners.Add(gameEventListener);
            public void UnRegister(GameEventListener gameEventListener) => _listeners.Remove(gameEventListener);

    }
}