using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

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