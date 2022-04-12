using System;
using UnityEngine;

namespace ProjectAssets.Scripts.Player
{
    /// <summary>
    /// Controls the collision of the Current weapon using raycasts
    /// </summary>
    [Serializable]
    public class ObjectPosition
    {
        public GameObject gameObject;
        public Vector3 currentPosition;
        public Vector3 lastPosition;

        public ObjectPosition(GameObject go)
        {
            gameObject = go;

        }

    }
}