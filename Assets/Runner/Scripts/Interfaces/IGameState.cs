﻿namespace Runner.Scripts.Interfaces
{
    public interface IGameState
    {
        void EnterState();
        void ExitState();
        void Update();
        void FixedUpdate();
    }
}