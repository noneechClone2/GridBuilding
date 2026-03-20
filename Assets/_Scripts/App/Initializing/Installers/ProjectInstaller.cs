using Builders;
using Buildings;
using Data;
using Data.Loaders;
using Grid.Cells;
using InputHandlers;
using UnityEngine;
using Zenject;

namespace App.Initializing.Installers
{
    public class ProjectInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindInput();
            
            Container.Bind<BaseStorage>().AsSingle();
        }

        private void BindInput()
        {
            Container.BindInterfacesAndSelfTo<PCInputHandler>().AsSingle();
        }
    }   
}