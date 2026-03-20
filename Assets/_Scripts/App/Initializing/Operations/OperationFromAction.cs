using System;

namespace App.Initializing.Operations
{
    public class OperationFromAction : IOperation
    {
        private Action _action;
        
        public OperationFromAction(Action action)
        {
            _action = action;
        }

        public void Perform()
        {
            _action?.Invoke();
        }
    }
}