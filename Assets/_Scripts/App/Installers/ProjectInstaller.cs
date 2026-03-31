using Data;
using Data.Storages;
using InputHandlers;
using InputHandlers.PlayerInputState;
using UnityEngine;
using Zenject;

namespace App.Initializing.Installers
{
    public class ProjectInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindInput();
            BindData();
        }

        private void BindInput()
        {
            Container.BindInterfacesAndSelfTo<PCInputHandler>().AsSingle();
            Container.Bind<CurrentPlayerInputState>().AsSingle();
        }

        private void BindData()
        {
            // Debug.Log(Application.persistentDataPath);
            Container.Bind<IStorage>().To<PersistentDataStorage>().AsSingle();
        }
    }   
}