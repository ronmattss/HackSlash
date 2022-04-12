using System;
using _ProjectAssets.Scripts.InteractionSystem;
using UnityEngine;

namespace ProjectAssets.Scripts.Player
{
    public class CameraAim : MonoBehaviour
    {
        // add an aim crosshair
        // Raycast to interactable objects SHOULD detect the distance of the player to the object
        
        public GameObject StartPosition;
        public GameObject hitPoint;
       [SerializeField] private PlayerInteract _interact;
        public LayerMask whatToHit;
        void Update()
        {
            // set startposition rotation to the camera's current rotation
            RaycastHit hit;
            if (Physics.Raycast(StartPosition.transform.position, StartPosition.transform.forward, out hit, Mathf.Infinity,whatToHit))
            {

                if (hit.collider.CompareTag("Interactable"))
                {
                    if (hit.collider.TryGetComponent(out IInteract interact))
                    {
                        if (_interact.interactable != interact)
                        {
                            _interact.interactable = interact;
                        }
                        // make the player look at the object at Y axis
                        
                    }

                }
                else
                {
                    _interact.interactable = null;
                    
                }

                // If the raycast hits a collider, set the position of the camera to the hit point
                if (hit.collider != null || hit.collider.tag == "Player")
                {

                    // log the name of the collider
                    // get the center of the hitpoint transform
                    // set the position of the camera to the hitpoint
                    
                 //  hitPoint.transform.position  = hit.transform.GetComponent<Renderer>().bounds.center;
                    hitPoint.transform.position = hit.point;

                    // Rotate Player's Y axis to look at the hit point
                    
                    
                    
                    
                }
            }

        }

        private void OnDrawGizmos()
        {
            // change Gizmos color to red
            Gizmos.color = Color.red;
            Gizmos.DrawLine(StartPosition.transform.position, StartPosition.transform.forward * 100);
        }
    }
}