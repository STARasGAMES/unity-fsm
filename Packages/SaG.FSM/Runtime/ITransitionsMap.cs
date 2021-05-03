using System.Collections.Generic;

namespace SaG.FSM
{
    public interface ITransitionsMap
    {
        void AddFromAnyState(ITransition transition);
        
        void AddFromState(IState state, ITransition transition);

        IEnumerable<ITransition> GetTransitionsFromState(IState state);
        
        IEnumerable<ITransition> GetTransitionsFromAnyState();
    }
}