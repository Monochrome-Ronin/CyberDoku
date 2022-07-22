using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    [SerializeField] private Button startButton;
    void Start()
    {
        startButton.onClick.AddListener(ClickStart);
    }

    // Update is called once per frame
   public void ClickStart()
   {
       SceneManager.LoadScene(1);
   }
}
