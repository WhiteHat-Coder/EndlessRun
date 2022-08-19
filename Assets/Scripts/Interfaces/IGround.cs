using System;
using UnityEngine;

namespace Interfaces
{
    public interface IGround : IObject
    {
        /// <summary>
        /// OnGroundCreated event
        /// </summary>
        event Action OnGroundCreated;

        /// <summary>
        ///  SetupInitialGround
        /// </summary>
        void SetupInitialGround();
        
        /// <summary>
        /// InitialPlayerPosition
        /// </summary>
        Vector3 InitialPlayerPosition { get; }
        
        /// <summary>
        /// Position
        /// </summary>
        Vector3 Position { get; set; }
        
        /// <summary>
        /// SetUpPosition
        /// </summary>
        /// <param name="position"></param>
        /// <param name="activeState"></param>
        void SetUpPosition(Vector3 position, bool activeState);

        /// <summary>
        ///  ReachedEndPosition
        /// </summary>
        void ReachedEndPosition();

    }
}