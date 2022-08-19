using UnityEngine;

namespace Interfaces
{
    public interface IPoolObjects
    {
        /// <summary>
        /// SpawnPlayer
        /// </summary>
        /// <returns></returns>
        GameObject SpawnPlayer();
        
        /// <summary>
        /// SpawnController
        /// </summary>
        void SpawnController();

        /// <summary>
        /// SpawnGround
        /// </summary>
        void SpawnGround();
        
        /// <summary>
        /// SetUpGroundPosition
        /// </summary>
        /// <param name="target"></param>
        /// <param name="position"></param>
        void SetUpGround(Vector3 position);
    }
}