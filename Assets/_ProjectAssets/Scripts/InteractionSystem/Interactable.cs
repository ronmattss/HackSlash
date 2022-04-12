using System;
using _ProjectAssets.Scripts.ScriptableGameEvents;
using UnityEngine;
using UnityEngine.Events;

namespace _ProjectAssets.Scripts.InteractionSystem
{
    public class Interactable : MonoBehaviour, IInteract
    {
        public UnityEvent OnInteract;
        public UnityEvent OnInteractionEnter;
        public UnityEvent OnInteractionExit;
        
        [SerializeField] private GameEvent onProximityEvent;
        [SerializeField] private GameEvent onInteractEvent;
        [SerializeField] private int interactableEventID;

        
        public void Interact(GameObject interactor)
        {
            OnInteract?.Invoke();
            onInteractEvent?.Invoke(interactableEventID);
        }

        private void OnTriggerEnter(Collider other)
        {
            OnInteractionEnter?.Invoke();
            onProximityEvent?.Invoke(interactableEventID);
        }


        private void OnTriggerExit(Collider other)
        {
            OnInteractionExit?.Invoke();
        }
    }
}












