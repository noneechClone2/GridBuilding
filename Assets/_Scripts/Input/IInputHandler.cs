using System;
using UnityEngine;

namespace InputHandlers
{
    public interface IInputHandler 
    {
        public event Action BuildingMovingStarted;
        public event Action<Vector3Int> BuildingMoved;
        public event Action BuildingMovingStopped;
        public event Action LeftMouseButtonClicked;
    }
}