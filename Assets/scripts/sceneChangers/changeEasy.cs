using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class changeEasy : MonoBehaviour
{

    public void LoadEasyLevel()
    {
        SceneManager.LoadScene("easyLevel", LoadSceneMode.Single);
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
