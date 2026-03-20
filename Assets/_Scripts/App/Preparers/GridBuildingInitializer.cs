using System.Collections.Generic;
using App.Initializing.Operations;
using Data.Loaders;
using Grid;
using Grid.Cells;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace App.Initializing
{
    public class GridBuildingInitializer : IInitializable
    {
        // private readonly string GameScene = "Game";
        private readonly GridController _gridController;
        private readonly CellsLoadHandler _cellsLoadHandler;
        
        public GridBuildingInitializer(GridController gridController, CellsLoadHandler cellsLoadHandler)
        {
            _gridController = gridController;
            _cellsLoadHandler = cellsLoadHandler;
        }


        public async void Initialize()
        {
            var operationGroup = new OperationGroupPerformer(new List<IOperation>()
            {
                new OperationFromAction((() => _gridController.Initialize())),
                new OperationFromAction((() => _cellsLoadHandler.Initialize()))
            });

            await operationGroup.DoOperations();
            // await SceneManager.LoadSceneAsync(GameScene);
        }
    }
}