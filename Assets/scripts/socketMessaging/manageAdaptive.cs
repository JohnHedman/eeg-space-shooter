using System;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Sockets;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class manageAdaptive : MonoBehaviour
{

    public GameObject playerShip;
    private int port = 5000;
    private string address = "127.0.0.1";
    
    private TcpClient client;
    private NetworkStream stream;
    private bool streamActive;


    void Start()
    {
        client = new TcpClient(address, port);
        stream = client.GetStream();
        streamActive = true;
        SendToServer("Start Adaptive!");
        InvokeRepeating("SendMessage", 11.0f, 5.0f);
        playerShip = GameObject.FindGameObjectWithTag("PlayerShip");
        InvokeRepeating("ReadMessage", 0.1f, 0.1f);
    }

    void Update()
    {
        if(playerShip == null){
			playerShip = GameObject.FindGameObjectWithTag("PlayerShip");
		}
    }

    void ReadMessage()
    {
        if(stream.DataAvailable)
        {
            byte[] readBuffer = new byte[1024];
            StringBuilder message = new StringBuilder();

            int size = stream.Read(readBuffer, 0, readBuffer.Length);

            message.AppendFormat("{0}", Encoding.ASCII.GetString(readBuffer, 0, size));

            Debug.Log("Recieved the message: " + message);

            DecodeMessage(message.ToString());
        }
    }

    void DecodeMessage(string message)
    {
        GameObject sceneManager = GameObject.FindGameObjectWithTag("SceneManager");


        if(message == "SpawnNormalEnemy"){
            sceneManager.GetComponent<adaptiveLevelManager>().SpawnNormalEnemy();
        }
        else if(message == "SpawnKamikazeEnemy"){
            sceneManager.GetComponent<adaptiveLevelManager>().SpawnKamikazeEnemy();
        }
        else if(message == "SpawnRedHeavyGunner"){
            sceneManager.GetComponent<adaptiveLevelManager>().SpawnRedHeavyGunner();
        }
        else if(message == "SpawnGreenHeavyGunner"){
            sceneManager.GetComponent<adaptiveLevelManager>().SpawnGreenHeavyGunner();
        }
        else if(message == "SpawnTripplePowerup"){
            sceneManager.GetComponent<adaptiveLevelManager>().SpawnTripplePowerup();
        }
        else if(message == "SpawnHealthPowerup"){
            sceneManager.GetComponent<adaptiveLevelManager>().SpawnHealthPowerup();
        }
        else if(message == "SpawnShieldPowerup"){
            sceneManager.GetComponent<adaptiveLevelManager>().SpawnShieldPowerup();
        }
        else{
            Debug.Log("Could not decode the message!");
        }
    }

    void SendMessage()
    {
        if(streamActive)
        {
            int enemy1Count = GameObject.FindGameObjectsWithTag("EnemyShip1").Length;
            int enemy2Count = GameObject.FindGameObjectsWithTag("EnemyShip2").Length;
            int enemy3Count = GameObject.FindGameObjectsWithTag("EnemyShip3").Length;
            int enemy4Count = GameObject.FindGameObjectsWithTag("EnemyShip4").Length;
            int enemyBullets = GameObject.FindGameObjectsWithTag("EnemyBullet").Length;
            int playerHealth; int playerShield;

            if(playerShip == null)
            {
                playerHealth = 0;
                playerShield = 0;
            }
            else
            {
                playerHealth = playerShip.GetComponent<playerDamage>().getCurrentHeath();
                playerShield = playerShip.GetComponent<playerDamage>().getCurrentShield();
            }

            string message = String.Format("{0}, {1}, {2}, {3}, {4}, {5}, {6}", enemy1Count, enemy2Count, enemy3Count, enemy4Count, enemyBullets, playerHealth, playerShield);

            SendToServer(message);

            Debug.Log("Sent: " + message);
        }
    }

    void SendToServer(string message)
    {
        Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);

        stream.Write(data, 0, data.Length);
    }

    public void EndServerStream()
    {
        streamActive = false;
        string message = "End Stream!";
        SendToServer(message);
        Debug.Log("Sent: " + message);
    }
}
