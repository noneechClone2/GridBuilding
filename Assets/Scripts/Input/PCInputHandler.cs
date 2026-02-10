using System;
using UnityEngine;

namespace InputHandlers
{
    public class PCInputHandler : MonoBehaviour, IInputHandler
    {
        public event Action MouseButtonPressed;
        public event Action MouseDragged;
        public event Action MouseButtonUnpressed;
        
        private bool _isDragging;

        private void Update()
        {
            if (_isDragging == false && Input.GetMouseButtonDown(0))
            {
                _isDragging = true;
                MouseButtonPressed?.Invoke();
            }
            else if (_isDragging && Input.GetMouseButton(0))
            {
                MouseDragged?.Invoke();

            } else if (Input.GetMouseButtonUp(0) && _isDragging)
            {
                _isDragging = false;
                MouseButtonUnpressed?.Invoke();
            }
        }
    }
}