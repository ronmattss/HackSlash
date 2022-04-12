using System;
using UnityEngine;

namespace ProjectAssets.Scripts.Player
{
    public class NpcSensors : MonoBehaviour
    {
        // handles NPC sensors
        public float detectionDistance = 10;
        public float attackDistance = 2;
        //
        public GameObject player;

        private void Update()
        {
            DetectPlayer();
            Debug.Log("Player detected? " + player);
        }
        
        //detects if player is within detection distance using SphereCastAll
        private void DetectPlayer()
        {
            var hits = Physics.SphereCastAll(transform.position, detectionDistance, Vector3.up);
            foreach (var hit in hits)
            {
                if (hit.collider.gameObject.CompareTag("Player"))
                {
                    player = hit.collider.gameObject;
                    return;
                }
                else
                {
                    player = null;
                }
            }
        }



        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, detectionDistance);
        }
    }
}