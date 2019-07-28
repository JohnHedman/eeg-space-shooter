using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scoreControl : MonoBehaviour
{

    public int playerScore;

    //public GameObject TextGUI;

    public Text scoreText;

    // Start is called before the first frame update
    void Start()
    {
        //scoreText = GameObject.FindGameObjectWithTag("ScoreText");
        playerScore = 0;
        UpdateScore();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void AddScore(int value)
    {
        playerScore += value;
        UpdateScore();
    }

    public void LossScore(int value)
    {
        playerScore -= value;
        UpdateScore();
    }

    private void UpdateScore()
    {
        scoreText.text = "Score: " + playerScore.ToString();
    }
}
