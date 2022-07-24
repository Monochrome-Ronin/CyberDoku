using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ScoreManager : MonoBehaviour
{
    [SerializeField] TMP_Text scoreText;
    private int score;

    void Update()
    {
        scoreText.text = score.ToString();
    }

    public void AddOnePoint() 
    {
        score++;
    }

    public void AddNinePoint()
    {
        score = score + 9;
    }
}
