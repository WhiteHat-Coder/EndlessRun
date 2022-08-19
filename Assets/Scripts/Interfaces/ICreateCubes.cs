using System;
using UnityEngine;

namespace Interfaces
{
    public interface ICreateCubes
    {
        /// <summary>
        /// Create cubes
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        void Create(float x, float y, Action<GameObject> clonedObjet);
    }
}