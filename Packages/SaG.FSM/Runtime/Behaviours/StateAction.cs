using UnityEngine;

namespace SaG.FSM.Behaviours
{
    public abstract class StateAction : MonoBehaviour, IStateAction
    {
        public virtual void OnEnter() { }

        public virtual void OnUpdate(float deltaTime) { }

        public virtual void OnExit() { }
    }
}