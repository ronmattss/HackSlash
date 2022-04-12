using UnityEngine;
using System;
using ProjectAssets.Scripts.Player;
using Unity.VisualScripting;

namespace DefaultNamespace
{
    // test script for Enemy
    public class Enemy : MonoBehaviour, IDamageable
    {
        
        // Get gameobjects renderer
    //    [SerializeField]  private MeshRenderer rend;
        [SerializeField]  private AudioSource audioSource;



        private void Awake()
        {
            // if (rend == null) return;
            //  rend.material.color = Color.red;
        }

        public void OnHit(GameObject source,int damage)
        {
            Debug.Log("AM I CALLED");
            PlayOnHitSound();
     //       ChangeColor();
        }

        // void ChangeColor()
        // {
        //     if (rend == null) return;
        //     // change color on hit
        //     if (rend.material.color == Color.red)
        //         rend.material.color = Color.yellow;
        //     else
        //         rend.material.color = Color.red;
        // }
        
        void PlayOnHitSound()
        {
            // play sound on hit
            audioSource.Play();
        }
    }
}