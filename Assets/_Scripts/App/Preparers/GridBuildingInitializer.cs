using System.Collections.Generic;
using App.Initializing.Operations;
using Grid;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace App.Initializing
{
    public class GridBuildingInitializer : IInitializable
    {
        // private readonly string GameScene = "Game";
        private readonly GridController _gridController;
        
        public GridBuildingInitializer(GridController gridController)
        {
            _gridController = gridController;
        }


        public async void Initialize()
        {
            var operationGroup = new OperationGroupPerformer(new List<IOperation>()
            {
                new OperationFromAction((() => _gridController.Initialize()))
            });

            await operationGroup.DoOperations();
            // await SceneManager.LoadSceneAsync(GameScene);
        }
    }
}