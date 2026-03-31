using UnityEngine.SceneManagement;
using Zenject;

namespace App.Initializing.Installers
{
    public class Bootstrapper : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<ProjectPreparer>().AsSingle().NonLazy();
        }
    }
}