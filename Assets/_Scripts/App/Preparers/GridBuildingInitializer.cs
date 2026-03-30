using System.Collections.Generic;
using App.Initializing.Operations;
using GridBuilding.Builders;
using GridBuilding.Buildings;
using GridBuilding.Grid;
using GridBuilding.Grid.Cells;
using InputHandlers.PlayerInputState;
using Zenject;

namespace App.Initializing
{
    public class GridBuildingInitializer : IInitializable
    {
        // private readonly string GameScene = "Game";
        private readonly GridController _gridController;
        private readonly CellsLoadHandler _cellsLoadHandler;
        private readonly GridBuildingPlayerInputStateChanger _gridBuildingPlayerInputStateChanger;
        private readonly CurrentPlayerInputState _currentPlayerInputState;
        private readonly BuildHandler _buildHandler;
        private readonly BuildingData _buildingData;

        public GridBuildingInitializer(GridController gridController, CellsLoadHandler cellsLoadHandler,
            GridBuildingPlayerInputStateChanger gridBuildingPlayerInputStateChanger,
            CurrentPlayerInputState currentPlayerInputState, BuildHandler buildHandler, BuildingData buildingData)
        {
            _gridController = gridController;
            _cellsLoadHandler = cellsLoadHandler;
            _gridBuildingPlayerInputStateChanger = gridBuildingPlayerInputStateChanger;
            _currentPlayerInputState = currentPlayerInputState;
            _buildHandler = buildHandler;
            _buildingData = buildingData;
        }


        public async void Initialize()
        {
            var operationGroup = new OperationGroupPerformer(new List<IOperation>()
            {
                new OperationFromAction(() => _gridController.Initialize()),
                new OperationFromAction(() => _cellsLoadHandler.Initialize()),
                new OperationFromAction(() =>
                    _gridBuildingPlayerInputStateChanger.Initialize(_currentPlayerInputState)),
                new OperationFromAction(() => _buildHandler.Initialize(_buildingData)),
            });

            // _playerInputStateChanger.ChangePlayerInputState(PlayerInputStates.Editor);

            await operationGroup.DoOperations();

            // await SceneManager.LoadSceneAsync(GameScene);
        }
    }
}