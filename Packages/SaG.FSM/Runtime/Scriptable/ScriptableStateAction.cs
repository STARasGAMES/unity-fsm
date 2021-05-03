using UnityEngine;

namespace SaG.FSM.Scriptable
{
    public abstract class ScriptableStateAction : ScriptableObject, IStateMachineEntity
    {
        public virtual void OnEnter() { }

        public virtual void OnUpdate(float deltaTime) { }

        public virtual void OnExit() { }
    }
}