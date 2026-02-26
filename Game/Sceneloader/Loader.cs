using System;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Loader : Singleton<Loader> {
    [SerializeField] Image _progressBar;
    [SerializeField] GameObject _loadingCanvas;
    [SerializeField] TextMeshProUGUI _taskText;
    public void Show(string task) {
        _loadingCanvas?.SetActive(true);      
        _taskText.text = task;
    }

    public void Hide() => _loadingCanvas?.SetActive(false);

    public async Task Play(Func<Task> complete, string task, bool hideonfinish = true) {

        Loader.Instance.Show(task);
        float fakeProgress = 0f;

        while (fakeProgress < 1f) {
            fakeProgress += 0.02f;
            if (_progressBar != null)
                _progressBar.fillAmount = fakeProgress;

            await System.Threading.Tasks.Task.Delay(10);
        }            

        if (complete != null)
            await complete();

        if (hideonfinish)
            Instance.Hide();
    }
}