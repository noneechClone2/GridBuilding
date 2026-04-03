using System;
using System.Collections.Generic;
using App.Initializing.Operations;
using Data;
using Data.Loaders;
using GridBuilding.Builders;
using GridBuilding.Buildings;
using GridBuilding.Grid;
using GridBuilding.Grid.Cells;
using GridBuilding.Grid.View;
using InputHandlers.PlayerInputState;
using Zenject;

namespace App.Initializing
{
    public class GridBuildingPreparer : IInitializable, IDisposable
    {
        // private readonly string GameScene = "Game";
        private readonly CellsLoadHandler _cellsLoadHandler;

        private readonly GridBuildingPlayerInputStateChanger _gridBuildingPlayerInputStateChanger;
        private readonly CurrentPlayerInputState _currentPlayerInputState;
        
        private readonly BuildingsLoadHandler _buildingsLoadHandler;
        private readonly BuildingsLoader _buildingsLoader;
        private readonly BuildHandler _buildHandler;
        private readonly BuildingMaterials _buildingMaterials;
        private readonly BuildingRotator _buildingRotator;
        private readonly BuildingMover _buildingMover;

        private readonly GridController _gridController;
        private readonly GridView _gridView;
        private readonly GridViewShower _gridViewShower;
        private readonly CellColorChanger _cellColorChanger;
        private readonly GridCollection _gridCollection;

        private List<IDisposable> _disposables;

        public GridBuildingPreparer(CellsLoadHandler cellsLoadHandler,
            GridBuildingPlayerInputStateChanger gridBuildingPlayerInputStateChanger,
            CurrentPlayerInputState currentPlayerInputState, BuildingsLoadHandler buildingsLoadHandler, BuildingsLoader buildingsLoader,
            BuildHandler buildHandler, BuildingMaterials buildingMaterials, BuildingRotator buildingRotator,
            BuildingMover buildingMover,
            GridController gridController, GridView gridView, GridViewShower gridViewShower,
            CellColorChanger cellColorChanger, GridCollection gridCollection)
        {
            _cellsLoadHandler = cellsLoadHandler;

            _currentPlayerInputState = currentPlayerInputState;
            _gridBuildingPlayerInputStateChanger = gridBuildingPlayerInputStateChanger;

            _buildingsLoadHandler = buildingsLoadHandler;
            _buildingsLoader = buildingsLoader;
            _buildHandler = buildHandler;
            _buildingMaterials = buildingMaterials;
            _buildingRotator = buildingRotator;
            _buildingMover = buildingMover;

            _gridController = gridController;
            _gridView = gridView;
            _gridViewShower = gridViewShower;
            _cellColorChanger = cellColorChanger;
            _gridCollection = gridCollection;

            _disposables = new List<IDisposable>()
                { _cellsLoadHandler, _buildingMover, _buildingRotator, _gridController, _cellColorChanger, _buildingsLoadHandler };
        }


        public async void Initialize()
        {
            var operationGroup = new OperationGroupPerformer(new List<IOperation>()
            {
                new OperationFromAction(() => _cellsLoadHandler.Initialize()),

                new OperationFromAction(() =>
                    _gridBuildingPlayerInputStateChanger.Initialize(_currentPlayerInputState)),
                
                new OperationFromAction(() => _buildingsLoadHandler.Initialize()),
                new OperationFromAction(() => _buildingsLoader.Initialize()),
                new OperationFromAction(() => _buildHandler.Initialize(_buildingMaterials)),
                new OperationFromAction(() => _buildingRotator.Initialize()),
                new OperationFromAction((() =>  _buildingMover.Initialize())), 

                new OperationFromAction(() => _gridView.Initialize(_gridViewShower, _gridCollection)),
                new OperationFromAction(() => _gridController.Initialize()),
                new OperationFromAction(() => _cellColorChanger.Initialize()),
            });

            await operationGroup.DoOperations();

            // await SceneManager.LoadSceneAsync(GameScene);
        }

        public void Dispose()
        {
            foreach (var disposable in _disposables)
                disposable.Dispose();
        }
    }
}