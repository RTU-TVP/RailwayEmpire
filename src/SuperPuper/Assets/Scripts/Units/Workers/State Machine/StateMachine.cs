using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Workers.State_Machine
{
    public class StateMachine
    {
        private readonly static List<Transition> EmptyTransitions = new List<Transition>(0);

        private readonly Dictionary<Type, List<Transition>> _transitions = new Dictionary<Type, List<Transition>>();

        private IState _currentState;

        private List<Transition> _currentTransitions = new List<Transition>();

        public void Tick()
        {
            var transition = GetTransition();
            if (transition != null) SetState(transition.To);

            _currentState?.Tick();
        }

        public void SetState(IState state)
        {
            if (state == _currentState)
                return;

            _currentState?.OnExit();
            _currentState = state;
            _transitions.TryGetValue(_currentState.GetType(), out _currentTransitions);
            _currentTransitions ??= EmptyTransitions;

            _currentState.OnEnter();

            Debug.Log(_currentState.GetType().Name);
        }

        public void AddTransition(IState to, IState from, Func<bool> predicate)
        {
            if (_transitions.TryGetValue(from.GetType(), out var transitions) == false)
            {
                transitions = new List<Transition>();
                _transitions[from.GetType()] = transitions;
            }

            transitions.Add(new Transition(to, predicate));
            Debug.Log(transitions.Count);
        }

        private Transition GetTransition()
        {
            return _currentTransitions.FirstOrDefault(transition => transition.Condition());
        }
    }
}
