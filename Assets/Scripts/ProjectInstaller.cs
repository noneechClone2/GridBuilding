using Builders;
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
    
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<Builder>().AsSingle().NonLazy();
        Container.Bind<BuildHandler>().FromInstance(_buildHandler).AsSingle();

        Container.Bind<GridController>().FromInstance(_gridController).AsSingle();
        Container.Bind<GridView>().FromInstance(_gridView).AsSingle();
        Container.Bind<GridModel>().AsSingle();
        Container.Bind<GridViewShower>().AsSingle();
        Container.BindInterfacesAndSelfTo<CellAvailabilityChanger>().AsSingle().NonLazy();
        Container.Bind<GridCollection>().AsSingle();

        Container.Bind<CoroutinePerformer>().FromInstance(_coroutineStarter).AsSingle();
    }
}