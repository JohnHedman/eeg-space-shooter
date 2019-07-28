// Socket Libraries
using System;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Sockets;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class sendScene : MonoBehaviour
{

    public GameObject playerShip;


    private int port = 5000;
    private string address = "127.0.0.1";
    private int count = 0;
    private TcpClient client;
    private NetworkStream stream;

    void Start()
    {
        client = new TcpClient(address, port);
        stream = client.GetStream();

        InvokeRepeating("SendMessage", 1.0f, 1.0f);
        playerShip = GameObject.FindGameObjectWithTag("PlayerShip");
    }

    // Update is called once per frame
    void Update()
    {
        if(playerShip == null){
			playerShip = GameObject.FindGameObjectWithTag("PlayerShip");
		}
    }

    void SendMessage()
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
        count++;
    }

    void SendToServer(string message)
    {
        Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);

        stream.Write(data, 0, data.Length);
    }


}
