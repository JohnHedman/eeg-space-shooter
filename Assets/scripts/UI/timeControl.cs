using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class timeControl : MonoBehaviour
{

    public float playerTime = 120.0f;

    public Text timeText;
    public Text finalScoreText;


    private float endGameTimer;
    private bool gameHasEnded;


    // Start is called before the first frame update
    void Start()
    {
        gameHasEnded = false;
        UpdateTimer();
    }

    // Update is called once per frame
    void Update()
    {
        endGameTimer -= Time.deltaTime;
        playerTime -= Time.deltaTime;

        if(playerTime <= 0)
        {
            playerTime = 0.0f;

            if(!gameHasEnded)
            {
                EndGame();
            }
            else
            {
                DestroyEnemies();
            }
        }

        UpdateTimer();
    }
    
    public void UpdateTimer()
    {
        timeText.text = "Time: " + string.Format("{0:0.#}", playerTime);
    }


    public void EndGame()
    {
        gameHasEnded = true;
        //DestroySpawners();
        DestroyEnemies();
        StopStreams();
        Invoke("DisplayFinalScore", 1.0f);
        Invoke("BringToMainMenu", 5.0f);
    }


    public void DestroyEnemies()
    {
        string[] tags = new string[]{"EnemyShip1", "EnemyShip2", "EnemyShip3", "EnemyShip4"};

        foreach (string tag in tags)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag(tag);

            foreach (GameObject enemy in enemies)
            {
                enemy.GetComponent<Animator>().SetBool("Death", true);
                Destroy(enemy, .45f);
            }
        }
    }

    public void StopStreams()
    {
        try
        {
            GameObject.FindGameObjectWithTag("socketComs").GetComponent<sendEasy>().EndServerStream();
        }
        catch
        {
            try
            {
                GameObject.FindGameObjectWithTag("socketComs").GetComponent<sendMedium>().EndServerStream();
            }
            catch
            {
                try
                {
                    GameObject.FindGameObjectWithTag("socketComs").GetComponent<sendHard>().EndServerStream();
                }
                catch
                {
                    try
                    {
                        GameObject.FindGameObjectWithTag("socketComs").GetComponent<manageAdaptive>().EndServerStream();
                    }
                    catch
                    {
                        Debug.Log("Had problem with ending server stream!");
                    }
                }
            }
        }
    }

    public void DisplayFinalScore()
    {
        int finalScore = this.GetComponent<scoreControl>().playerScore;

        finalScoreText.text = "Final Score: " + finalScore.ToString();
        finalScoreText.color = Color.white;
    }

    public void BringToMainMenu()
    {
        SceneManager.LoadScene("mainMenu", LoadSceneMode.Single);
    }


}
