using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    internal void LoadScene(int sceneCode, Action OnLoaded)
    {
        switch (sceneCode)
        {
            case 0:
                StartCoroutine(AsyncLoading(OnLoaded));
                break;
        }
    }
    AsyncOperation op;
    public float loadingValue;
    IEnumerator AsyncLoading(Action OnLoaded)
    {
        op = SceneManager.LoadSceneAsync("SampleScene", LoadSceneMode.Additive);

        //阻止当加载完成自动切换  
        //op.allowSceneActivation = false;  

        yield return op;
        OnLoaded?.Invoke();
    }
    void Update()
    {
        if (op != null)
        {
            loadingValue = op.progress;

            if (op.progress >= 0.9f)
            {
                //operation.progress的值最大为0.9  
                loadingValue = 1.0f;
                FindObjectOfType<GameManager>().LoadingScene = false;
            }
        }
    }
}