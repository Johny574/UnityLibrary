using System.Threading.Tasks;
using UnityEngine;

public class SceneLoader : Singleton<SceneLoader> {
    public async System.Threading.Tasks.Task LoadScene(string scenename) {
        AsyncOperation scene = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(scenename);
        scene.allowSceneActivation = false;
        
        await Loader.Instance.Play(async () =>  {
            while (scene.progress < 0.9f) {
                await System.Threading.Tasks.Task.Delay(10);
            }
            scene.allowSceneActivation = true;
            await Task.Delay(100);
        }, $"Loading {scenename}");
    }
}