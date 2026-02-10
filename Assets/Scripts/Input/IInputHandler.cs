using System;
using UnityEngine;

namespace Inputa
{
    public interface IInputHandler 
    {
        public event Action ButtonPressed;
        public event Action<Vector3> ButtonDragged;
        public event Action ButtonUnpressed;
    }
}