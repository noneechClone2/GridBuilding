using _Scripts.GridBuilding.View;
using Data;
using Data.Loaders;
using GridBuilding.Builders;
using GridBuilding.Buildings;
using GridBuilding.Grid;
using GridBuilding.Grid.Cells;
using GridBuilding.Grid.View;
using InputHandlers.PlayerInputState;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace App.Initializing.Installers
{
    public class GridBuildingInstaller : MonoInstaller
    {
        [SerializeField] private GridView _gridView;
        
        [SerializeField] private CanvasRaycaster _canvasRaycaster;
        
        [SerializeField] private BuildHandler _buildHandler;
        [SerializeField] private BuildingMaterials buildingMaterials;
        [SerializeField] private BuildingsPrefabsCollection _buildingsPrefabsCollection;
        
        [SerializeField] private CoroutinePerformer _coroutinePerformer;
        [SerializeField] private GameSaveLoadService _gameSaveLoadService;
        [SerializeField] private GridBuildingPlayerInputStateChanger _gridBuildingPlayerInputStateChanger;
        
        [SerializeField] private EditingButtons _editingButtons;
        
        public override void InstallBindings()
        {
            BindGrid();
            BindBuilders();
            BindSavesAndLoads();
            BindData();
            BindCollections();
            BindInitializer();
            BindInput();
            BindCanvasRaycaster();
            BindEditingButtons();
            
            Container.Bind<CoroutinePerformer>().FromInstance(_coroutinePerformer).AsSingle();
        }

        private void BindCanvasRaycaster()
        {
            Container.Bind<CanvasRaycaster>().FromInstance(_canvasRaycaster).AsSingle();
        }

        private void BindEditingButtons()
        {
            Container.Bind<EditingButtons>().FromInstance(_editingButtons).AsSingle();
        }

        private void BindInitializer()
        {
            Container.BindInterfacesAndSelfTo<GridBuildingPreparer>().AsSingle().NonLazy();
        }

        private void BindData()
        {
            Container.Bind<BuildingMaterials>().FromInstance(buildingMaterials).AsSingle();
        }

        private void BindSavesAndLoads()
        {
            Container.Bind<GridLoader>().AsSingle();
            Container.BindInterfacesAndSelfTo<CellsLoadHandler>().AsSingle();
            
            Container.Bind<BuildingsLoader>().AsSingle();
            Container.Bind<BuildingsLoadHandler>().AsSingle();
            
            Container.Bind<GameSaveLoadService>().FromInstance(_gameSaveLoadService).AsSingle();
        }

        private void BindGrid()
        {
            Container.Bind<GridController>().AsSingle();
            Container.Bind<GridView>().FromInstance(_gridView).AsSingle();
            Container.Bind<GridModel>().AsSingle();
            Container.Bind<GridViewShower>().AsSingle();
            Container.BindInterfacesAndSelfTo<CellColorChanger>().AsSingle();
            Container.Bind<GridCollection>().AsSingle();
        }
        
        private void BindBuilders()
        {
            Container.Bind<BuildingMover>().AsSingle();
            Container.Bind<BuildHandler>().FromInstance(_buildHandler).AsSingle();
            Container.Bind<BuildingRotator>().AsSingle();
        }

        private void BindCollections()
        {
            Container.Bind<BuildingsPrefabsCollection>().FromInstance(_buildingsPrefabsCollection).AsSingle();
        }

        private void BindInput()
        {
            Container.Bind<GridBuildingPlayerInputStateChanger>().FromInstance(_gridBuildingPlayerInputStateChanger).AsSingle();
        }
    }
}