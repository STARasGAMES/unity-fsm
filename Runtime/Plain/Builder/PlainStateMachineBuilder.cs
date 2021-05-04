using System;
using System.Collections.Generic;

namespace SaG.FSM.Plain.Builder
{
    public class PlainStateMachineBuilder
    {
        private readonly ITransitionsMap _transitionsMap = new TransitionsMap();
        
        private IState _currentState;
        private IState _defaultState;
        private bool _isBuildingTransitionsFromAnyState;

        private PlainStateMachineBuilder()
        {
            
        }
        
        public PlainStateMachineBuilder Default(IState state)
        {
            if (state == null)
                throw new ArgumentNullException(nameof(state));
            _defaultState = state;
            return this;
        }
        
        public PlainStateMachineBuilder From(IState state)
        {
            if (state == null)
                throw new ArgumentNullException(nameof(state));
            _currentState = state;
            _isBuildingTransitionsFromAnyState = false;
            return this;
        }
        
        public PlainStateMachineBuilder FromAny()
        {
            _isBuildingTransitionsFromAnyState = true;
            _currentState = null;
            return this;
        }
        
        public PlainStateMachineBuilder To(IState state, Func<bool> condition)
        {
            if (state == null)
                throw new ArgumentNullException(nameof(state));
            if (condition == null)
                throw new ArgumentNullException(nameof(condition));
            
            var transition = new PlainTransition(state, condition);
            if (_isBuildingTransitionsFromAnyState)
            {
                _transitionsMap.AddFromAnyState(transition);
                return this;
            }
            
            if (_currentState == null)
                throw new Exception("Invalid builder state. Call From<T>() or FromAny() before using To<T>()");
            
            _transitionsMap.AddFromState(_currentState, transition);
            return this;
        }
        
        public IStateMachine Build()
        {
            return new StateMachine(_defaultState, _transitionsMap);
        }

        public static PlainStateMachineBuilder Create(IState defaultState)
        {
            var builder = new PlainStateMachineBuilder();
            builder.Default(defaultState);
            return builder;
        }
    }
}