using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SaG.FSM.Scriptable
{
    public class ScriptableTransitionTable : ScriptableObject
    {
        [SerializeField] private List<SerializableTransition> _transitions;
        
        [System.Serializable]
        private class SerializableTransition
        {
            public ScriptableState fromState;
            public ScriptableState toState;
            public List<ScriptableCondition> conditions;

            public Transition ToTransition() => new Transition(toState, conditions);
        }

        private class Transition : ITransition
        {
            private readonly IState _targetState;
            private readonly IEnumerable<ICondition> _conditions;

            public Transition(IState targetState, IEnumerable<ICondition> conditions)
            {
                _targetState = targetState;
                _conditions = conditions;
            }

            public IState TargetState => _targetState;
            
            public bool Evaluate()
            {
                return _conditions.All(c => c.Value);
            }

            public void OnEnter()
            {
                foreach (var condition in _conditions)
                    condition.OnEnter();
            }

            public void OnUpdate(float deltaTime)
            {
                foreach (var condition in _conditions)
                    condition.OnUpdate(deltaTime);
            }

            public void OnExit()
            {
                foreach (var condition in _conditions)
                    condition.OnExit();
            }
        }

        public ITransitionsMap Get()
        {
            ITransitionsMap result = new TransitionsMap();
            foreach (var transition in _transitions)
            {
                result.AddFromState(transition.fromState, transition.ToTransition());
            }
            return result;
        }
    }
}