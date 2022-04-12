using _ProjectAssets.Scripts.InteractionSystem;
using _ProjectAssets.Scripts.StarterAssets.InputSystem;
using _ProjectAssets.Scripts.StarterAssets.ThirdPersonController.Scripts;
using ProjectAssets.Scripts;
using ProjectAssets.Scripts.Player;
using UnityEngine;

namespace _ProjectAssets.Scripts.Player
{
    public class PlayerStateController : EntityStateController
    {
        [SerializeField] private StarterAssetsThirdPerson  playerThirdPerson;
        [SerializeField] private ThirdPersonController _playerController;
        [SerializeField] private PlayerInteract _playerInteract;
        [SerializeField] private PlayerStateStatus _playerStateStatus;


        public override void InitializeComponents()
        {
            base.InitializeComponents();
            _playerController = GetComponent<ThirdPersonController>();

        }
        public PlayerInteract GetPlayerInteract() => _playerInteract;
        public ThirdPersonController GetPlayerControllerScript() => _playerController;
        public StarterAssetsThirdPerson GetPlayerController() => playerThirdPerson;
        public PlayerStateStatus GetPlayerStateStatus() => _playerStateStatus;
        

    }
    
}