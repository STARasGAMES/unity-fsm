using UnityEngine;

namespace SaG.FSM.Mono
{
    public abstract class MonoStateAction : MonoBehaviour, IStateAction
    {
        public virtual void OnEnter() { }

        public virtual void OnUpdate(float deltaTime) { }

        public virtual void OnExit() { }
    }
}