namespace SaG.FSM
{
    public interface ICondition : IStateMachineEntity
    {
        bool Value { get; }
    }
}