using Builders;
using Data;
using Grid;
using Grid.Cells;
using UnityEngine;
using Zenject;

public class ProjectInstaller : MonoInstaller
{
    [SerializeField] private GridView _gridView;
    [SerializeField] private BuildHandler _buildHandler;
    [SerializeField] private CoroutinePerformer _coroutineStarter;
    [SerializeField] private GridController _gridController;
    [SerializeField] private DataLoader _dataLoader;

    public override void InstallBindings()
    {
        BindData();
        BindGrid();
        BindBuilders();

        Container.Bind<CoroutinePerformer>().FromInstance(_coroutineStarter).AsSingle();
    }

    private void BindData()
    {
        Container.Bind<DataLoader>().FromInstance(_dataLoader).AsSingle();
        Container.Bind<BaseStorage>().AsSingle();
    }

    private void BindBuilders()
    {
        Container.BindInterfacesAndSelfTo<Builder>().AsSingle().NonLazy();
        Container.Bind<BuildHandler>().FromInstance(_buildHandler).AsSingle();
    }

    private void BindGrid()
    {
        Container.Bind<GridController>().FromInstance(_gridController).AsSingle();
        Container.Bind<GridView>().FromInstance(_gridView).AsSingle();
        Container.Bind<GridModel>().AsSingle();
        Container.Bind<GridViewShower>().AsSingle();
        Container.BindInterfacesAndSelfTo<CellColorChanger>().AsSingle().NonLazy();
        Container.Bind<GridCollection>().AsSingle();
    }
}