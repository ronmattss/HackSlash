using System;
using System.Collections.Generic;
using _ProjectAssets.Scripts.Helpers;
using _ProjectAssets.Scripts.InteractionSystem;
using _ProjectAssets.Scripts.StateMachine;
using ProjectAssets.Scripts.Player;
using UnityEngine;

namespace ProjectAssets.Scripts
{
    
    /// <summary>
    /// Class that handles the State of an entity using the State Pattern
    /// </summary>
    public class EntityStateController : MonoBehaviour
    {
        //Using State Machine to control Player's Animation and States
        // Have a State class where it stores, the current State, Next State (possible States available), and the Animation to play
        
        // Handles the State of the Player
        // Reads the Input and changes the State of the Player
        
        // For Player States

        
        [SerializeField] protected Animator _animator;
        [SerializeField] protected StateOutput idleStateOutput;
        protected  StateOutput _currentStateOutputClone;

        [SerializeField] protected EntityState _currentState;

        [SerializeField] protected string[] _attackNames;
        [SerializeField] protected int _attackCounter;
        [SerializeField] protected BaseState _baseState;
        [SerializeField] protected List<StateOutput> _universalStateOutputs = new List<StateOutput>();
        
        public float currentAttackLock;

       

        // TODO: Create booleans for exiting states, useful for animation based states

        
        
        
        
        public Animator GetAnimator() => _animator;
        
        [SerializeField]private bool exitCurrentState = false;
        [SerializeField]private StateStatus currentStatus;
        
        public bool canAttack = true;
        
        private  void Awake()
        {
            _currentStateOutputClone = idleStateOutput.Clone();
            InitializeComponents();
            // _baseState.SetStateController(this);
            // _baseState.SetAnimator(gameObject.GetComponent<Animator>());
            // _baseState.SetStateStatus(StateStatus.Enter);
        }


        public virtual void InitializeComponents()
        {
            
            _animator = GetComponent<Animator>();
            _baseState = new IdleState(_animator,this,_currentStateOutputClone);

        }
        
       

        void Update()
        {
           // currentStatus = _baseState.GetStateStatus();
            
            _baseState = _baseState.Process();
            _currentState = _baseState.GetCurrentState();
            
        }

        // TODO: Change state to a new state

        public void SetExitState(int exit)
        {
            exitCurrentState = Convert.ToBoolean(exit);
        }
        // prevents the player from being stuck in a state when the idle state is playing
        public void ForceExitState()
        {
            if (exitCurrentState)
            {
                exitCurrentState = false;
            }
        }
        public bool GetExitState()
        {
            return exitCurrentState;
        }

        public void CanAttack(int attackPossibility)
        {
            canAttack = Convert.ToBoolean(attackPossibility);
        }

        public void SetBoolAnimatorParameter(string currentAnimation)
        {
            _baseState.GetAnimator().SetBool(currentAnimation,!_baseState.GetAnimator().GetBool(currentAnimation));
        }
        
        public BaseState GetBaseState()
        {
            return _baseState;
        }

        public String ReturnNextAttack()
        {
            Debug.Log(_attackCounter);

            return _attackNames[_attackCounter];
        }
      public  void IncrementAttackCounter()
        {
            if(_attackCounter <= _attackNames.Length )
            {
                _attackCounter++;
            }
            else
            {
                _attackCounter  = 0;
            }
        }

        
        public void ResetAttackCounter()
        {
            _attackCounter = 0;
        }
        
        // force weapon to deactivate
        public void ForceWeaponDeactivate()
        {
            
            if (this.gameObject.TryGetComponent(out WeaponHolder holder))
            {
                
                for(int i = 0; i <2; i++)
                {
                    holder.ResetWeapon(i);
                }
            }
        }
        
        public StateOutput GetDefaultState() => idleStateOutput;
        public List<StateOutput> GetUniversalStateOutputs() => _universalStateOutputs;








    }
}