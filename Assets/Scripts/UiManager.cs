using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{
     [SerializeField] private Button startButton;
     
       private void Start()
        {
            startButton.onClick.AddListener(ClickStart);
        }
       
       private void ClickStart()
       {
           SceneManager.LoadScene(1);
       }
}
