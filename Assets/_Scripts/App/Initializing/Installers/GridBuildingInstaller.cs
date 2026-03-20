using Builders;
using Buildings;
using Data.Loaders;
using Grid;
using Grid.Cells;
using UnityEngine;
using Zenject;

namespace App.Initializing.Installers
{
    public class GridBuildingInstaller : MonoInstaller
    {
        [SerializeField] private BuildHandler _buildHandler;
        [SerializeField] private GridView _gridView;
        [SerializeField] private BuildingData _buildingData;
        [SerializeField] private GridLoader _gridLoader;
        [SerializeField] private CoroutinePerformer _coroutinePerformer;
        [SerializeField] private BuildingsPrefabsCollection _buildingsPrefabsCollection;
        
        public override void InstallBindings()
        {
            BindGrid();
            BindBuilders();
            BindData();
            BindCollections();
            BindInitializer();
            
            Container.Bind<CoroutinePerformer>().FromInstance(_coroutinePerformer).AsSingle();
        }

        private void BindInitializer()
        {
            Container.BindInterfacesAndSelfTo<GridBuildingInitializer>().AsSingle().NonLazy();
        }

        private void BindData()
        {
            Container.Bind<GridLoader>().FromInstance(_gridLoader).AsSingle();
            Container.Bind<BuildingData>().FromInstance(_buildingData).AsSingle();
            Container.BindInterfacesAndSelfTo<BuildingsLoadHandler>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<CellsLoadHandler>().AsSingle().NonLazy();
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
            Container.BindInterfacesAndSelfTo<Builder>().AsSingle().NonLazy();
            Container.Bind<BuildHandler>().FromInstance(_buildHandler).AsSingle();
        }

        private void BindCollections()
        {
            Container.Bind<BuildingsPrefabsCollection>().FromInstance(_buildingsPrefabsCollection).AsSingle();
        }
    }
}