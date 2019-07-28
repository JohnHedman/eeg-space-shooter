using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySpawner : MonoBehaviour
{
    public int startingNormal = 5;
    public int startingKamikaze = 2;
    public int startingRedHeavyGunners = 2;
    public int startingGreenHeavyGunners = 1;

    public float normalEnemySpawnTime = 2.0f;
    public float kamikazeEnemySpawnTime = 10.0f;
    public float redHeavyGunnerSpawnTime = 20.0f;

    public float greenHeavyGunnerSpawnTime = 30.0f;

    public GameObject kamikazeEnemy;
    public GameObject normalEnemy;
    public GameObject redHeavyGunner;
    public GameObject greenHeavyGunner;
    public GameObject tripplePowerup;
    public GameObject healthPowerup;
    public GameObject shieldPowerup;
    
    public float trippleShotPowerupCooldown = 60.0f;
    public float healthPowerupCooldown = 30.0f;
    public float shieldPowerupCooldown = 30.0f;

    public GameObject player;


    private float tripplePowerupTimer = 0.0f;
    private float healthPowerupTimer = 0.0f;
    private float shieldPowerupTimer = 10.0f;




    // Start is called before the first frame update
    void Start()
    {
        shieldPowerupTimer = 5.0f;
        healthPowerupTimer = 30.0f;
        tripplePowerupTimer = 15.0f;


        player = GameObject.FindGameObjectWithTag("PlayerShip");
        for(int x = 0; x < startingNormal; x++)
        {
            SpawnNormalEnemy();
        }
        for(int x = 0; x < startingKamikaze; x++)
        {
            SpawnKamikazeEnemy();
        }
        InvokeRepeating("SpawnNormalEnemy", normalEnemySpawnTime, normalEnemySpawnTime);
        InvokeRepeating("SpawnKamikazeEnemy", kamikazeEnemySpawnTime, kamikazeEnemySpawnTime);
        InvokeRepeating("SpawnRedHeavyGunner", 1.0f, redHeavyGunnerSpawnTime);
        InvokeRepeating("SpawnGreenHeavyGunner", greenHeavyGunnerSpawnTime, greenHeavyGunnerSpawnTime);
        InvokeRepeating("CheckForPowerups", 1.0f, 5.0f);
        //InvokeRepeating("DebugPowerup", 1.0f, 1.0f);
    }

    void DebugPowerup()
    {
        Debug.Log("Health Timer: " + healthPowerupTimer.ToString());
        Debug.Log("Shield Timer: " + shieldPowerupTimer.ToString());
        Debug.Log("Tripple Timer: " + tripplePowerupTimer.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        if(player == null)
        {
            player = GameObject.FindGameObjectWithTag("PlayerShip");
        }
        
        tripplePowerupTimer -= Time.deltaTime;
        healthPowerupTimer -= Time.deltaTime;
        shieldPowerupTimer -= Time.deltaTime;
    }

    void CheckForPowerups()
    {
        if(player != null)
        {
            if(tripplePowerupTimer <= 0 && GetNumberOfEnemies() > 10)
            {
                SpawnTripplePowerup();
                tripplePowerupTimer = trippleShotPowerupCooldown;
            }
            else if(healthPowerupTimer <= 0 && player.GetComponent<playerDamage>().getCurrentHeath() <= 80)
            {
                SpawnHealthPowerup();
                healthPowerupTimer = healthPowerupCooldown;
            }
            else if(shieldPowerupTimer <= 0 && player.GetComponent<playerDamage>().getCurrentShield() <= 80)
            {
                SpawnShieldPowerup();
                shieldPowerupTimer = shieldPowerupCooldown;
            }
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

        Vector3 spawnLocation = new Vector3(spawnX, 25, 0);
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

        Vector3 spawnLocation = new Vector3(spawnX, 25, 0);
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

        Vector3 spawnLocation = new Vector3(spawnX, 25, 0);
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

        Vector3 spawnLocation = new Vector3(spawnX, 25, 0);
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
