using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ScoreManager : MonoBehaviour
{
    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text _bestScore;
    [SerializeField] ParticleSystem hit;
    private int score;

    private void Awake() {
        _bestScore.text = PlayerPrefs.GetInt("BestScore").ToString();
    }

    void Update()
    {
        scoreText.text = score.ToString();
        PlayerPrefs.SetInt("Score", score);
        if(PlayerPrefs.GetInt("Score") > PlayerPrefs.GetInt("BestScore")){
            PlayerPrefs.SetInt("BestScore", PlayerPrefs.GetInt("Score"));
        }
    }

    public void AddOnePoint()
    {
        score++;
        hit.Play();
    }

    public void AddNinePoint()
    {
        score += 9;
        hit.Play();
    }
    //������� ��� ���� � ��� ����������
}
