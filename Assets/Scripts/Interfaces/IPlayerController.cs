using System;

namespace Interfaces
{
    public interface IPlayerController : ISetup
    {
        event Action<int> OnCollectedGreenCube;
        event Action OnCollectedRedCube;
    }
}