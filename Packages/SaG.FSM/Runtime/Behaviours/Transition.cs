using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SaG.FSM.Behaviours
{
    public class Transition : MonoBehaviour, ITransition
    {
        [SerializeField] protected State _targetState = default;

        private List<ICondition> _conditions;
        
        public virtual IState TargetState => _targetState;

        public virtual bool IsMuted => gameObject.activeSelf;

        private void Awake()
        {
            _conditions = new List<ICondition>();
            foreach (Transform t in transform)
            {
                if (t.TryGetComponent<ICondition>(out var condition))
                {
                    _conditions.Add(condition);
                }
            }
        }

        public bool Evaluate()
        {
            return _conditions.All(c => c.Value);
        }

        public virtual void OnEnter()
        {
            foreach (var condition in _conditions)
            {
                condition.OnEnter();
            }
        }

        public virtual void OnUpdate(float deltaTime)
        {
            foreach (var condition in _conditions)
            {
                condition.OnUpdate(deltaTime);
            }
        }

        public virtual void OnExit()
        {
            foreach (var condition in _conditions)
            {
                condition.OnExit();
            }
        }
    }
}