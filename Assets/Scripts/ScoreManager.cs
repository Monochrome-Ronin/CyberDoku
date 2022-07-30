using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ScoreManager : MonoBehaviour
{
    [SerializeField] TMP_Text scoreText;
    [SerializeField] ParticleSystem hit;
    private int score;

    void Update()
    {
        scoreText.text = score.ToString();
        AddOnePoint();
    }

    public void AddOnePoint()
    {
        score++;
        hit.Play();
    }

    public void AddNinePoint()
    {
        score += 9;
    }
}
