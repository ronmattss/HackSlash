using System;
using UnityEngine;

namespace ProjectAssets.Scripts.Player
{
    public class PlayerEffects : MonoBehaviour
    {
        public GameObject hitEffect;

        private void Awake()
        {
    //        currentWeapon = GetComponent<WeaponComponent>();
        //    currentWeapon.onHit += ShowEffect;
            
        }

        void ShowEffect(Vector3 position)
        {
           var effect =  Instantiate(hitEffect, position, Quaternion.identity);
            
           
           Destroy(effect, 1f);

        }
    }
}