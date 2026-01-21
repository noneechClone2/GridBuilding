using Builders;
using Grid;
using UnityEngine;
using Zenject;

public class ProjectInstaller : MonoInstaller
{
    [SerializeField] private GridView _gridView;
    [SerializeField] private BuildHandler _buildHandler;
    [SerializeField] private GridController _gridPresenter;
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<Builder>().AsSingle().NonLazy();
        Container.Bind<BuildHandler>().FromInstance(_buildHandler).AsSingle();
        Container.Bind<GridController>().FromInstance(_gridPresenter).AsSingle();
        Container.Bind<GridView>().FromInstance(_gridView).AsSingle();
        Container.Bind<GridModel>().AsSingle();
    }
}