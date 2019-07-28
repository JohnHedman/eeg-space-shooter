using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy04Manager : MonoBehaviour
{
    public bool hasSpawned = false;
    public float spawnSpeed = 15.0f;

    private int spawnX = 0;
    private int spawnY = 0;


    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<enemy04_AI>().enabled = false;
        hasSpawned = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!hasSpawned)
        {
            Vector3 location = this.transform.position;

            location.y -= spawnSpeed * Time.deltaTime;

            if(location.y < spawnY)
            {
                location.y = spawnY;
                hasSpawned = true;
                this.GetComponent<enemy04_AI>().enabled = true;
            }

            this.transform.position = location;
        }
        else
        {
            this.GetComponent<enemy04_AI>().DisableEnemyManager();
        }
    }

    public void setSpawnLocation(int x, int y)
    {
        spawnX = x;
        spawnY = y;
    }
}
