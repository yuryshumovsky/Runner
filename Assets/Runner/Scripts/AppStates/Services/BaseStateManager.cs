using System.Collections.Generic;
using Runner.Scripts.Interfaces;
using UnityEngine;
using Zenject;

namespace Runner.Scripts.AppStates.Services
{
    /// <summary>
    /// Provides a base implementation for state change workflow.
    /// </summary>
    public abstract class BaseStateManager<T> : ITickable, IFixedTickable
    {
        protected T _currentState;
        protected IGameState _currentStateHandler;
        protected Dictionary<T, IGameState> _states;

        public void ChangeState(T state)
        {
            if (_currentState.Equals(state))
            {
                Debug.Log($"We're already in this state {state}");
                return;
            }

            _currentState = state;
            if (_currentStateHandler != null)
            {
                _currentStateHandler.ExitState();
                _currentStateHandler = null;
            }

            _currentStateHandler = _states[state];

            _currentStateHandler.EnterState();
        }

        public T CurrentState
        {
            get { return _currentState; }
        }

        public void Tick()
        {
            _currentStateHandler.Update();
        }

        public void FixedTick()
        {
            _currentStateHandler.FixedUpdate();
        }
    }
}