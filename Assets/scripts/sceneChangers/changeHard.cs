using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class changeHard : MonoBehaviour
{
    public void LoadHardLevel()
    {
        SceneManager.LoadScene("hardLevel", LoadSceneMode.Single);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
