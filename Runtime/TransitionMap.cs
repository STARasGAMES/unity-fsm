using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace SaG.FSM
{
    public class TransitionsMap : ITransitionsMap
    {
        private readonly IDictionary<IState, IList<ITransition>> _fromStateTransitions = new Dictionary<IState, IList<ITransition>>();
        private readonly IList<ITransition> _fromAnyStateTransitions = new List<ITransition>();
        
        public void AddFromAnyState(ITransition transition)
        {
            if (transition == null)
                throw new ArgumentNullException(nameof(transition));
            _fromAnyStateTransitions.Add(transition);
        }

        public void AddFromState(IState state, ITransition transition)
        {
            if (state == null)
                throw new ArgumentNullException(nameof(state));
            if (transition == null)
                throw new ArgumentNullException(nameof(transition));
            if (!_fromStateTransitions.TryGetValue(state, out var transitions))
            {
                transitions = new List<ITransition>();
                _fromStateTransitions.Add(state, transitions);
            }
            transitions.Add(transition);
        }

        public IEnumerable<ITransition> GetTransitionsFromState(IState state)
        {
            if (state == null)
                throw new ArgumentNullException(nameof(state));
            if (!_fromStateTransitions.TryGetValue(state, out var transitions))
            {
                throw new ArgumentOutOfRangeException(nameof(state), $"{state} is not part of this transition map");
            }

            return transitions;
        }

        public IEnumerable<ITransition> GetTransitionsFromAnyState()
        {
            return _fromAnyStateTransitions;
        }
    }
}