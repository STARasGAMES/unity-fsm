using System;
using System.Collections.Generic;
using UnityEngine;

namespace SaG.FSM.Mono
{
    public class MonoStateMachine : MonoBehaviour, IStateMachine
    {
        [SerializeField] private MonoState _defaultState = default;
        protected IStateMachine _stateMachine;

        public IStateMachine StateMachine => _stateMachine ?? (_stateMachine = new StateMachine(_defaultState, GetTransitionTable()));

        protected virtual void Awake()
        {
        }

        public IState CurrentState => _stateMachine.CurrentState;
        
        public event Action<IState> StateChanged
        {
            add => StateMachine.StateChanged += value;
            remove => StateMachine.StateChanged -= value;
        }

        public void ChangeState(IState state)
        {
            StateMachine.ChangeState(state);
        }

        public void OnUpdate(float deltaTime)
        {
            StateMachine.OnUpdate(deltaTime);
        }

        private void Update()
        {
            OnUpdate(Time.deltaTime);
        }

        private ITransitionsMap GetTransitionTable()
        {
            List<MonoState> states = new List<MonoState>();
            GetComponentsInChildren(states);
            
            var transitionsMap = new TransitionsMap();
            foreach (var state in states)
            {
                foreach (var transition in state.GetComponentsInChildren<ITransition>())
                {
                    transitionsMap.AddFromState(state, transition);
                }
            }

            return transitionsMap;
        }
    }
}