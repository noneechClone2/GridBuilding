using System;
using UnityEngine;

namespace InputHandlers
{
    public interface IInputHandler 
    {
        public event Action MouseButtonPressed;
        public event Action MouseDragged;

        public event Action MouseButtonUnpressed;
    }
}