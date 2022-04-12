using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace ProjectAssets.Scripts.Player
{
    public class WeaponColliderController : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _meleeSensors = new List<GameObject>();
        [SerializeField] private List<ObjectPosition> _meleeSensorsPositions = new List<ObjectPosition>();
        public event Action<Collider, Vector3> OnMeleeHit;
        [SerializeField] private List<string> ignoreNames = new List<string>();
        public LayerMask hittableMask;
        [SerializeField] private ParticleSystem swordTrail;
        [SerializeField] private List<RaycastHit> _hits = new List<RaycastHit>();
        List<RaycastHit> _uniqueHits = new List<RaycastHit>();



        private Collider _currentTarget;
        // private float _weaponTimeout = 0.25f;
        // private float _currentWeaponTimeout = 0.25f;

        public bool isWeaponActive = false;

        bool MatchNames(string objectName)
        {
            foreach (var names in ignoreNames)
            {
                if (objectName == names)
                    return true;
            }

            return false;
        }

        private void Start()
        {
            //get all children gameobject

            foreach (Transform child in transform)
            {
                if (MatchNames(child.name)) continue;
                _meleeSensors.Add(child.gameObject);
                _meleeSensorsPositions.Add(new ObjectPosition(child.gameObject));
            }

            foreach (var meleeSensorsPosition in _meleeSensorsPositions)
            {
                var position = meleeSensorsPosition.gameObject.transform.position;
                meleeSensorsPosition.currentPosition = position;
                meleeSensorsPosition.lastPosition = position;
            }
        }

        private void Update()
        {
            // when weapon is active record the position of the sensors
            if (isWeaponActive)
            {
                swordTrail.Play();

                foreach (var meleeSensorsPosition in _meleeSensorsPositions)
                {
                    var position = meleeSensorsPosition.gameObject.transform.position;
                    meleeSensorsPosition.currentPosition = position;
                }

                foreach (var sensorsPosition in _meleeSensorsPositions)
                {

                    _hits.Add(HitCheck(sensorsPosition.lastPosition, sensorsPosition.currentPosition));
                }

                foreach (var meleeSensorsPosition in _meleeSensorsPositions)
                {
                    var position = meleeSensorsPosition.gameObject.transform.position;
                    meleeSensorsPosition.lastPosition = position;
                }


                CheckTarget();
            }
            else
            {
                _currentTarget = null;
                _hits.Clear();
                _uniqueHits.Clear();
            }

        }




        private void CheckTarget()
        {

            var currTarget = _currentTarget;
            // check every hit in the hits list
            foreach (var hit in _hits)
            {
                // continue until something is hit
                if (hit.collider == null && currTarget == null)
                {
                    currTarget = null;
                    continue;
                }

                if(hit.collider != null) // if you hit something in this frame
                {   
                   // if it hit something check if it is on the IsOnUniqueHits
                   if (!IsOnUniqueHits(hit)) // if not then add it to the unique hits
                   {
                       _uniqueHits.Add(hit); // Add it to the unique hits
                       OnMeleeHit?.Invoke(hit.collider, hit.point); // invoke the event
                       currTarget = hit.collider; // set the current target to the hit collider
                       _currentTarget = currTarget;
                   }

                }
            }
            if(currTarget == null)
            {
                OnMeleeHit?.Invoke(null, Vector3.zero);
                _currentTarget = null;
            }
          //  OnMeleeHit?.Invoke(null, Vector3.zero);
        }

        bool IsOnUniqueHits(RaycastHit currentHit)
        {
            //check if current hit is unique
            if (_uniqueHits.Count < 1) return false;
            foreach (var hit in _uniqueHits)
            {
                if (hit.collider == currentHit.collider)
                {
                    return true;
                }
            }

            return false;
        }
        private RaycastHit HitCheck(Vector3 lastPosition, Vector3 currentPosition)
        {
            Physics.Linecast(lastPosition, currentPosition, out RaycastHit hit, hittableMask);

            return hit;
        }
        
        
        

        public Collider ResetTarget()
        {
            return _currentTarget;
        }

        private void OnDrawGizmos()
        {
            if (isWeaponActive)
            {
                for (int i = 0; i < _meleeSensorsPositions.Count; i++)
                {
                    Gizmos.DrawLine(_meleeSensorsPositions[i].lastPosition, _meleeSensorsPositions[i].currentPosition);
                }
            }
        }
    }
}