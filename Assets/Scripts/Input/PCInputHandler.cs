using System;
using UnityEngine;

namespace Inputa
{
    public class PCInputHandler : MonoBehaviour, IInputHandler
    {
        public event Action ButtonPressed;
        public event Action<Vector3> ButtonDragged;
        public event Action ButtonUnpressed;
    }
}