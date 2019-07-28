using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class tutorialLevelManager : MonoBehaviour
{

    public GameObject kamikazeEnemy;
    public GameObject normalEnemy;
    public GameObject redHeavyGunner;
    public GameObject greenHeavyGunner;
    public GameObject tripplePowerup;
    public GameObject healthPowerup;
    public GameObject shieldPowerup;

    private float powerupCooldown = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        powerupCooldown -= Time.deltaTime;

        if(Input.GetButtonDown("AButton"))
        {
            SpawnNormalEnemy();
        }

        if(Input.GetButtonDown("BButton"))
        {
            SpawnRedHeavyGunner();
        }

        if(Input.GetButtonDown("XButton"))
        {
            SpawnKamikazeEnemy();
        }

        if(Input.GetButtonDown("YButton"))
        {
            SpawnGreenHeavyGunner();
        }

        if(Input.GetButtonDown("Start"))
        {
            SceneManager.LoadScene("mainMenu", LoadSceneMode.Single);
        }

        if(Input.GetAxis("DPadVertical") >= 0.9 && powerupCooldown <= 0.0)
        {
            powerupCooldown = 3.0f;
            SpawnTripplePowerup();
        }

        if(Input.GetAxis("DPadHorizontal") <= -0.9 && powerupCooldown <= 0.0)
        {
            powerupCooldown = 3.0f;
            SpawnHealthPowerup();
        }

        if(Input.GetAxis("DPadHorizontal") >=  0.9 && powerupCooldown <= 0.0)
        {
            powerupCooldown = 3.0f;
            SpawnShieldPowerup();
        }


    }

    int GetNumberOfEnemies()
    {
        GameObject[] enemy1 = GameObject.FindGameObjectsWithTag("EnemyShip1");
        GameObject[] enemy2 = GameObject.FindGameObjectsWithTag("EnemyShip2");
        GameObject[] enemy3 = GameObject.FindGameObjectsWithTag("EnemyShip3");
        GameObject[] enemy4 = GameObject.FindGameObjectsWithTag("EnemyShip4");

        return enemy1.Length + enemy2.Length + enemy3.Length + enemy4.Length;
    }

    void SpawnTripplePowerup()
    {
        SpawnPowerup(tripplePowerup);
    }

    void SpawnHealthPowerup()
    {
        SpawnPowerup(healthPowerup);
    }

    void SpawnShieldPowerup()
    {
        SpawnPowerup(shieldPowerup);
    }

    void SpawnPowerup(GameObject powerupPrefab)
    {
        int xMin = -18; int xMax = 18;
        int yMin = -10; int yMax = -10;
        int spawnX; int spawnY;

        int tries = 0;

        do
        {
            spawnX = Random.Range(xMin, xMax);
            spawnY = yMax;

            tries++;
            if(tries == 10)
            {
                Debug.Log("Can't find space to spawn powerup!");
            }
            
        } while (IsPowerupThere(spawnX, spawnY));

        Vector3 spawnLocation = new Vector3(spawnX, spawnY, 0);
        Quaternion rotation = Quaternion.Euler(0,0,0);

        GameObject trippleShotSpawn = (GameObject)Instantiate(powerupPrefab, spawnLocation, rotation);
    }

    void SpawnNormalEnemy()
    {
        int xMin = -18; int xMax = 18;
        int yMin =  -3; int yMax = 9;
        int spawnX; int spawnY;

        int tries = 0;

        do
        {
            spawnX = Random.Range(xMin/3, xMax/3) * 3;
            spawnY = (int)(RandomFromDistribution.RandomRangeExponential(yMin/3, yMax/3, 2, RandomFromDistribution.Direction_e.Right)) * 3;

            tries++;
            if(tries == 10)
            {
                Debug.Log("Can't find space to spawn normal enemy!");
                return;
            }

        } while (IsEnemyThere(spawnX, spawnY, "EnemyShip1"));

        Vector3 spawnLocation = new Vector3(spawnX, 30, 0);
        Quaternion roation = Quaternion.Euler(0,0,0);

        GameObject normalEnemySpawn = (GameObject)Instantiate(normalEnemy, spawnLocation, roation);
        normalEnemySpawn.GetComponent<enemy01Manager>().setSpawnLocation(spawnX, spawnY);

    }

    void SpawnKamikazeEnemy()
    {
        int xMin = -18; int xMax = 18;
        int yMin =  12; int yMax = 12;
        int spawnX; int spawnY;

        int tries = 0;

        do
        {
            spawnX = Random.Range(xMin/3, xMax/3) * 3;
            spawnY = yMax;

            tries++;
            if(tries == 10)
            {
                Debug.Log("Can't find space to spawn kamikaze enemy!");
                return;
            }
        } while(IsEnemyThere(spawnX, spawnY, "EnemyShip3"));

        Vector3 spawnLocation = new Vector3(spawnX, 30, 0);
        Quaternion roation = Quaternion.Euler(0,0,0);

        GameObject kamikazeEnemySpawn = (GameObject)Instantiate(kamikazeEnemy, spawnLocation, roation);
        kamikazeEnemySpawn.GetComponent<enemy03Manager>().setSpawnLocation(spawnX, spawnY);

    }

    void SpawnRedHeavyGunner()
    {

        GameObject[] gunners = GameObject.FindGameObjectsWithTag("EnemyShip2");

        if(gunners.Length == 2)
        {
            return;
        }

        int xMin = -18; int xMax = 18;
        int yMin =  15; int yMax = 15;
        int spawnX; int spawnY;

        int tries = 0;

        do
        {
            spawnX = Random.Range(xMin/3, xMax/3) * 3;
            spawnY = yMax;

            tries++;
            if(tries == 10)
            {
                Debug.Log("Can't find space to spawn red heavy gunner enemy!");
                return;
            }
        } while(IsEnemyThere(spawnX, spawnY, "EnemyShip2"));

        Vector3 spawnLocation = new Vector3(spawnX, 30, 0);
        Quaternion roation = Quaternion.Euler(0,0,0);

        GameObject redHeavyGunnerSpawn = (GameObject)Instantiate(redHeavyGunner, spawnLocation, roation);
        redHeavyGunnerSpawn.GetComponent<enemy02Manager>().setSpawnLocation(spawnX, spawnY);
    }

    void SpawnGreenHeavyGunner()
    {
        GameObject[] gunners = GameObject.FindGameObjectsWithTag("EnemyShip4");

        if(gunners.Length == 2)
        {
            return;
        }

        int xMin = -18; int xMax = 18;
        int yMin =  18; int yMax = 18;
        int spawnX; int spawnY;

        int tries = 0;

        do
        {
            spawnX = Random.Range(xMin/3, xMax/3) * 3;
            spawnY = yMax;

            tries++;
            if(tries == 10)
            {
                Debug.Log("Can't find space to spawn green heavy gunner enemy!");
                return;
            }
        } while(IsEnemyThere(spawnX, spawnY, "EnemyShip4"));

        Vector3 spawnLocation = new Vector3(spawnX, 30, 0);
        Quaternion roation = Quaternion.Euler(0,0,0);

        GameObject greenHeavyGunnerSpawn = (GameObject)Instantiate(greenHeavyGunner, spawnLocation, roation);
        greenHeavyGunnerSpawn.GetComponent<enemy04Manager>().setSpawnLocation(spawnX, spawnY);
    }

    bool IsEnemyThere(int x, int y, string tag)
    {
        GameObject[] normalEnemies = GameObject.FindGameObjectsWithTag("EnemyShip1");

        foreach (GameObject enemy in normalEnemies)
        {
            if(Mathf.Abs(enemy.transform.position.x - x) <= 2.5 && Mathf.Abs(enemy.transform.position.y - y) <= 2.5)
            {
                return true;
            }
        }

        return false;
    }

    bool IsPowerupThere(int x, int y)
    {
        GameObject[] powerups = GameObject.FindGameObjectsWithTag("Powerups");

        foreach (GameObject powerup in powerups)
        {
            if(Mathf.Abs(powerup.transform.position.x - x) <= 4.0)
            {
                return true;
            }
        }

        return false;
    }
}
