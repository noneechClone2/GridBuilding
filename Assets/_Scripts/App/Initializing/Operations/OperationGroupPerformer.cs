using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace App.Initializing.Operations
{
    public class OperationGroupPerformer
    {
        private readonly List<IOperation> _operations;
        
        public OperationGroupPerformer(List<IOperation> operations)
        {
            _operations = operations;
        }
        
        public async UniTask DoOperations()
        {
            foreach (var operation in _operations)
            {
                operation.Perform();
                await UniTask.Yield();
            }
        }
    }
}