using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerRespawn : MonoBehaviour
{

    public GameObject playerShip;
    public Vector3 playerSpawnLocation = new Vector3(0, -15, 0);

    public Vector3 playerSpawnRotation = new Vector3(0,0,0);

    private bool playerSpawn = false;
    private float spawnTime = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        playerSpawn = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(playerSpawn)
        {
            spawnTime -= Time.deltaTime;
            if(spawnTime <= 0)
            {
                playerSpawn = false;
                spawnTime = 1.0f;
                GameObject newPlayer = (GameObject)Instantiate(playerShip, playerSpawnLocation, Quaternion.Euler(playerSpawnRotation));
                newPlayer.GetComponent<playerManager>().ActivateInvulnerability(2);
            }
        }
        
    }

    public void respawnPlayer(int delay)
    {
        playerSpawn = true;
        spawnTime = delay;
    }
}
