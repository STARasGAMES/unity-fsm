using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

namespace SaG.FSM
{
    public class StateMachine : IStateMachine, IDisposable
    {
        public IState CurrentState => _currentState;

        private IState _currentState;
        private readonly ITransitionsMap _transitionsMap;

        public event Action<IState> StateChanged; 
        
        public StateMachine(IState state, ITransitionsMap transitionsMap)
        {
            _currentState = state ?? throw new ArgumentNullException(nameof(state));
            _transitionsMap = transitionsMap ?? throw new ArgumentNullException(nameof(transitionsMap));
            foreach (var transition in _transitionsMap.GetTransitionsFromAnyState())
            {
                transition.OnEnter();
            }
            _currentState.OnEnter();
        }

        public void ChangeState(IState state)
        {
            if (state == null)
                throw new ArgumentNullException(nameof(state));
            _currentState.OnExit();
            foreach (var transition in _transitionsMap.GetTransitionsFromState(_currentState))
            {
                transition.OnExit();
            }
            _currentState = state;
            foreach (var transition in _transitionsMap.GetTransitionsFromState(_currentState))
            {
                transition.OnEnter();
            }
            _currentState.OnEnter();
            OnStateChanged();
        }

        public void OnUpdate(float deltaTime)
        {
            IEnumerable<ITransition> anyStateTransitions = _transitionsMap.GetTransitionsFromAnyState();
            IEnumerable<ITransition> fromStateTransitions = _transitionsMap.GetTransitionsFromState(CurrentState);
            
            foreach (var transition in anyStateTransitions.Concat(fromStateTransitions))
            {
                if (transition.IsMuted)
                    continue;
                transition.OnUpdate(deltaTime);
                if (transition.Evaluate())
                {
                    var nextState = transition.TargetState;
                    Debug.Log($"Transiting to {nextState} because of {transition}", transition as Object);
                    ChangeState(nextState);
                    return;
                }
            }
            
            _currentState.OnUpdate(deltaTime);
        }

        private void OnStateChanged()
        {
            StateChanged?.Invoke(CurrentState);
        }

        public void Dispose()
        {
            _currentState.OnExit();
            foreach (var transition in _transitionsMap.GetTransitionsFromAnyState())
            {
                transition.OnExit();
            }
        }
    }
}