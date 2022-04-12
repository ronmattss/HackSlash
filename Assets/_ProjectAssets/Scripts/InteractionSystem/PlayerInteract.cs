using System;
using UnityEngine;

namespace _ProjectAssets.Scripts.InteractionSystem
{
    public class PlayerInteract : MonoBehaviour
    {
        public IInteract interactable;

        [SerializeField] private float distanceToInteract = 2f;
        private void Update()
        {
            if (interactable != null)
                // log found
                Debug.Log("Found interactable");
            else
            {
                // log not found
                Debug.Log("No interactable found");
            }
        }
        
       public void Interact()
        {
            if (interactable != null)
            {
                interactable.Interact(this.gameObject);
                interactable = null;
            }
        }
    }
}