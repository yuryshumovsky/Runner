using System;

namespace Runner.Scripts.Interfaces
{
    public interface IInitController
    {
        Action<IInitController> OnReady { get; set; }
        Action<string> OnError { get; set; }

        void Init();
    }
}