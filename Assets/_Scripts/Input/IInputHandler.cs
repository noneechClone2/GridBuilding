using System;
using UnityEngine;

namespace InputHandlers
{
    public interface IInputHandler 
    {
        public event Action LeftMouseButtonStartDragging;
        public event Action<Vector3Int> BuildingMoved;
        public event Action LeftMouseButtonStoppedDragging;
    }
}