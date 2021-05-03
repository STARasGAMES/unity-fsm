namespace SaG.FSM
{
    public interface ITransition : IStateMachineEntity
    {
        IState TargetState { get; }
        
        bool Evaluate();
    }
}