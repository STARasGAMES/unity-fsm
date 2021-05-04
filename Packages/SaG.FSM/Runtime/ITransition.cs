namespace SaG.FSM
{
    public interface ITransition : IStateMachineEntity
    {
        // There is no 'source state' because there is AnyState transitions, which obviously doesn't have source state.
        
        /// <summary>
        /// Returns state where transition leads.
        /// </summary>
        IState TargetState { get; }
        
        /// <summary>
        /// Indicates whether this transition should be ignored.
        /// </summary>
        bool IsMuted { get; }
        
        /// <summary>
        /// Returns value indicating whether this transition should occur. 
        /// </summary>
        /// <returns></returns>
        bool Evaluate();
    }
}