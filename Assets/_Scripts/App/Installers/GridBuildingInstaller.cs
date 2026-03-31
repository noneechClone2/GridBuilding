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
        
        [SerializeField] private BuildHandler _buildHandler;
        [FormerlySerializedAs("_buildingData")] [SerializeField] private BuildingMaterials buildingMaterials;
        [SerializeField] private BuildingsPrefabsCollection _buildingsPrefabsCollection;
        
        [SerializeField] private CoroutinePerformer _coroutinePerformer;
        [SerializeField] private GameSaveLoadService _gameSaveLoadService;
        [SerializeField] private GridBuildingPlayerInputStateChanger _gridBuildingPlayerInputStateChanger;
        
        public override void InstallBindings()
        {
            BindGrid();
            BindBuilders();
            BindSavesAndLoads();
            BindData();
            BindCollections();
            BindInitializer();
            BindInput();
            
            Container.Bind<CoroutinePerformer>().FromInstance(_coroutinePerformer).AsSingle();
        }

        private void BindInitializer()
        {
            Container.BindInterfacesAndSelfTo<GridBuildingInitializer>().AsSingle().NonLazy();
        }

        private void BindData()
        {
            Container.Bind<BuildingMaterials>().FromInstance(buildingMaterials).AsSingle();
        }

        private void BindSavesAndLoads()
        {
            Container.Bind<GridLoader>().AsSingle();
            Container.BindInterfacesAndSelfTo<BuildingsLoadHandler>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<CellsLoadHandler>().AsSingle().NonLazy();
            Container.Bind<GameSaveLoadService>().FromInstance(_gameSaveLoadService).AsSingle();
        }

        private void BindGrid()
        {
            Container.Bind<GridController>().AsSingle().NonLazy();
            Container.Bind<GridView>().FromInstance(_gridView).AsSingle();
            Container.Bind<GridModel>().AsSingle();
            Container.Bind<GridViewShower>().AsSingle();
            Container.BindInterfacesAndSelfTo<CellColorChanger>().AsSingle().NonLazy();
            Container.Bind<GridCollection>().AsSingle();
        }
        
        private void BindBuilders()
        {
            Container.BindInterfacesAndSelfTo<BuildingMover>().AsSingle().NonLazy();
            Container.Bind<BuildHandler>().FromInstance(_buildHandler).AsSingle();
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