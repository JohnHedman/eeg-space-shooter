using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class changeMedium : MonoBehaviour
{
    public void LoadMediumLevel()
    {
        SceneManager.LoadScene("mediumLevel", LoadSceneMode.Single);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
