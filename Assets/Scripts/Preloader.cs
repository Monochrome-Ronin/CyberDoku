using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Preloader : MonoBehaviour
{
    [SerializeField] private Image _loadingImage;

    private void Start()
    {
        StartCoroutine(AsyncLoad(2));
    }
    IEnumerator AsyncLoad(int sceneID){
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneID);
        while(!operation.isDone){
            float progress = operation.progress / 0.9f;
            _loadingImage.fillAmount = progress;
            yield return null;
        }
    }
}
