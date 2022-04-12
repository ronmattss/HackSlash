using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace _ProjectAssets.Scripts.ScriptableGameEvents
{
    public class GameEventListenerWithDelay : GameEventListener
    {
     [SerializeField] float _delay = 0.5f;
     [SerializeField] private UnityEvent _delayEvent;


     public override void RaiseEvent()
     {
        _unityEvent.Invoke();
        StartCoroutine(RunDelayEvent());
     }

     private IEnumerator RunDelayEvent()
     {
         yield return new WaitForSeconds(_delay);
         _delayEvent?.Invoke();
     }
     
     
    }
}