using _ProjectAssets.Scripts.StateMachine;

namespace ProjectAssets.Scripts
{
    using System;
    using UnityEngine;


    [Serializable]
    public class BaseState
    {
        protected StateStatus _status = StateStatus.Enter;
        protected EntityState _entityState;
        protected EntityStateController _entityStateController;
        protected Animator animator; // since some logic will be inside the animator, we need to store it
        public BaseState nextState;
        public StateOutput outState;


        // when no input is received from the player, the state will be set to this state

        public BaseState(Animator animator, EntityStateController entityStateController, StateOutput outState)
        {
            this.animator = animator;
            this._entityStateController = entityStateController;
            this.outState = outState;
            this._entityState = EntityState.Idle;

            this.outState.Initialize(this._entityStateController); // init the main outstate
            foreach (var outputs in this.outState.stateOutputs) // then init all subOutputs
            {
                outputs.Initialize(this._entityStateController);

                // Debug.Log($"output: {outputs.state}");
            }

            if (_entityStateController.GetUniversalStateOutputs().Count > 0)
            {
                foreach (var outputs in _entityStateController.GetUniversalStateOutputs())
                {
                    outputs.Initialize(this._entityStateController);

                }
            }
        }

        public virtual void Enter()
        {
//            outState.Initialize(_entityStateController);
            outState.status = StateStatus.Update;
            outState.currentState = this;
            if (_entityStateController.GetUniversalStateOutputs().Count > 0)
            {
                foreach (var outputs in _entityStateController.GetUniversalStateOutputs())
                {
                    outputs.status = StateStatus.Update;
                    outputs.currentState = this;
                }
            }

            _status = StateStatus.Update;
        }

        public virtual void Update()
        {
            _status = StateStatus.Update;

            if (_entityStateController.currentAttackLock > 0)
            {
                _entityStateController.currentAttackLock -= Time.deltaTime;
            }


            if (_entityStateController.GetUniversalStateOutputs().Count > 0)
            {
                foreach (var outputs in _entityStateController.GetUniversalStateOutputs())
                {
                    outputs.NextState();
                    if (outputs.status == StateStatus.Exit)
                    {
                        outputs.status = StateStatus.Exit;
                        nextState = outputs.nextState;
                        _status = StateStatus.Exit;
                        break;
                    }
                }
            }


            foreach (var outputState in outState.stateOutputs)
            {
                outputState.NextState();
                if (outputState.status == StateStatus.Exit)
                {
                    outState.status = StateStatus.Exit;
                    nextState = outputState.nextState;
                    _status = StateStatus.Exit;

                    break;
                }
            }
        }

        public virtual void Exit()
        {
            if (_entityStateController.GetUniversalStateOutputs().Count > 0)
            {
                foreach (var outputs in _entityStateController.GetUniversalStateOutputs())
                {
                    outputs.status = StateStatus.Enter;
                }
            }

            outState.status = StateStatus.Enter;
            _status = StateStatus.Enter;
        }

        public BaseState Process()
        {
            if (_status == StateStatus.Enter) Enter();
            if (_status == StateStatus.Update) Update();
            if (_status == StateStatus.Exit)
            {
                Exit();
                return nextState; // Depends on the input, default on Idle State or if jumping, falling
            }

            return this;
        }


        public void SetStateController(EntityStateController controller)
        {
            _entityStateController = controller;
        }

        public void SetAnimator(Animator animator)
        {
            this.animator = animator;
        }

        public void SetStateStatus(StateStatus status)
        {
            _status = status;
        }

        public Animator GetAnimator() => animator;

        public StateStatus GetStateStatus() => _status;
        public EntityState GetCurrentState() => _entityState;
        public BaseState GetNextState() => nextState;
        public EntityStateController GetEntityStateController() => _entityStateController;
    }
}