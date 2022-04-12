using System;
using UnityEngine;

namespace _ProjectAssets.Scripts.InteractionSystem
{
    // The base of interactions. IInteract will serve as a base function such that any interactions deriving this interface will invoke from this function

    
    public interface IInteract
    {
        void Interact(GameObject interactor);
    }
}