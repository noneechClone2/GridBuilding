using Builders;
using Cysharp.Threading.Tasks;
using Grid;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace App.Initializing
{
    public class ProjectPreparer
    {
        private readonly string MenuSceneName = "Game";
            
        
        
        public ProjectPreparer()
        {
            
            Initialize().Forget();
        }

        private async UniTask Initialize()
        {
            await SceneManager.LoadSceneAsync(MenuSceneName);
        }
    }
}