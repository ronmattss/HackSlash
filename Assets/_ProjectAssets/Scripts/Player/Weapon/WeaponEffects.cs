using System;
using UnityEngine;
namespace ProjectAssets.Scripts.Player
{
    
    [Serializable]
    public class WeaponEffects
    {
        public GameObject hitEffect;
        
        public AudioClip hitSound;
        public AudioClip swooshSound;
        
        
        
        public void PlayHitEffect(Vector3 hitPoint)
        {
            hitEffect.transform.position = hitPoint;
            hitEffect.gameObject.GetComponent<ParticleSystem>().Play();
        }
    }
}