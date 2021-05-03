namespace SaG.FSM
{
    public interface IStateMachineEntity
    {
        /// <summary>
        /// Called when entered state. Responsible for running first update on state entrance.
        /// </summary>
        void OnEnter();
        
        // Personally, I would like to name this methods without 'On' prefix,
        // but Unity doesn't allow to use method with name 'Update' that has arguments. 
        /// <summary>
        /// Called when state isn't changed. If StateMachine entered or exited state, then this method won't be called this frame.
        /// </summary>
        /// <param name="deltaTime">Time since last frame. In most cases equals to Time.deltaTime</param>
        void OnUpdate(float deltaTime);
        
        /// <summary>
        /// Called when exited state. Responsible for running last update on state exit.
        /// </summary>
        void OnExit();
    }
}