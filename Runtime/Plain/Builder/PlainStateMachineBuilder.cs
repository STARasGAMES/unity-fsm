using System;
using System.Collections.Generic;

namespace SaG.FSM.Plain.Builder
{
    public class PlainStateMachineBuilder
    {
        private readonly ITransitionsMap _transitionsMap = new TransitionsMap();
        
        private IState _currentState;
        private IState _defaultState;
        
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
            return this;
        }
        
        public PlainStateMachineBuilder To(IState state, Func<bool> condition)
        {
            if (_currentState == null)
                throw new Exception("Invalid builder state. Call From<T>() before using To<T>()");
            if (state == null)
                throw new ArgumentNullException(nameof(state));
            if (condition == null)
                throw new ArgumentNullException(nameof(condition));
            var transition = new PlainTransition(state, condition);
            _transitionsMap.AddFromState(_currentState, transition);
            return this;
        }
        
        
        public IStateMachine Build()
        {
            return new StateMachine(_defaultState, _transitionsMap);
        }
    }
}